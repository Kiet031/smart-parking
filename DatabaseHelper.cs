using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Npgsql;

namespace SmartParking
{
    public class ParkingRecord
    {
        public string CardID { get; set; } = string.Empty;
        public DateTime EntryTime { get; set; }
        public string EntryFrontImage { get; set; } = string.Empty;
        public string EntryRearImage { get; set; } = string.Empty;
    }

    public static class DatabaseHelper
    {
        public static string ImagesDir => FileStorageManager.SharedServerPath;

        private static readonly Dictionary<string, string> EnvVars = new Dictionary<string, string>();

        public static void LoadEnv()
        {
            EnvVars.Clear();
            try
            {
                string[] paths = {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env"),
                    Path.Combine(Directory.GetCurrentDirectory(), ".env"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".env")
                };

                foreach (var path in paths)
                {
                    if (File.Exists(path))
                    {
                        foreach (var line in File.ReadAllLines(path))
                        {
                            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;
                            var idx = line.IndexOf('=');
                            if (idx > 0)
                            {
                                var key = line.Substring(0, idx).Trim();
                                var val = line.Substring(idx + 1).Trim();
                                EnvVars[key] = val;
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading .env: {ex.Message}");
            }
        }

        public static string GetEnv(string key, string defaultValue = "")
        {
            if (EnvVars.Count == 0)
            {
                LoadEnv();
            }
            return EnvVars.TryGetValue(key, out var val) ? val : defaultValue;
        }

        private static string GetMasterConnectionString()
        {
            string host = GetEnv("SQL_HOST", "localhost");
            string port = GetEnv("SQL_PORT", "5432");
            string user = GetEnv("SQL_USER", "postgres");
            string pass = GetEnv("SQL_PASSWORD", "12345678");

            return $"Host={host};Port={port};Username={user};Password={pass};Database=postgres;TrustServerCertificate=true;";
        }

        public static string GetConnectionString()
        {
            string host = GetEnv("SQL_HOST", "localhost");
            string port = GetEnv("SQL_PORT", "5432");
            string user = GetEnv("SQL_USER", "postgres");
            string pass = GetEnv("SQL_PASSWORD", "12345678");
            string db = GetEnv("SQL_DATABASE", "smart_parking");

            return $"Host={host};Port={port};Username={user};Password={pass};Database={db};TrustServerCertificate=true;";
        }

        private static void EnsureDatabaseExists()
        {
            string dbName = GetEnv("SQL_DATABASE", "smart_parking");
            using (var conn = new NpgsqlConnection(GetMasterConnectionString()))
            {
                conn.Open();
                string checkDbQuery = "SELECT 1 FROM pg_database WHERE datname = @DbName";
                using (var cmd = new NpgsqlCommand(checkDbQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@DbName", dbName);
                    var exists = cmd.ExecuteScalar();
                    if (exists == null)
                    {
                        using (var createCmd = new NpgsqlCommand($"CREATE DATABASE {dbName}", conn))
                        {
                            createCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public static void InitializeDatabase()
        {
            try
            {
                EnsureDatabaseExists();

                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    // 1. card_types Table
                    string createCardTypes = @"
                        CREATE TABLE IF NOT EXISTS card_types (
                            card_type_id VARCHAR(50) PRIMARY KEY,
                            card_type_name VARCHAR(100) NOT NULL,
                            default_fee NUMERIC NOT NULL DEFAULT 0
                        );
                    ";
                    using (var cmd = new NpgsqlCommand(createCardTypes, connection)) { cmd.ExecuteNonQuery(); }

                    // 2. rfid_cards Table
                    string createRfidCards = @"
                        CREATE TABLE IF NOT EXISTS rfid_cards (
                            card_id VARCHAR(50) PRIMARY KEY,
                            card_type_id VARCHAR(50) REFERENCES card_types(card_type_id) ON DELETE CASCADE,
                            status VARCHAR(50) NOT NULL DEFAULT 'Active',
                            registration_date DATE NOT NULL DEFAULT CURRENT_DATE,
                            expiry_date DATE NOT NULL
                        );
                    ";
                    using (var cmd = new NpgsqlCommand(createRfidCards, connection)) { cmd.ExecuteNonQuery(); }

                    // 3. subscription_users Table (BỔ SUNG THÊM CỘT ẢNH XE)
                    string createSubscriptionUsers = @"
                        CREATE SEQUENCE IF NOT EXISTS subscription_users_seq START WITH 4;

                        CREATE TABLE IF NOT EXISTS subscription_users (
                            member_id VARCHAR(50) PRIMARY KEY DEFAULT 'MB' || to_char(nextval('subscription_users_seq'), 'FM000000'),
                            card_id VARCHAR(50) REFERENCES rfid_cards(card_id) ON DELETE CASCADE,
                            user_code VARCHAR(50) NOT NULL,
                            full_name VARCHAR(200) NOT NULL,
                            birth_date DATE NOT NULL,
                            member_image_path VARCHAR(500) NOT NULL,
                            vehicle_info TEXT,
                            license_plate VARCHAR(50) NOT NULL,
                            -- THÊM MỚI: Cột lưu đường dẫn ảnh xe đăng ký
                            vehicle_image_path VARCHAR(500) NOT NULL DEFAULT '[NO_IMAGE_FALLBACK]'
                        );
                    ";
                    using (var cmd = new NpgsqlCommand(createSubscriptionUsers, connection)) { cmd.ExecuteNonQuery(); }


                    // 5. active_parking Table
                    string createActiveParking = @"
                        CREATE TABLE IF NOT EXISTS active_parking (
                            card_id VARCHAR(50) PRIMARY KEY REFERENCES rfid_cards(card_id) ON DELETE CASCADE,
                            entry_time TIMESTAMP NOT NULL,
                            entry_image_path VARCHAR(500) NOT NULL,
                            entry_gate VARCHAR(100) NOT NULL
                        );
                    ";
                    using (var cmd = new NpgsqlCommand(createActiveParking, connection)) { cmd.ExecuteNonQuery(); }

                    // 6. parking_history Table - Partitioned by Range
                    string createParkingHistory = @"
                        CREATE TABLE IF NOT EXISTS parking_history (
                            transaction_id BIGSERIAL,
                            card_id VARCHAR(50) NOT NULL,
                            entry_time TIMESTAMP NOT NULL,
                            entry_image_path VARCHAR(500) NOT NULL,
                            exit_time TIMESTAMP NOT NULL,
                            exit_image_path VARCHAR(500) NOT NULL,
                            fee_collected NUMERIC NOT NULL DEFAULT 0,
                            exit_gate VARCHAR(100) NOT NULL,
                            PRIMARY KEY (transaction_id, exit_time)
                        ) PARTITION BY RANGE (exit_time);
                    ";
                    using (var cmd = new NpgsqlCommand(createParkingHistory, connection)) { cmd.ExecuteNonQuery(); }

                    // 7. users Table
                    string createUsers = @"
                        CREATE TABLE IF NOT EXISTS users (
                            username VARCHAR(50) PRIMARY KEY,
                            password VARCHAR(50) NOT NULL,
                            role VARCHAR(50) NOT NULL
                        );
                    ";
                    using (var cmd = new NpgsqlCommand(createUsers, connection)) { cmd.ExecuteNonQuery(); }

                    // 8. settings Table
                    string createSettings = @"
                        CREATE TABLE IF NOT EXISTS settings (
                            setting_key VARCHAR(100) PRIMARY KEY,
                            setting_value TEXT
                        );
                    ";
                    using (var cmd = new NpgsqlCommand(createSettings, connection)) { cmd.ExecuteNonQuery(); }

                    SeedDefaultData(connection);

                    EnsurePartitionExists(DateTime.Today);
                    EnsurePartitionExists(DateTime.Today.AddMonths(1));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, "Khởi tạo hệ thống cơ sở dữ liệu bãi xe");
                Application.Exit();
            }
        }

        private static void SeedDefaultData(NpgsqlConnection connection)
        {
            string checkUsers = "SELECT COUNT(*) FROM users";
            long userCount = 0;
            using (var cmd = new NpgsqlCommand(checkUsers, connection))
            {
                userCount = Convert.ToInt64(cmd.ExecuteScalar() ?? 0L);
            }
            if (userCount == 0)
            {
                string insertUsers = @"
                    INSERT INTO users (username, password, role) VALUES ('admin', 'admin123', 'Admin');
                    INSERT INTO users (username, password, role) VALUES ('guard', 'guard123', 'Guard');
                ";
                using (var cmd = new NpgsqlCommand(insertUsers, connection)) { cmd.ExecuteNonQuery(); }
            }

            string checkSettings = "SELECT COUNT(*) FROM settings";
            long settingCount = 0;
            using (var cmd = new NpgsqlCommand(checkSettings, connection))
            {
                settingCount = Convert.ToInt64(cmd.ExecuteScalar() ?? 0L);
            }
            if (settingCount == 0)
            {
                string insertSettings = @"
                    -- CẤU HÌNH MẶC ĐỊNH LÀN VÀO - CAMERA TRƯỚC
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamEntryFrontIP', '111.222.333.444');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamEntryFrontPort', '554');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamEntryFrontUser', 'admin');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamEntryFrontPass', 'abcd1234');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamEntryFrontUrl', 'rtsp://admin:abcd1234@111.222.333.444:554/Streaming/Channels/101');
                    
                    -- CẤU HÌNH MẶC ĐỊNH LÀN VÀO - CAMERA SAU
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamEntryRearIP', '111.222.333.444');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamEntryRearPort', '554');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamEntryRearUser', 'admin');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamEntryRearPass', 'abcd1234');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamEntryRearUrl', 'rtsp://admin:abcd1234@111.222.333.444:554/Streaming/Channels/102');

                    -- CẤU HÌNH MẶC ĐỊNH LÀN RA - CAMERA TRƯỚC
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamExitFrontIP', '111.222.333.444');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamExitFrontPort', '554');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamExitFrontUser', 'admin');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamExitFrontPass', 'abc12345');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamExitFrontUrl', 'rtsp://admin:abc12345@111.222.333.444:554/Streaming/Channels/201');

                    -- CẤU HÌNH MẶC ĐỊNH LÀN RA - CAMERA SAU
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamExitRearIP', '111.222.333.444');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamExitRearPort', '554');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamExitRearUser', 'admin');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamExitRearPass', 'abc12345');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamExitRearUrl', 'rtsp://admin:abc12345@111.222.333.444:554/Streaming/Channels/202');
                    
                    -- ĐỊNH CẤU HÌNH CHUNG
                    INSERT INTO settings (setting_key, setting_value) VALUES ('CamType', 'Hikvision / HiLook');
                    INSERT INTO settings (setting_key, setting_value) VALUES ('HistoryRetentionMonths', '3');
                ";
                using (var cmd = new NpgsqlCommand(insertSettings, connection)) { cmd.ExecuteNonQuery(); }
            }

            string checkCardTypes = "SELECT COUNT(*) FROM card_types";
            long cardTypeCount = 0;
            using (var cmd = new NpgsqlCommand(checkCardTypes, connection))
            {
                cardTypeCount = Convert.ToInt64(cmd.ExecuteScalar() ?? 0L);
            }
            if (cardTypeCount == 0)
            {
                string insertCardTypes = @"
                    INSERT INTO card_types (card_type_id, card_type_name, default_fee) VALUES ('1', 'Vé tháng (Sinh viên)', 0);
                    INSERT INTO card_types (card_type_id, card_type_name, default_fee) VALUES ('2', 'Vé tháng (Giảng viên)', 0);
                    INSERT INTO card_types (card_type_id, card_type_name, default_fee) VALUES ('3', 'Vé tháng (Cán bộ)', 0);
                    INSERT INTO card_types (card_type_id, card_type_name, default_fee) VALUES ('4', 'Vé lượt', 5000);
                ";
                using (var cmd = new NpgsqlCommand(insertCardTypes, connection)) { cmd.ExecuteNonQuery(); }
            }

            string checkCards = "SELECT COUNT(*) FROM rfid_cards";
            long cardCount = 0;
            using (var cmd = new NpgsqlCommand(checkCards, connection))
            {
                cardCount = Convert.ToInt64(cmd.ExecuteScalar() ?? 0L);
            }
            if (cardCount == 0)
            {
                string insertCards = @"
                    INSERT INTO rfid_cards (card_id, card_type_id, status, registration_date, expiry_date) VALUES ('11111111', '1', 'Active', CURRENT_DATE, CURRENT_DATE + INTERVAL '1 year');
                    INSERT INTO rfid_cards (card_id, card_type_id, status, registration_date, expiry_date) VALUES ('22222222', '2', 'Active', CURRENT_DATE, CURRENT_DATE + INTERVAL '1 year');
                    INSERT INTO rfid_cards (card_id, card_type_id, status, registration_date, expiry_date) VALUES ('33333333', '3', 'Active', CURRENT_DATE, CURRENT_DATE + INTERVAL '1 year');
                    INSERT INTO rfid_cards (card_id, card_type_id, status, registration_date, expiry_date) VALUES ('44444444', '4', 'Active', CURRENT_DATE, CURRENT_DATE + INTERVAL '10 years');
                    INSERT INTO rfid_cards (card_id, card_type_id, status, registration_date, expiry_date) VALUES ('55555555', '4', 'Active', CURRENT_DATE, CURRENT_DATE + INTERVAL '10 years');
                ";
                using (var cmd = new NpgsqlCommand(insertCards, connection)) { cmd.ExecuteNonQuery(); }

                string insertSubUsers = @"
                    INSERT INTO subscription_users (member_id, card_id, user_code, full_name, birth_date, member_image_path, vehicle_info, license_plate, vehicle_image_path)
                    VALUES ('MB000001', '11111111', 'SV12345', 'Nguyen Van A', '2004-05-15', '[NO_IMAGE_FALLBACK]', 'Yamaha Exciter, Den', '29F1-123.45', '[NO_IMAGE_FALLBACK]');
                    
                    INSERT INTO subscription_users (member_id, card_id, user_code, full_name, birth_date, member_image_path, vehicle_info, license_plate, vehicle_image_path)
                    VALUES ('MB000002', '22222222', 'GV09876', 'Tran Thi B', '1985-08-20', '[NO_IMAGE_FALLBACK]', 'Honda Lead, Do', '29H1-678.90', '[NO_IMAGE_FALLBACK]');
                    
                    INSERT INTO subscription_users (member_id, card_id, user_code, full_name, birth_date, member_image_path, vehicle_info, license_plate, vehicle_image_path)
                    VALUES ('MB000003', '33333333', 'CB00555', 'Le Hoang C', '1980-01-10', '[NO_IMAGE_FALLBACK]', 'VinFast Klara, Trang', '29A1-999.99', '[NO_IMAGE_FALLBACK]');
                ";
                using (var cmd = new NpgsqlCommand(insertSubUsers, connection)) { cmd.ExecuteNonQuery(); }
            }
        }

        public static void EnsurePartitionExists(DateTime date)
        {
            string tableName = $"parking_history_y{date.Year}m{date.Month:D2}";
            DateTime startOfMonth = new DateTime(date.Year, date.Month, 1);
            DateTime startOfNextMonth = startOfMonth.AddMonths(1);

            string startStr = startOfMonth.ToString("yyyy-MM-dd HH:mm:ss");
            string endStr = startOfNextMonth.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string createPartition = $@"
                        CREATE TABLE IF NOT EXISTS {tableName} PARTITION OF parking_history
                        FOR VALUES FROM ('{startStr}') TO ('{endStr}');
                    ";
                    using (var cmd = new NpgsqlCommand(createPartition, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ensuring partition table {tableName} exists: {ex.Message}");
            }
        }

        public static void CleanupOldPartitions(int retentionMonths)
        {
            if (retentionMonths <= 0) return;

            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    string getPartitionsQuery = @"
                        SELECT child.relname AS partition_name
                        FROM pg_inherits
                        JOIN pg_class parent ON pg_inherits.inhparent = parent.oid
                        JOIN pg_class child ON pg_inherits.inhrelid = child.oid
                        WHERE parent.relname = 'parking_history';
                    ";

                    List<string> partitions = new List<string>();
                    using (var cmd = new NpgsqlCommand(getPartitionsQuery, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            partitions.Add(reader.GetString(0));
                        }
                    }

                    DateTime cutoff = DateTime.Today.AddMonths(-retentionMonths);

                    foreach (var partitionName in partitions)
                    {
                        var match = System.Text.RegularExpressions.Regex.Match(partitionName, @"parking_history_y(\d{4})m(\d{2})");
                        if (match.Success)
                        {
                            int year = int.Parse(match.Groups[1].Value);
                            int month = int.Parse(match.Groups[2].Value);

                            DateTime partitionEnd = new DateTime(year, month, 1).AddMonths(1);

                            if (partitionEnd < cutoff)
                            {
                                Console.WriteLine($"Dropping old history partition: {partitionName}");
                                string dropQuery = $"DROP TABLE IF EXISTS {partitionName};";
                                using (var dropCmd = new NpgsqlCommand(dropQuery, connection))
                                {
                                    dropCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cleaning up old partitions: {ex.Message}");
            }
        }

        #region Nghiệp vụ Người dùng (Users CRUD & Auth)
        public static string? ValidateUser(string username, string password)
        {
            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = "SELECT role FROM users WHERE username = @Username AND password = @Password";
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        return cmd.ExecuteScalar()?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, "Đăng nhập hệ thống (Xác thực người dùng)");
                return null;
            }
        }

        public static DataTable GetAllUsers()
        {
            var dt = new DataTable();
            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = "SELECT username, password, role FROM users";
                    using (var cmd = new NpgsqlCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, "Tải danh sách tài khoản");
            }
            return dt;
        }

        public static void InsertUser(string username, string password, string role)
        {
            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = "INSERT INTO users (username, password, role) VALUES (@Username, @Password, @Role)";
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Role", role);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, "Thêm tài khoản người dùng");
            }
        }

        public static void UpdateUser(string username, string password, string role)
        {
            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = "UPDATE users SET password = @Password, role = @Role WHERE username = @Username";
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Role", role);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, "Cập nhật tài khoản người dùng");
            }
        }

