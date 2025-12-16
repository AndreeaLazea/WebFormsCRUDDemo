# Project Structure

## Directory Structure

```
/WebFormsCRUDDemo
├── DemoCRUD.sln
├── Web.config
├── Default.aspx
├── Error.aspx
├── README.md
├── SETUP_GUIDE.md
├── CLEAN_CODE_PRINCIPLES.md
├── PROJECT_STRUCTURE.md
│
├── SQL/
│   └── CreateTable.sql
│
├── App_Code/
│   ├── Models/
│   │   └── DemoItem.vb
│   ├── DAL/
│   │   └── DemoItemDAL.vb
│   ├── BLL/
│   │   └── DemoItemBL.vb
│   └── Common/
│       └── Constants.vb
│
└── Pages/
    ├── DemoItems.aspx
    └── DemoItems.aspx.vb
```

## Key Files

**DemoItem.vb**: Entity model representing the DemoItems table

**DemoItemDAL.vb**: Data access layer with CRUD operations using parameterized queries

**DemoItemBL.vb**: Business logic layer with validation, sanitization, and error handling

**Constants.vb**: Application constants (validation rules, error messages)

**DemoItems.aspx**: Main CRUD page with GridView and form controls

**DemoItems.aspx.vb**: Code-behind with event handlers for GridView operations

**CreateTable.sql**: Database schema and sample data

**Web.config**: Application configuration including connection string

## Architecture Flow

```
User Input (UI)
    ↓
DemoItems.aspx.vb (Presentation)
    ↓
DemoItemBL.vb (Business Logic - Validation)
    ↓
DemoItemDAL.vb (Data Access - Parameterized SQL)
    ↓
SQL Server Database
```

## Layer Responsibilities

**Presentation Layer**: Handle user interactions, display data, client-side validation

**Business Logic Layer**: Validate input, sanitize data, enforce business rules

**Data Access Layer**: Execute SQL queries, manage connections, map data to objects
