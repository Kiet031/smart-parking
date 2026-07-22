# Smart Parking

Smart Parking là một ứng dụng desktop Windows được xây dựng trên .NET WinForms để hỗ trợ quản lý bãi đỗ xe thông minh, tích hợp camera, RFID, lưu trữ ảnh giao dịch và quản lý vé tháng/thẻ cư dân.

Dự án này nhằm mục tiêu mô phỏng một hệ thống bãi đỗ xe hiện đại với các chức năng chính như:
- nhận diện xe thông qua thẻ RFID,
- theo dõi xe vào/ra bằng camera,
- lưu trữ ảnh giao dịch và thông tin đăng ký vé tháng,
- hỗ trợ tra cứu lịch sử xe và quản trị dữ liệu.

---

## 1. Tổng quan dự án

Smart Parking được thiết kế như một phần mềm quản lý bãi xe cho môi trường trường học, khu văn phòng hoặc cơ sở có nhu cầu theo dõi xe ra vào một cách tự động và có tổ chức.

Hệ thống gồm 2 vai trò chính:
- Admin: quản lý cấu hình hệ thống, dữ liệu, camera, lưu trữ và bảng dữ liệu.
- Guard: giám sát xe vào/ra, xử lý giao dịch, đăng ký thành viên vé tháng và kiểm soát xe ra cổng.

---

## 2. Tính năng chính

### Quản lý đăng nhập và phân quyền
- Hỗ trợ đăng nhập cho vai trò Admin và Guard.
- Mật khẩu và vai trò được lưu trong hệ thống cơ sở dữ liệu.

### Quản lý xe vào/ra
- Nhận dữ liệu từ thiết bị RFID khi thẻ được quẹt.
- Ghi nhận thời gian xe vào/ra và lưu trữ hình ảnh giao dịch.
- Hỗ trợ xử lý xe vé tháng và xe lượt.

### Camera và hình ảnh
- Tích hợp camera qua RTSP để hiển thị hình ảnh trực tiếp từ các cổng vào/ra.
- Chụp ảnh snapshot cho mỗi giao dịch vào/ra.
- Lưu trữ hình ảnh nén để tiết kiệm dung lượng.

### Quản lý vé tháng
- Đăng ký thông tin thành viên vé tháng.
- Lưu trữ ảnh chân dung và ảnh xe.
- Gắn thẻ RFID với thông tin thành viên.

### Tra cứu lịch sử và dữ liệu
- Tìm kiếm giao dịch theo biển số, thời gian, mã thẻ RFID hoặc mã thành viên.
- Xem thông tin xe đang ở trong bãi và lịch sử đã ra.

### Quản lý cấu hình hệ thống
- Cấu hình thông tin camera và kết nối RTSP.
- Cấu hình lưu trữ dữ liệu ảnh theo thời hạn.
- Quản trị dữ liệu bằng giao diện CRUD trực tiếp trên nhiều bảng.

---

## 3. Công nghệ sử dụng

Dự án được xây dựng bằng các công nghệ sau:
- Ngôn ngữ: C#
- Framework: .NET 9 WinForms
- Cơ sở dữ liệu: PostgreSQL
- Thư viện ảnh và xử lý video: Emgu.CV
- Giao tiếp thiết bị: System.IO.Ports (RFID via COM Port)
- Quản lý file ảnh: lưu trữ dưới dạng file nén và thư mục ImageData

---

## 4. Cấu trúc thư mục chính

```text
SmartParking/
├── AdminForm.cs / AdminForm.Designer.cs
├── GuardForm.cs / GuardForm.Designer.cs
├── LoginForm.cs / LoginForm.Designer.cs
├── SubscriptionRegistrationForm.cs
├── SearchVehicleForm.cs
├── CameraService.cs
├── DatabaseHelper.cs
├── RFIDReader.cs
├── FileStorageManager.cs
├── ExceptionManager.cs
├── Program.cs
├── SmartParking.csproj
└── README.md
```

Các lớp chính trong dự án bao gồm:
- Form đăng nhập và giao diện người dùng.
- CameraService: điều khiển và hiển thị luồng camera.
- RFIDReader: đọc dữ liệu từ cổng COM của thiết bị RFID.
- DatabaseHelper: tạo và quản lý cơ sở dữ liệu PostgreSQL.
- FileStorageManager: lưu trữ và nén ảnh giao dịch/thành viên.

---

## 5. Yêu cầu hệ thống

### Phần mềm
- Windows 10/11
- Visual Studio 2022 hoặc phiên bản hỗ trợ .NET 9
- .NET SDK 9.0
- PostgreSQL Server

### Thiết bị hỗ trợ
- Camera IP hoặc camera hỗ trợ RTSP
- Thiết bị RFID đọc thẻ qua cổng COM (tùy chọn nếu muốn dùng đầy đủ chức năng quét thẻ)

---

## 6. Cài đặt và chạy dự án

### Bước 1: Cài đặt môi trường
- Cài đặt Visual Studio 2022 với workload .NET desktop development.
- Cài đặt PostgreSQL và tạo database tên `smart_parking`.
- Cài đặt .NET SDK 9.0.

### Bước 2: Cấu hình database
Dự án sẽ tự động tạo các bảng cần thiết khi chạy lần đầu. Bạn có thể cấu hình thông tin kết nối bằng file `.env` ở thư mục gốc dự án.

Ví dụ nội dung `.env`:

```env
SQL_HOST=localhost
SQL_PORT=5432
SQL_USER=postgres
SQL_PASSWORD=12345678
SQL_DATABASE=smart_parking
```

### Bước 3: Restore package và build
Mở terminal tại thư mục dự án và chạy:

```powershell
dotnet restore
dotnet build
```

### Bước 4: Chạy ứng dụng
```powershell
dotnet run
```

Hoặc mở file dự án trong Visual Studio và chạy bằng phím F5.

---

## 7. Tài khoản mặc định

Khi khởi tạo database lần đầu, hệ thống sẽ tự động tạo các tài khoản mặc định:
- Admin:
  - Username: `admin`
  - Password: `admin123`
- Guard:
  - Username: `guard`
  - Password: `guard123`

---

## 8. Ứng dụng mẫu và luồng hoạt động

1. Người dùng đăng nhập vào hệ thống.
2. Bảo vệ quét thẻ RFID hoặc nhập thông tin thủ công.
3. Hệ thống xác định xe vào hoặc ra cổng.
4. Ảnh camera được ghi nhận và lưu lại cùng dữ liệu giao dịch.
5. Admin có thể xem và quản lý toàn bộ dữ liệu thông qua giao diện quản trị.

---

## 9. Điểm nổi bật của dự án

- Giao diện desktop thân thiện và trực quan.
- Hỗ trợ nghiệp vụ quản lý bãi xe thực tế.
- Tích hợp cả phần cứng (camera, RFID) và phần mềm.
- Có thể mở rộng cho các hệ thống bãi xe thông minh hơn như nhận diện biển số, nhận diện khuôn mặt hoặc tích hợp cloud.

---

## 10. Ghi chú

Dự án này đang tập trung vào mô hình quản lý bãi xe nội bộ, phù hợp cho việc học tập, demo và phát triển thêm các tính năng nâng cao trong tương lai.

Nếu bạn muốn, tôi có thể tiếp tục hỗ trợ bạn viết thêm:
- phiên bản README chuyên nghiệp hơn theo chuẩn GitHub,
- phần hướng dẫn cài đặt chi tiết bằng tiếng Anh,
- hoặc tài liệu kiến trúc hệ thống cho dự án này.
