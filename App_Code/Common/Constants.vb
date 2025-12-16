Imports Microsoft.VisualBasic

Public Class Constants

    ' Validation Constants
    Public Const TITLE_MIN_LENGTH As Integer = 3
    Public Const TITLE_MAX_LENGTH As Integer = 200
    Public Const DESCRIPTION_MAX_LENGTH As Integer = 1000

    ' Error Messages
    Public Const ERROR_TITLE_REQUIRED As String = "Title is required"
    Public Const ERROR_TITLE_TOO_SHORT As String = "Title must be at least 3 characters long"
    Public Const ERROR_TITLE_TOO_LONG As String = "Title cannot exceed 200 characters"
    Public Const ERROR_DESCRIPTION_TOO_LONG As String = "Description cannot exceed 1000 characters"
    Public Const ERROR_INVALID_ID As String = "Invalid Item ID"

    ' Success Messages
    Public Const SUCCESS_ITEM_ADDED As String = "Item added successfully!"
    Public Const SUCCESS_ITEM_UPDATED As String = "Item updated successfully!"
    Public Const SUCCESS_ITEM_DELETED As String = "Item deleted successfully!"

    ' Database
    Public Const CONNECTION_STRING_NAME As String = "DemoConnectionString"

End Class
