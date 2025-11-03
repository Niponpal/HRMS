# HRMS - Human Resource Management System

A modern, scalable Human Resource Management System built with ASP.NET Core MVC, SQL Server, and Clean Architecture.

## 🚀 Features

### Core Modules
- **Employee Management** - Complete employee lifecycle management
- **Attendance & Time Tracking** - Clock-in/clock-out with reporting
- **Leave Management** - Leave requests, approvals, and tracking
- **Payroll System** - Salary processing and payslip generation
- **Performance Management** - Employee reviews and goal tracking
- **Recruitment** - Job postings and applicant tracking
- **Dashboard & Analytics** - Real-time HR metrics and reports

### Technical Features
- Role-based access control (RBAC)
- Responsive UI with Bootstrap 5
- Real-time notifications
- Excel import/export
- Audit logging
- Email integration
- RESTful API

## 🏗️ Architecture

### Clean Architecture Layers

```
HRMS/
├── HRMS.Domain/          # Core Domain Layer
│   ├── Entities/         # Domain Entities
│   ├── Enums/           # Domain Enums
│   ├── Interfaces/      # Repository Interfaces
│   └── ValueObjects/    # Domain Value Objects
├── HRMS.Application/     # Application Layer
│   ├── Features/        # CQRS Features
│   ├── Interfaces/      # Service Interfaces
│   ├── Models/          # DTOs and ViewModels
│   └── Services/        # Application Services
├── HRMS.Infrastructure/  # Infrastructure Layer
│   ├── Data/            # DbContext and Migrations
│   ├── Identity/        # ASP.NET Core Identity
│   ├── Repositories/    # Repository Implementations
│   └── Services/        # External Services
├── HRMS.Web/            # Presentation Layer
│   ├── Controllers/     # MVC Controllers
│   ├── Views/           # Razor Views
│   └── wwwroot/         # Static Files
└── HRMS.API/            # Web API Layer (Optional)
```

## 🛠️ Technology Stack

### Backend
- **Framework**: ASP.NET Core 8.0 MVC
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server 2022
- **Authentication**: ASP.NET Core Identity
- **CQRS**: MediatR
- **Validation**: FluentValidation
- **Mapping**: AutoMapper

### Frontend
- **UI Framework**: Bootstrap 5.3
- **Icons**: Font Awesome 6.0
- **Charts**: Chart.js
- **JavaScript**: ES6+, jQuery
- **CSS**: Sass/LESS

### Tools & Libraries
- **Logging**: Serilog
- **Testing**: xUnit, Moq, Selenium
- **Documentation**: Swagger/OpenAPI
- **Caching**: Redis
- **Background Jobs**: Hangfire
- **Email**: SendGrid/MailKit

## 📋 Prerequisites

- .NET 8.0 SDK
- SQL Server 2019+ or Azure SQL
- Visual Studio 2022 or VS Code
- Node.js (for frontend build tools)

## 🚀 Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/your-organization/hrms.git
cd hrms
```

### 2. Database Setup
```sql
-- Create database
CREATE DATABASE HRMS;
GO

-- Or use EF Core migrations
dotnet ef database update --project HRMS.Infrastructure --startup-project HRMS.Web
```

### 3. Configuration
Update `appsettings.json` with your connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=HRMS;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true;"
  },
  "JwtSettings": {
    "Secret": "your-super-secret-key",
    "ExpiryMinutes": 60
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "SenderName": "HRMS",
    "SenderEmail": "noreply@hrms.com"
  }
}
```

### 4. Run Migrations
```bash
dotnet ef migrations add InitialCreate --project HRMS.Infrastructure --startup-project HRMS.Web
dotnet ef database update --project HRMS.Infrastructure --startup-project HRMS.Web
```

### 5. Seed Initial Data
```bash
dotnet run seed-data --project HRMS.Web
```

### 6. Run the Application
```bash
dotnet run --project HRMS.Web
```
Or use Visual Studio:
- Set `HRMS.Web` as startup project
- Press F5 to run

## 🗄️ Database Schema

### Core Tables
```sql
-- Employees table
CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY,
    EmployeeCode NVARCHAR(20) UNIQUE NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(256) UNIQUE NOT NULL,
    PhoneNumber NVARCHAR(20),
    DateOfBirth DATE,
    HireDate DATE NOT NULL,
    DepartmentId INT FOREIGN KEY REFERENCES Departments(Id),
    PositionId INT FOREIGN KEY REFERENCES Positions(Id),
    Salary DECIMAL(18,2),
    Status INT NOT NULL, -- Active, Inactive, Suspended
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Departments table
CREATE TABLE Departments (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    ManagerId INT FOREIGN KEY REFERENCES Employees(Id),
    IsActive BIT NOT NULL DEFAULT 1
);

-- Attendance table
CREATE TABLE Attendances (
    Id INT PRIMARY KEY IDENTITY,
    EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(Id),
    CheckIn DATETIME2 NOT NULL,
    CheckOut DATETIME2,
    TotalHours DECIMAL(5,2),
    Status INT NOT NULL, -- Present, Absent, Late, HalfDay
    Notes NVARCHAR(500),
    AttendanceDate DATE NOT NULL
);

-- Leave applications table
CREATE TABLE LeaveApplications (
    Id INT PRIMARY KEY IDENTITY,
    EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(Id),
    LeaveTypeId INT NOT NULL FOREIGN KEY REFERENCES LeaveTypes(Id),
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    TotalDays INT NOT NULL,
    Reason NVARCHAR(500),
    Status INT NOT NULL, -- Pending, Approved, Rejected
    ApprovedById INT FOREIGN KEY REFERENCES Employees(Id),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
```

