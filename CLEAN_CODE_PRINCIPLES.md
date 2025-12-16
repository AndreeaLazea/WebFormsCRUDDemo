# Clean Code Principles

## Architecture

**Three-tier separation**: DAL handles database access, BLL handles validation and business logic, UI handles presentation.

**Single Responsibility**: Each class has one clear purpose (DemoItemDAL for data access, DemoItemBL for business logic, Constants for application constants).

## Code Quality

**DRY Principle**: Constants centralized in `Constants.vb`, validation logic in BLL, reusable helper methods.

**Meaningful Names**: Methods use verbs (`GetAllItems`, `InsertItem`), classes use nouns (`DemoItem`, `DemoItemDAL`).

**Small Methods**: Most methods under 30 lines, focused on single task.

## Security

**SQL Injection Prevention**: All queries use parameterized commands with SqlParameter.

**Input Validation**: Multi-layer validation (client-side JavaScript, server-side ASP.NET validators, BLL validation).

**Input Sanitization**: Trim whitespace, remove control characters using regex.

**Error Handling**: Try-catch blocks at each layer, no sensitive data in error messages.

## Code Organization

**Regions**: Logical grouping of related methods (Page Events, Data Binding, Button Events, GridView Events, Validation, Helper Methods).

**File Organization**: Clear folder structure (Models, DAL, BLL, Pages, SQL).

## Testability

**Separation enables testing**: DAL can be mocked, BLL can be tested without database, static methods easily testable.
