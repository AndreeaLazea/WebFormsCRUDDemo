# Demo CRUD WebForms Application

A clean ASP.NET WebForms CRUD application built with VB.NET, demonstrating proper three-tier architecture and clean code practices.

## What This Is

This is a demo project showing:
- Complete CRUD operations (Create, Read, Update, Delete)
- Proper separation of concerns (DAL/BLL/UI)
- Bootstrap 5 responsive UI
- Server and client-side validation
- Secure database access with parameterized queries

## Architecture

```
┌─────────────────────────────────────────┐
│        PRESENTATION LAYER (UI)          │
│                                         │
│  DemoItems.aspx + DemoItems.aspx.vb    │
│  - GridView with CRUD                   │
│  - Bootstrap 5 styling                  │
│  - Client & Server validation           │
└──────────────┬──────────────────────────┘
               │
               ↓
┌──────────────────────────────────────────┐
│      BUSINESS LOGIC LAYER (BLL)          │
│                                           │
│  DemoItemBL.vb                           │
│  - Input validation                       │
│  - Business rules                         │
│  - Data sanitization                      │
└──────────────┬───────────────────────────┘
               │
               ↓
┌──────────────────────────────────────────┐
│       DATA ACCESS LAYER (DAL)             │
│                                           │
│  DemoItemDAL.vb                          │
│  - Parameterized SQL queries              │
│  - Connection management                  │
│  - CRUD operations                        │
└──────────────┬───────────────────────────┘
               │
               ↓
┌──────────────────────────────────────────┐
│         SQL SERVER DATABASE               │
│                                           │
│  DemoItems Table                         │
│  - ItemID (PK)                           │
│  - Title                                  │
│  - Description                            │
│  - CreatedOn                              │
└───────────────────────────────────────────┘
```

## Project Structure

```
/exam
├── SQL/
│   └── CreateTable.sql         # Database creation script
│
├── App_Code/
│   ├── Models/
│   │   └── DemoItem.vb         # Entity model
│   ├── DAL/
│   │   └── DemoItemDAL.vb      # Data access layer
│   ├── BLL/
│   │   └── DemoItemBL.vb       # Business logic layer
│   └── Common/
│       └── Constants.vb        # Application constants
│
├── Pages/
│   ├── DemoItems.aspx          # Main CRUD page
│   └── DemoItems.aspx.vb       # Code-behind
│
├── Web.config                  # Configuration
├── Default.aspx                # Landing page
└── README.md                   # This file
```

## Quick Start

### 1. Setup Database

**Option A: SQL Server Management Studio (Windows)**

Run the SQL script to create the table:

```sql
-- In SQL Server Management Studio
USE CRUDDemoDB
GO

-- Run SQL/CreateTable.sql
```

**Option B: Docker Container (macOS/Linux)**

```bash
# Run SQL Server in Docker
docker run -e "ACCEPT_EULA=Y" -e 'SA_PASSWORD=YourStrong!Pass123' \
  -p 1433:1433 --name sqlserver \
  -d mcr.microsoft.com/mssql/server:2019-latest

# Wait ~30 seconds, then create database
docker exec -it sqlserver /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P 'YourStrong!Pass123' -C \
  -Q "CREATE DATABASE CRUDDemoDB;"

# Copy and run SQL script
docker cp SQL/CreateTable.sql sqlserver:/tmp/
docker exec -it sqlserver /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P 'YourStrong!Pass123' -C \
  -i /tmp/CreateTable.sql
```

The script creates the `DemoItems` table with sample data.

### 2. Configure Connection String

Update `Web.config` with your database details:

```xml
<connectionStrings>
  <add name="DemoConnectionString" 
       connectionString="Data Source=localhost;Initial Catalog=CRUDDemoDB;Integrated Security=True;" 
       providerName="System.Data.SqlClient"/>
</connectionStrings>
```

**For SQL Server Authentication (Docker):**
```xml
connectionString="Data Source=localhost,1433;Initial Catalog=CRUDDemoDB;User ID=sa;Password=YourStrong!Pass123;TrustServerCertificate=True;"
```

### 3. Open & Run

