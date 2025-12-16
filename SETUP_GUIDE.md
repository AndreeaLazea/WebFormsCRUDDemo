# Setup Guide

Quick guide to get the demo running on your machine.

## Prerequisites

- Visual Studio 2019 or later
- SQL Server 2012+ (Express is fine)
- .NET Framework 4.8

## Step 1: Database Setup (5 minutes)

1. Open SQL Server Management Studio
2. Create a database or use an existing one
3. Run the script: `SQL/CreateTable.sql`
4. Verify it worked:
   ```sql
   SELECT * FROM DemoItems
   ```

You should see 3 sample rows.

## Step 2: Configure Connection String (2 minutes)

Open `Web.config` and update the connection string:

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

Replace:
- `localhost` with your server name if different
- `CRUDDemoDB` with your actual database name (recommended to keep as CRUDDemoDB)

## Step 3: Open in Visual Studio (1 minute)

1. File → Open → Web Site
2. Select the `/exam` folder
3. Wait for solution to load

## Step 4: Build & Run (1 minute)

1. Press Ctrl + Shift + B to build
2. Check for any errors in Error List
3. Press F5 to run
4. Browser will open automatically
5. Navigate to `Pages/DemoItems.aspx`

## Testing the App

### Test Create
1. Fill in "Title" field (required)
2. Optionally fill "Description"
3. Click "Save Item"
4. Should see success message and item appears in grid

### Test Edit
1. Click "Edit" button on any row
2. Modify the fields directly in the grid
3. Click "Update" to save
4. Or click "Cancel" to discard changes

### Test Delete
1. Click "Delete" button
2. Confirm the popup
3. Item should disappear from grid

### Test Validation
1. Try submitting empty title → should fail
2. Try title with 2 characters → should fail
3. Try very long title (>200 chars) → should fail

## Troubleshooting

### Cannot open database
**Problem**: Connection string is wrong or database doesn't exist  
**Fix**: Double-check database name and server name in Web.config

### Login failed for user
**Problem**: SQL Server authentication issue  
**Fix**: 
- Make sure SQL Server allows your auth method (Windows or SQL)
- Check username/password if using SQL Auth
- Verify SQL Server is running

### Build errors
**Problem**: Missing references or wrong framework version  
**Fix**:
- Clean solution: Build → Clean Solution
- Rebuild: Build → Rebuild Solution  
- Check .NET Framework 4.8 is installed

### GridView is empty
**Problem**: No data or connection issue  
**Fix**:
- Run the SQL script again to insert sample data
- Check browser console (F12) for JavaScript errors
- Verify connection string is correct

### Port already in use
**Problem**: Another app using the port  
**Fix**: Stop IIS Express from system tray, or restart Visual Studio

## Project URLs

After starting with F5:
- Home: `http://localhost:port/Default.aspx`
- CRUD Page: `http://localhost:port/Pages/DemoItems.aspx`

(Port is auto-assigned by Visual Studio)

## Tips

- Use F5 to run with debugging
- Use Ctrl+F5 to run without debugging (faster)
- Set breakpoints in code-behind with F9
- Step through code with F10 (step over) or F11 (step into)
- Check "Output" window for build messages
- Check "Error List" window for compilation errors

## Time Required

- First-time setup: ~10 minutes
- Subsequent runs: ~1 minute

---

That's it! You should now have the app running.