## 🔧 Project Structure Details

### Domain Layer (HRMS.Domain)
```csharp
// Example Entity
public class Employee : BaseEntity
{
    public string EmployeeCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime HireDate { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public EmployeeStatus Status { get; set; }
    
    // Domain methods
    public bool IsActive() => Status == EmployeeStatus.Active;
    public string GetFullName() => $"{FirstName} {LastName}";
}
```

### Application Layer (HRMS.Application)
```csharp
// CQRS Commands
public class CreateEmployeeCommand : IRequest<Result<int>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    // ... other properties
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        // Business logic implementation
    }
}
```

### Infrastructure Layer (HRMS.Infrastructure)
```csharp
// Repository Implementation
public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;
    
    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Employee> GetByIdAsync(int id)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}
```

## 🔐 Authentication & Authorization

### Roles
- **SuperAdmin**: Full system access
- **HRManager**: HR management functions
- **DepartmentManager**: Department-specific access
- **Employee**: Self-service portal

### Policy-based Authorization
```csharp
services.AddAuthorization(options =>
{
    options.AddPolicy("HRManagement", policy =>
        policy.RequireRole("HRManager", "SuperAdmin"));
        
    options.AddPolicy("DepartmentAccess", policy =>
        policy.Requirements.Add(new DepartmentManagerRequirement()));
});
```

## 📊 API Endpoints

### Employee Management
```
GET    /api/employees          # Get all employees
GET    /api/employees/{id}     # Get employee by ID
POST   /api/employees          # Create new employee
PUT    /api/employees/{id}     # Update employee
DELETE /api/employees/{id}     # Delete employee
```

### Attendance Management
```
POST   /api/attendance/checkin     # Employee check-in
POST   /api/attendance/checkout    # Employee check-out
GET    /api/attendance/report      # Attendance report
```

## 🧪 Testing

### Unit Tests
```bash
dotnet test HRMS.Domain.Tests
dotnet test HRMS.Application.Tests
```

### Integration Tests
```bash
dotnet test HRMS.Infrastructure.Tests
```

### UI Tests
```bash
dotnet test HRMS.Web.Tests
```

## 📦 Deployment

### Development
```bash
dotnet publish -c Debug -o ./publish
```

### Production
```bash
dotnet publish -c Release -o ./publish
```

### Docker Support
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY ./publish .
ENTRYPOINT ["dotnet", "HRMS.Web.dll"]
```

## 🔄 CI/CD Pipeline

### GitHub Actions Example
```yaml
name: Build and Deploy
on:
  push:
    branches: [ main ]
jobs:
  build:
    runs-on: windows-latest
    steps:
    - uses: actions/checkout@v2
    - name: Setup .NET
      uses: actions/setup-dotnet@v1
    - name: Restore dependencies
      run: dotnet restore
    - name: Build
      run: dotnet build --no-restore
    - name: Test
      run: dotnet test --no-build --verbosity normal
    - name: Publish
      run: dotnet publish -c Release -o ./publish
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 Code Style Guide

### C# Coding Standards
- Use PascalCase for class names and methods
- Use camelCase for local variables and parameters
- Use meaningful names for variables and methods
- Follow SOLID principles
- Write unit tests for business logic

### Frontend Standards
- Use semantic HTML5
- Follow BEM methodology for CSS
- Use async/await for JavaScript
- Implement responsive design

## 🐛 Troubleshooting

### Common Issues

1. **Migration Errors**
   ```bash
   dotnet ef migrations remove --project HRMS.Infrastructure
   dotnet ef migrations add InitialCreate --project HRMS.Infrastructure
   ```

2. **Database Connection Issues**
   - Verify connection string
   - Check SQL Server service is running
   - Ensure proper authentication

3. **Identity Errors**
   - Run identity migrations
   - Check role seeding
   - Verify cookie settings

## 📞 Support

For support and questions:
- 📧 Email: support@hrms.com
- 🐛 Issues: [GitHub Issues](https://github.com/your-organization/hrms/issues)
- 📚 Documentation: [Wiki](https://github.com/your-organization/hrms/wiki)

## 📄 License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## 🙏 Acknowledgments

- ASP.NET Core Team
- Entity Framework Core Team
- Bootstrap Team
- All contributors and testers

---

**HRMS** - Streamlining Human Resource Management for Modern Organizations
