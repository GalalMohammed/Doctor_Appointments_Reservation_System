# Doctor Appointments Reservation System

## Project Overview

This “Doctor Appointments Reservation System” is a full‑stack ASP.NET MVC web application designed for booking and managing healthcare appointments, similar to popular solutions like Vezeeta. It offers patients a modern, intuitive interface for finding doctors, selecting available slots, and handling online payments. Doctors can manage schedules, and maintain profiles.

**Live Demo:**
<https://doc-net.runasp.net/>

---

## Key Features

- **Responsive Template**
  Fully responsive, Bootstrap‑based layout with a clean, user‑friendly design.

- **Authentication & Authorization**
  - ASP.NET Core Identity (register, login, email confirmation, forgot password, reset password).
  - Role‑based access (Doctor, Patient).
  - External logins (Google, Facebook, Microsoft Account).

- **Appointment Booking**
  - Browse doctors by specialty.
  - View available time slots.
  - Real‑time slot availability checks.

- **Online Payments**
  - Stripe and/or PayPal integration for secure credit‑card payments.

- **Validation & Security**
  - Server‑ and client‑side validation (DataAnnotations + jQuery unobtrusive).
  - Dependency Injection, Repository, and Service patterns for clean separation of concerns.

---

## Architecture & Design

- **Presentation Layer (ASP.NET MVC)**
  - Controllers orchestrate requests and return strongly‑typed ViewModels.
  - Razor views styled with Bootstrap 5 and custom theming.

- **Business Layer (Services/Managers)**
  - `IAppointmentManager`, `IDoctorManager`, etc., encapsulate business rules and data orchestration.

- **Data Access Layer (Repositories)**
  - EF Core code‑first DbContext and Repository pattern for maintainable, testable data access.

- **Identity Layer**
  - Custom `AppUser` extends IdentityUser; mapped to `Doctor` or `Patient` via foreign keys.

---

## Getting Started

### Prerequisites

- Visual Studio 2022 or later (with ASP.NET and web development workload)
- .NET 9 SDK
- SQL Server
- PayPal sandbox credentials
- ReCaptcha site key and secret key
- External login credentials for external services (Google, Facebook, Microsoft Account)
- Email service credentials

### Local Setup

1. **Clone Repository**

   ```bash
   git clone https://github.com/YourOrg/DoctorAppointmentSystem.git
   cd DoctorAppointmentSystem
   ```

2. **Configure Connection String**
   In `appsettings.json`, set your database and external‑login keys:

   ```jsonc
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AppointmentsDb;Trusted_Connection=True;"
   },
   "PayPalOptions": {
    "ClientId": "your-client-id", "ClientSecret": "your-client-secret", "Mode": "sandbox", "Url": "https://api-m.sandbox.paypal.com/"
   },
   "ReCaptchaSettings": {
    "SiteKey": "your-site-key", "SecretKey": "your-secret-key", "ApiUrl": "https://www.google.com/recaptcha/api/siteverify"
   },
   "Google": {
   "ClientId": "your-client-id", "ClientSecret": "your-client-secret"
   },
   "Facebook": {
    "AppId": "your-app-id", "AppSecret": "your-app-secret"
   },
   "Microsoft": {
    "ClientId": "your-client-id", "ClientSecret": "your-client-secret"
   },
   "EmailSettings": {
    "EmailAddress": "your-email@example.com", "Password": "your-email-password"
   }
   ```

3. **Add Entity Framework Core migrations**

4. **Run the Application**
   Navigate to `https://localhost:7144` to explore.

---

## Deployment

This project is currently hosted at:
**<https://doc-net.runasp.net/>**

To deploy your own instance:

1. Publish the project.
2. Ensure your production `appsettings.json` has correct connection strings, payment, ReCaptcha, Google, Facebook, Microsoft, and email settings configured properly.

---

## Contributing

1. Fork the repo
2. Create a feature branch (`git checkout -b feat/my-new-feature`)
3. Commit your changes (`git commit -m 'Add new feature'`)
4. Push to the branch (`git push origin feat/my-new-feature`)
5. Open a Pull Request

---

## Troubleshooting

- **Migrations failing?**
  Ensure `DefaultConnection` is correct and SQL Server is running.
- **External logins not working?**
  Verify OAuth credentials and redirect URIs in Google/Facebook developer consoles.
- **Stripe/PayPal errors?**
  Check sandbox keys.

---

## License

This project is licensed under the **MIT License**. See [LICENSE](LICENSE) for details.

---

## Acknowledgments

- [ASP.NET Core MVC Documentation](https://docs.microsoft.com/aspnet/core/mvc/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [PayPal API](https://developer.paypal.com/home/)
