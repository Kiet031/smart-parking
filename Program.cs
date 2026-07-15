using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SmartParking;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Khởi động bootstrapper tự thiết lập thư mục và chuyển dịch dữ liệu cũ
        Bootstrapper.Initialize();

        // Bind global unhandled exception event listeners inside the main entry point
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        
        // Intercept UI thread errors
        Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
        
        // Intercept background thread or peripheral processing failures
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

        ApplicationConfiguration.Initialize();
        Application.Run(new LoginForm());
    }

    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
        ExceptionManager.HandleException(e.Exception, "Lỗi Luồng Giao Diện (Thread UI)");
    }

    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            ExceptionManager.HandleException(ex, "Lỗi Hệ Thống Nghiêm Trọng (Domain Unhandled)");
        }
    }
}

public static class Bootstrapper
{
    public static void Initialize()
    {
        try
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string imageDataDir = Path.Combine(baseDir, "ImageData");
            string vaoDir = Path.Combine(imageDataDir, "Transactions", "Vao");
            string raDir = Path.Combine(imageDataDir, "Transactions", "Ra");
            string subDir = Path.Combine(imageDataDir, "Subscriptions");

            // Tạo các thư mục lưu trữ hệ thống mới
            Directory.CreateDirectory(vaoDir);
            Directory.CreateDirectory(raDir);
            Directory.CreateDirectory(subDir);

            // Chạy dọn dẹp và di chuyển cấu trúc cũ
            MigrateAndCleanupLegacy(baseDir, subDir);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khởi tạo Bootstrapper: {ex.Message}");
        }
    }

    private static void MigrateAndCleanupLegacy(string baseDir, string newSubDir)
    {
        string legacySubDir = Path.Combine(baseDir, "Subscriptions");
        if (!Directory.Exists(legacySubDir)) return;

        string[] legacyCategories = { "Students", "Staff", "Lecturers" };
        bool hasLegacyDirs = false;

        foreach (var category in legacyCategories)
        {
            string legacyPath = Path.Combine(legacySubDir, category);
            if (Directory.Exists(legacyPath))
            {
                hasLegacyDirs = true;
                string[] files = Directory.GetFiles(legacyPath);
                foreach (var file in files)
                {
                    try
                    {
                        string filename = Path.GetFileName(file);
                        MigrateFileToDatabaseUsers(file, category, filename);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi di chuyển file {file}: {ex.Message}");
                    }
                }

                try
                {
                    if (Directory.GetFiles(legacyPath).Length == 0 && Directory.GetDirectories(legacyPath).Length == 0)
                    {
                        Directory.Delete(legacyPath, true);
                    }
                }
                catch { }
            }
        }

        if (hasLegacyDirs)
        {
            try
            {
                if (Directory.GetDirectories(legacySubDir).Length == 0 && Directory.GetFiles(legacySubDir).Length == 0)
                {
                    Directory.Delete(legacySubDir, true);
                }
            }
            catch { }
        }
    }

    private static void MigrateFileToDatabaseUsers(string filePath, string category, string filename)
    {
        string pattern = $"%{category}/{filename}";

        try
        {
            using (var connection = new Npgsql.NpgsqlConnection(DatabaseHelper.GetConnectionString()))
            {
                connection.Open();
                string query = @"
                    SELECT member_id, license_plate, member_image_path, vehicle_image_path
                    FROM subscription_users
                    WHERE member_image_path LIKE @pattern OR vehicle_image_path LIKE @pattern;
                ";
                using (var cmd = new Npgsql.NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@pattern", pattern);
                    using (var reader = cmd.ExecuteReader())
                    {
                        var updates = new System.Collections.Generic.List<(string memberId, string plate, string type)>();
                        while (reader.Read())
                        {
                            string memberId = reader.GetString(0);
                            string plate = reader.GetString(1);
                            string memberImg = reader.GetString(2);
                            string vehicleImg = reader.GetString(3);

                            if (memberImg.EndsWith($"{category}/{filename}", StringComparison.OrdinalIgnoreCase))
                            {
                                updates.Add((memberId, plate, "portrait"));
                            }
                            if (vehicleImg.EndsWith($"{category}/{filename}", StringComparison.OrdinalIgnoreCase))
                            {
                                updates.Add((memberId, plate, "vehicle"));
                            }
                        }
                        reader.Close();

                        if (updates.Count > 0)
                        {
                            using (Image img = Image.FromFile(filePath))
                            {
                                foreach (var up in updates)
                                {
                                    string newRelPath = FileStorageManager.SaveCompressedSubscriptionImage(img, up.plate, up.type);

                                    string col = (up.type == "portrait") ? "member_image_path" : "vehicle_image_path";
                                    string updateQuery = $"UPDATE subscription_users SET {col} = @newPath WHERE member_id = @memberId";
                                    using (var updateCmd = new Npgsql.NpgsqlCommand(updateQuery, connection))
                                    {
                                        updateCmd.Parameters.AddWithValue("@newPath", newRelPath);
                                        updateCmd.Parameters.AddWithValue("@memberId", up.memberId);
                                        updateCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            File.Delete(filePath);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi migrate file {filename} trong {category}: {ex.Message}");
        }
    }
}