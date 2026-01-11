# Database Setup Guide for Local SQL Server

This guide will help you set up the database on your local SQL Server instance using Entity Framework Core migrations.

## Prerequisites

1. **SQL Server** must be installed on your machine (SQL Server Express, Developer, or Standard edition)
2. **SQL Server Management Studio (SSMS)** must be installed
3. **.NET SDK 8.0** must be installed (already in your project)

## Step 1: Connect to SQL Server via SSMS

1. **Open SQL Server Management Studio (SSMS)**
2. In the "Connect to Server" dialog:
   - **Server name**: `localhost` or `.` (for default instance)
     - If you have a named instance, use: `localhost\INSTANCENAME` (e.g., `localhost\SQLEXPRESS`)
   - **Authentication**: Windows Authentication (default)
   - Click **Connect**

3. **Verify Connection**: Once connected, you should see your server in the Object Explorer on the left side.

## Step 2: Verify SQL Server is Running

Before running migrations, ensure SQL Server service is running:

1. Press `Win + R`, type `services.msc`, and press Enter
2. Look for **SQL Server (MSSQLSERVER)** or **SQL Server (SQLEXPRESS)** (depending on your installation)
3. Ensure the service status is **Running**
4. If not running, right-click and select **Start**

## Step 3: Update Connection String (Already Done)

The connection string in `appsettings.json` has been updated to:
```
Server=localhost; Database=GymFeeManagement2; Trusted_Connection=True; TrustServerCertificate=True;
```

**Note**: If you're using SQL Server Express or a named instance, you may need to update the server name:
- For SQL Server Express: `Server=localhost\\SQLEXPRESS; ...`
- For a named instance: `Server=localhost\\YOURINSTANCENAME; ...`

## Step 4: Create Database Using Migrations

Open a terminal/command prompt in the project root directory and run the following commands:

### Option A: Using .NET CLI (Recommended)

```bash
# Navigate to the project directory
cd "C:\Users\User\Documents\GYM-20250128T234432Z-001\GYM\GYMFeeManagement_System-BE\GYMFeeManagement_System-BE"

# Apply all pending migrations to create/update the database
dotnet ef database update
```

### Option B: Using Package Manager Console (Visual Studio)

If you're using Visual Studio:
1. Open **Package Manager Console** (Tools → NuGet Package Manager → Package Manager Console)
2. Set the **Default project** to `GYMFeeManagement_System-BE`
3. Run:
```powershell
Update-Database
```

## Step 5: Verify Database Creation

1. **In SSMS**: Refresh the Object Explorer (right-click on your server → Refresh)
2. Expand **Databases** folder
3. You should see **GymFeeManagement2** database listed
4. Expand the database to see all the tables created by the migration

## Step 6: Adding New Migrations (If Needed)

If you make changes to your entities and need to create a new migration:

```bash
# Create a new migration
dotnet ef migrations add MigrationName

# Apply the migration to the database
dotnet ef database update
```

## Troubleshooting

### Issue: "Cannot open database" or "Login failed"

**Solution**: 
- Verify SQL Server service is running
- Check if you're using the correct server name in the connection string
- Ensure Windows Authentication is enabled for your SQL Server instance

### Issue: "A network-related or instance-specific error occurred"

**Solution**:
- Check if SQL Server Browser service is running (for named instances)
- Verify the server name in the connection string matches your SQL Server instance name
- Try using `localhost` or `.` instead of the server name

### Issue: "TrustServerCertificate" error

**Solution**: The connection string already includes `TrustServerCertificate=True`, which should resolve this. If issues persist, you may need to configure SQL Server SSL certificates.

### Issue: Connection string not working

**Solution**: Test your connection string format:
- Default instance: `Server=localhost; Database=GymFeeManagement2; Trusted_Connection=True; TrustServerCertificate=True;`
- Named instance: `Server=localhost\\SQLEXPRESS; Database=GymFeeManagement2; Trusted_Connection=True; TrustServerCertificate=True;`

## Additional Commands

### List all migrations:
```bash
dotnet ef migrations list
```

### Remove the last migration (if not applied):
```bash
dotnet ef migrations remove
```

### Generate SQL script (without applying):
```bash
dotnet ef migrations script
```

### Generate SQL script for a specific migration:
```bash
dotnet ef migrations script MigrationName
```

## Connection String Examples

### Local SQL Server (Default Instance)
```
Server=localhost; Database=GymFeeManagement2; Trusted_Connection=True; TrustServerCertificate=True;
```

### Local SQL Server Express
```
Server=localhost\\SQLEXPRESS; Database=GymFeeManagement2; Trusted_Connection=True; TrustServerCertificate=True;
```

### SQL Server with SQL Authentication
```
Server=localhost; Database=GymFeeManagement2; User Id=your_username; Password=your_password; TrustServerCertificate=True;
```

---

**Note**: After running `dotnet ef database update`, your database will be created automatically with all tables and relationships defined in your `GymDbContext`.