1. Open solution in Visual Studio 2019+
2. Build solution (Ctrl + Shift + B)
3. Press F5 to run
4. Navigate to `Pages/DemoItems.aspx`

## Features

### CRUD Operations

**Create**
- Form-based input with validation
- Required field checking (Title)
- Length validation (3-200 characters)
- Success/error feedback

**Read**
- GridView display with all items
- Formatted date display
- Responsive Bootstrap layout

**Update**
- Inline GridView editing
- Validation on update
- Real-time feedback

**Delete**
- JavaScript confirmation dialog
- Immediate UI refresh
- Error handling

### Validation

**Client-Side (JavaScript)**
- Real-time field validation
- Title length check (minimum 3 characters)
- Form submission prevention on errors

**Server-Side (ASP.NET)**
- Required field validators
- Custom validators
- Length constraints
- Business rule validation in BLL

### Security

**SQL Injection Prevention**
```vb
' All queries use parameterized commands
cmd.Parameters.AddWithValue("@Title", title)
cmd.Parameters.AddWithValue("@Description", description)
```

**Input Sanitization**
- Trimming whitespace
- Removing control characters
- Length validation

**Error Handling**
- Try-catch blocks at every layer
- User-friendly error messages
- No sensitive data exposed

## Database Schema

```sql
CREATE TABLE DemoItems (
    ItemID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NULL,
    CreatedOn DATETIME DEFAULT GETDATE() NOT NULL
)
```

## Technology Stack

- **Language**: VB.NET (.NET Framework 4.8)
- **Framework**: ASP.NET WebForms
- **Database**: SQL Server 2012+
- **UI**: Bootstrap 5.3.0
- **Data Access**: ADO.NET

## Key Files

### Data Access Layer
`App_Code/DAL/DemoItemDAL.vb`
- GetAllItems() - Retrieves all items
- GetItemById() - Gets single item
- InsertItem() - Creates new item
- UpdateItem() - Modifies existing item
- DeleteItem() - Removes item

All methods use parameterized queries for security.

### Business Logic Layer
`App_Code/BLL/DemoItemBL.vb`
- Validates all input
- Sanitizes data
- Enforces business rules
- Handles errors

Validation rules:
- Title: Required, 3-200 characters
- Description: Optional, max 1000 characters

### Presentation Layer
`Pages/DemoItems.aspx` + `.aspx.vb`
- Bootstrap 5 responsive UI
- GridView with inline editing
- Client + server validation
- Success/error messages

## Common Issues

**Can't connect to database**
- Check connection string in Web.config
- Verify SQL Server is running
- Check database name exists

**Login failed**
- Check SQL Server authentication mode (Windows vs SQL Auth)
- Verify credentials
- Ensure SQL Server allows remote connections

**Build errors**
- Clean solution (Build → Clean Solution)
- Rebuild (Build → Rebuild Solution)
- Check .NET Framework 4.8 is installed

**GridView not displaying**
- Verify data exists in SQL table
- Check browser console for errors
- Review error messages

## Testing Checklist

- [ ] Add new item with valid data
- [ ] Try empty title (should fail validation)
- [ ] Try title with 2 characters (should fail)
- [ ] Try title with 201 characters (should fail)
- [ ] Edit item inline
- [ ] Update item successfully
- [ ] Cancel edit
- [ ] Delete item with confirmation
- [ ] Test with special characters

## Clean Code Practices

**Separation of Concerns**
- Each layer has single responsibility
- No SQL in UI or BLL
- No business logic in DAL

**Constants Usage**
- No magic numbers
- Centralized in Constants.vb
- Easy to maintain

**Error Handling**
- Try-catch at every layer
- Meaningful error messages
- Proper exception chaining

**Naming Conventions**
- Clear, descriptive names
- Consistent throughout
- Self-documenting code

## Potential Enhancements

For production use, consider adding:
- Pagination for large datasets
- Sorting and filtering
- Search functionality
- Logging (log4net or NLog)
- Unit tests
- Authentication/Authorization
- Audit trail

## License

This is a demo project created for evaluation purposes.

---

**Built with**: Visual Studio 2019+ | SQL Server | Bootstrap 5 | VB.NET
