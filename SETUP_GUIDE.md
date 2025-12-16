# Setup Guide

## Prerequisites

- Visual Studio 2019+ (not VS Code)
- SQL Server 2012+
- .NET Framework 4.8

## Database Setup

1. Open SQL Server Management Studio
2. Create a database or use an existing one
3. Run the script: `SQL/CreateTable.sql`
4. Verify it worked:
   ```sql
   SELECT * FROM DemoItems
   ```

You should see 3 sample rows.

## Configure Connection String

Update `Web.config` with your database details:

**Windows Authentication (recommended for local dev):**
```xml
<add name="DemoConnectionString" 
     connectionString="Data Source=localhost;Initial Catalog=YourDatabaseName;Integrated Security=True;" 
     providerName="System.Data.SqlClient"/>
```

**SQL Server Authentication:**
```xml
<add name="DemoConnectionString" 
     connectionString="Data Source=localhost;Initial Catalog=YourDatabaseName;User ID=sa;Password=yourpassword;" 
     providerName="System.Data.SqlClient"/>
```

## Build & Run

1. Open `DemoCRUD.sln` in Visual Studio
2. Press Ctrl + Shift + B to build
3. Press F5 to run
4. Navigate to `Pages/DemoItems.aspx`

## Testing

- Insert new items (Title required, 3-200 chars)
- Edit items inline in GridView
- Delete items with confirmation
- Test validation with empty/short/long titles

## Troubleshooting

**Cannot open database**: Verify connection string and database name in Web.config

**Login failed**: Check SQL Server authentication mode and credentials

**Build errors**: Clean and rebuild solution, verify .NET Framework 4.8 is installed

**GridView empty**: Verify SQL script ran successfully, check connection string