        public static void DeleteUser(string username)
        {
            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = "DELETE FROM users WHERE username = @Username";
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, "Xóa tài khoản người dùng");
            }
        }
        #endregion

        #region Nghiệp vụ Bãi xe (ActiveParking CRUD)
        public static bool IsCardInParking(string cardId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = "SELECT COUNT(1) FROM active_parking WHERE card_id = @CardID";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CardID", cardId);
                        long count = Convert.ToInt64(command.ExecuteScalar() ?? 0L);
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, "Kiểm tra thẻ trong bãi");
                return false;
            }
        }

        public static void InsertActiveParking(string cardId, string frontImgPath, string rearImgPath)
        {
            try
            {
                EnsurePartitionExists(DateTime.Today);

                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string insertQuery = @"
                        INSERT INTO active_parking (card_id, entry_time, entry_image_path, entry_gate)
                        VALUES (@CardID, @EntryTime, @EntryImagePath, @EntryGate);
                    ";
                    using (var command = new NpgsqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@CardID", cardId);
                        command.Parameters.AddWithValue("@EntryTime", DateTime.Now);
                        command.Parameters.AddWithValue("@EntryImagePath", $"{frontImgPath};{rearImgPath}");
                        command.Parameters.AddWithValue("@EntryGate", "Cổng số 1");
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, "Ghi nhận xe vào");
                throw;
            }
        }

        public static ParkingRecord? GetActiveParking(string cardId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = @"
                        SELECT card_id, entry_time, entry_image_path 
                        FROM active_parking 
                        WHERE card_id = @CardID;
                    ";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CardID", cardId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string combinedPath = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                                string frontPath = string.Empty;
                                string rearPath = string.Empty;

                                if (!string.IsNullOrEmpty(combinedPath))
                                {
                                    var parts = combinedPath.Split(';');
                                    frontPath = parts[0];
                                    if (parts.Length > 1) rearPath = parts[1];
                                }

                                return new ParkingRecord
                                {
                                    CardID = reader.GetString(0),
                                    EntryTime = reader.GetDateTime(1),
                                    EntryFrontImage = frontPath,
                                    EntryRearImage = rearPath
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, "Lấy thông tin xe trong bãi");
            }
            return null;
        }

        public static void DeleteActiveParking(string cardId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM active_parking WHERE card_id = @CardID;";
                    using (var command = new NpgsqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@CardID", cardId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, "Xóa lượt gửi xe hiện tại");
            }
        }

        public static void UpdateActiveParking(string cardId, string entryTime, string frontPath, string rearPath)
        {
            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = @"
                        UPDATE active_parking 
                        SET entry_time = @EntryTime, entry_image_path = @EntryImagePath 
                        WHERE card_id = @CardID;
                    ";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CardID", cardId);
                        command.Parameters.AddWithValue("@EntryTime", DateTime.Parse(entryTime));
                        command.Parameters.AddWithValue("@EntryImagePath", $"{frontPath};{rearPath}");
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, "Cập nhật lượt gửi xe");
            }
        }

        public static DataTable GetAllActiveParking()
        {
            var dt = new DataTable();
            dt.Columns.Add("CardID", typeof(string));
            dt.Columns.Add("EntryTime", typeof(string));
            dt.Columns.Add("FrontImagePath", typeof(string));
            dt.Columns.Add("RearImagePath", typeof(string));

            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = "SELECT card_id, entry_time, entry_image_path FROM active_parking ORDER BY entry_time DESC";
                    using (var cmd = new NpgsqlCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string cardId = reader.GetString(0);
                            string entryTime = reader.GetDateTime(1).ToString("yyyy-MM-dd HH:mm:ss");
                            string combinedPath = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            string frontPath = "";
                            string rearPath = "";
                            if (!string.IsNullOrEmpty(combinedPath))
                            {
                                var parts = combinedPath.Split(';');
                                frontPath = parts[0];
                                if (parts.Length > 1) rearPath = parts[1];
                            }
                            dt.Rows.Add(cardId, entryTime, frontPath, rearPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, "Tải danh sách xe trong bãi");
            }
            return dt;
        }

        public static void CompleteExitTransaction(string cardId, string exitFrontImg, string exitRearImg, string exitGate = "Cổng số 1")
        {
            DateTime exitTime = DateTime.Now;
            EnsurePartitionExists(exitTime);

            ParkingRecord? active = GetActiveParking(cardId);
            if (active == null)
            {
                throw new Exception("Không tìm thấy xe trong bãi.");
            }

            decimal fee = 5000;
            using (var connection = new NpgsqlConnection(GetConnectionString()))
            {
                connection.Open();
                string feeQuery = @"
                    SELECT ct.default_fee 
                    FROM rfid_cards rc
                    JOIN card_types ct ON rc.card_type_id = ct.card_type_id
                    WHERE rc.card_id = @CardID;
                ";
                using (var cmd = new NpgsqlCommand(feeQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@CardID", cardId);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        fee = Convert.ToDecimal(result);
                    }
                }

                using (var tx = connection.BeginTransaction())
                {
                    try
                    {
                        string insertHistory = @"
                            INSERT INTO parking_history (card_id, entry_time, entry_image_path, exit_time, exit_image_path, fee_collected, exit_gate)
                            VALUES (@CardID, @EntryTime, @EntryImagePath, @ExitTime, @ExitImagePath, @FeeCollected, @ExitGate);
                        ";
                        using (var cmd = new NpgsqlCommand(insertHistory, connection, tx))
                        {
                            cmd.Parameters.AddWithValue("@CardID", cardId);
                            cmd.Parameters.AddWithValue("@EntryTime", active.EntryTime);
                            cmd.Parameters.AddWithValue("@EntryImagePath", $"{active.EntryFrontImage};{active.EntryRearImage}");
                            cmd.Parameters.AddWithValue("@ExitTime", exitTime);
                            cmd.Parameters.AddWithValue("@ExitImagePath", $"{exitFrontImg};{exitRearImg}");
                            cmd.Parameters.AddWithValue("@FeeCollected", fee);
                            cmd.Parameters.AddWithValue("@ExitGate", exitGate);
                            cmd.ExecuteNonQuery();
                        }

                        string deleteActive = "DELETE FROM active_parking WHERE card_id = @CardID;";
                        using (var cmd = new NpgsqlCommand(deleteActive, connection, tx))
                        {
                            cmd.Parameters.AddWithValue("@CardID", cardId);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                    catch (Exception)
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void ResetDatabase()
        {
            using (var connection = new NpgsqlConnection(GetConnectionString()))
            {
                connection.Open();
                string query = "TRUNCATE TABLE active_parking CASCADE;";
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable GetRecentLogs(int limit = 50)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Mã Thẻ", typeof(string));
            dataTable.Columns.Add("Giờ Vào", typeof(string));
            dataTable.Columns.Add("Ảnh Trước", typeof(string));
            dataTable.Columns.Add("Ảnh Sau", typeof(string));

            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = @"
                        SELECT card_id, entry_time, entry_image_path 
                        FROM active_parking 
                        ORDER BY entry_time DESC 
                        LIMIT @Limit;
                    ";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Limit", limit);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string cardId = reader.GetString(0);
                                string entryTime = reader.GetDateTime(1).ToString("yyyy-MM-dd HH:mm:ss");
                                string combinedPath = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                string frontPath = "";
                                string rearPath = "";
                                if (!string.IsNullOrEmpty(combinedPath))
                                {
                                    var parts = combinedPath.Split(';');
                                    frontPath = Path.GetFileName(parts[0]);
                                    if (parts.Length > 1) rearPath = Path.GetFileName(parts[1]);
                                }
                                dataTable.Rows.Add(cardId, entryTime, frontPath, rearPath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching recent logs: {ex.Message}");
            }
            return dataTable;
        }
        #endregion

        #region Cấu hình Hệ thống (Settings Key-Value)
        public static string GetSetting(string key, string defaultValue = "")
        {
            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = "SELECT setting_value FROM settings WHERE setting_key = @SettingKey";
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@SettingKey", key);
                        var val = cmd.ExecuteScalar();
                        return val?.ToString() ?? defaultValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading setting {key}: {ex.Message}");
                return defaultValue;
            }
        }

        public static void SaveSetting(string key, string value)
        {
            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = @"
                        INSERT INTO settings (setting_key, setting_value) 
                        VALUES (@SettingKey, @SettingValue)
                        ON CONFLICT (setting_key) DO UPDATE SET setting_value = @SettingValue;
                    ";
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@SettingKey", key);
                        cmd.Parameters.AddWithValue("@SettingValue", value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving setting {key}: {ex.Message}");
            }
        }

        public static string GetCameraUrl(string key)
        {
            string fallback = "rtsp://admin:password@111.222.333.444:554/Streaming/Channels/101";
            if (key == "CamEntryRearUrl") fallback = "rtsp://admin:password@111.222.333.444:554/Streaming/Channels/102";
            else if (key == "CamExitFrontUrl" || key == "CamTruocUrl") fallback = "rtsp://admin:password@111.222.333.444:554/Streaming/Channels/201";
            else if (key == "CamExitRearUrl" || key == "CamSauUrl") fallback = "rtsp://admin:password@111.222.333.444:554/Streaming/Channels/202";
            return GetSetting(key, fallback);
        }

        public static DataTable GetTableData(string tableName)
        {
            var dt = new DataTable();
            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    string query = $"SELECT * FROM {tableName}";
                    using (var cmd = new NpgsqlCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, $"Tải dữ liệu bảng {tableName}");
            }
            return dt;
        }

        public static void ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var kp in parameters)
                            {
                                cmd.Parameters.AddWithValue(kp.Key, kp.Value ?? DBNull.Value);
                            }
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, "Thực thi truy vấn cơ sở dữ liệu");
            }
        }

        public static (DataTable Data, int TotalCount) GetPaginatedTableData(string tableName, string searchTerm, int limit, int offset)
        {
            var dt = new DataTable();
            int totalCount = 0;

            try
            {
                using (var connection = new NpgsqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    string whereClause = "";
                    var parameters = new Dictionary<string, object>();

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        string searchPattern = $"%{searchTerm}%";
                        parameters.Add("search", searchPattern);

                        if (tableName == "users")
                        {
                            whereClause = "WHERE username LIKE @search OR role LIKE @search";
                        }
                        else if (tableName == "active_parking")
                        {
                            whereClause = "WHERE card_id LIKE @search OR entry_gate LIKE @search";
                        }
                        else if (tableName == "card_types")
                        {
                            whereClause = "WHERE card_type_id LIKE @search OR card_type_name LIKE @search";
                        }
                        else if (tableName == "rfid_cards")
                        {
                            whereClause = "WHERE card_id LIKE @search OR card_type_id LIKE @search OR status LIKE @search";
                        }
                        else if (tableName == "subscription_users")
                        {
                            whereClause = "WHERE member_id LIKE @search OR card_id LIKE @search OR full_name LIKE @search OR license_plate LIKE @search";
                        }
                        else if (tableName == "parking_history")
                        {
                            whereClause = "WHERE card_id LIKE @search OR exit_gate LIKE @search";
                        }
                        else if (tableName == "settings")
                        {
                            whereClause = "WHERE setting_key LIKE @search OR setting_value LIKE @search";
                        }
                    }

                    string countQuery = $"SELECT COUNT(1) FROM {tableName} {whereClause}";
                    using (var cmd = new NpgsqlCommand(countQuery, connection))
                    {
                        foreach (var kp in parameters)
                        {
                            cmd.Parameters.AddWithValue(kp.Key, kp.Value);
                        }
                        totalCount = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    }

                    string orderBy = "";
                    if (tableName == "users") orderBy = "ORDER BY username";
                    else if (tableName == "active_parking") orderBy = "ORDER BY entry_time DESC";
                    else if (tableName == "card_types") orderBy = "ORDER BY card_type_id";
                    else if (tableName == "rfid_cards") orderBy = "ORDER BY card_id";
                    else if (tableName == "subscription_users") orderBy = "ORDER BY member_id";
                    else if (tableName == "parking_history") orderBy = "ORDER BY exit_time DESC";
                    else if (tableName == "settings") orderBy = "ORDER BY setting_key";

                    string dataQuery = $"SELECT * FROM {tableName} {whereClause} {orderBy} LIMIT @limit OFFSET @offset";
                    using (var cmd = new NpgsqlCommand(dataQuery, connection))
                    {
                        foreach (var kp in parameters)
                        {
                            cmd.Parameters.AddWithValue(kp.Key, kp.Value);
                        }
                        cmd.Parameters.AddWithValue("limit", limit);
                        cmd.Parameters.AddWithValue("offset", offset);

                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, $"Tải dữ liệu phân trang bảng {tableName}");
            }

            return (dt, totalCount);
        }
        #endregion
    }
}
