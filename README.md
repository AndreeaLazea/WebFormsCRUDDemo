# Demo CRUD WebForms Application

ASP.NET WebForms CRUD application with VB.NET, three-tier architecture, Bootstrap 5 UI, and SQL Server.

## Features

- Complete CRUD operations
- Three-tier architecture (DAL/BLL/UI)
- Client and server-side validation
- Parameterized SQL queries

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
/WebFormsCRUDDemo
├── SQL/
│   └── CreateTable.sql
├── App_Code/
│   ├── Models/
│   │   └── DemoItem.vb
│   ├── DAL/
│   │   └── DemoItemDAL.vb
│   ├── BLL/
│   │   └── DemoItemBL.vb
│   └── Common/
│       └── Constants.vb
├── Pages/
│   ├── DemoItems.aspx
│   └── DemoItems.aspx.vb
├── Web.config
├── Default.aspx
└── README.md
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

## Implementation Details

**CRUD Operations**: GridView with inline editing, form-based insert/update, JavaScript delete confirmation

**Validation**: Client-side (JavaScript) and server-side (ASP.NET validators + BLL validation)

**Security**: Parameterized SQL queries, input sanitization, error handling at all layers

## Database Schema

```sql
CREATE TABLE DemoItems (
    ItemID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NULL,
    CreatedOn DATETIME DEFAULT GETDATE() NOT NULL
)
```

## Project Layers

**DAL** (`App_Code/DAL/DemoItemDAL.vb`): Database operations with parameterized queries

**BLL** (`App_Code/BLL/DemoItemBL.vb`): Input validation, sanitization, business rules

**UI** (`Pages/DemoItems.aspx`): GridView with inline editing, form controls, validation

## Troubleshooting

**Can't connect to database**: Verify connection string and SQL Server is running

**Login failed**: Check authentication mode and credentials

**Build errors**: Clean and rebuild solution, verify .NET Framework 4.8 is installed

**GridView not displaying**: Verify SQL script ran successfully and check browser console

## Technology Stack

VB.NET (.NET Framework 4.8), ASP.NET WebForms, SQL Server 2012+, Bootstrap 5.3.0, ADO.NET
