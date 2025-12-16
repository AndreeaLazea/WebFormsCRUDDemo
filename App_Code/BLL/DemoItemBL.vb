Imports Microsoft.VisualBasic
Imports System.Text.RegularExpressions

Public Class DemoItemBL

    Public Shared Function GetAllItems() As List(Of DemoItem)
        Try
            Return DemoItemDAL.GetAllItems()
        Catch ex As Exception
            Throw New Exception("Error retrieving items", ex)
        End Try
    End Function

    Public Shared Function GetItemById(itemId As Integer) As DemoItem
        If itemId <= 0 Then
            Throw New ArgumentException(Constants.ERROR_INVALID_ID)
        End If

        Try
            Return DemoItemDAL.GetItemById(itemId)
        Catch ex As Exception
            Throw New Exception("Error retrieving item", ex)
        End Try
    End Function

    Public Shared Function InsertItem(title As String, description As String, ByRef errorMessage As String) As Boolean
        errorMessage = String.Empty

        ' Validate input
        If Not ValidateItem(title, description, errorMessage) Then
            Return False
        End If

        ' Sanitize input
        Dim sanitizedTitle As String = SanitizeInput(title)
        Dim sanitizedDescription As String = SanitizeInput(description)

        Try
            Return DemoItemDAL.InsertItem(sanitizedTitle, sanitizedDescription)
        Catch ex As Exception
            errorMessage = "Error inserting item: " & ex.Message
            Return False
        End Try
    End Function

    Public Shared Function UpdateItem(itemId As Integer, title As String, description As String, ByRef errorMessage As String) As Boolean
        errorMessage = String.Empty

        ' Validate item ID
        If itemId <= 0 Then
            errorMessage = Constants.ERROR_INVALID_ID
            Return False
        End If

        ' Validate input
        If Not ValidateItem(title, description, errorMessage) Then
            Return False
        End If

        ' Sanitize input
        Dim sanitizedTitle As String = SanitizeInput(title)
        Dim sanitizedDescription As String = SanitizeInput(description)

        Try
            Return DemoItemDAL.UpdateItem(itemId, sanitizedTitle, sanitizedDescription)
        Catch ex As Exception
            errorMessage = "Error updating item: " & ex.Message
            Return False
        End Try
    End Function

    Public Shared Function DeleteItem(itemId As Integer, ByRef errorMessage As String) As Boolean
        errorMessage = String.Empty

        If itemId <= 0 Then
            errorMessage = Constants.ERROR_INVALID_ID
            Return False
        End If

        Try
            Return DemoItemDAL.DeleteItem(itemId)
        Catch ex As Exception
            errorMessage = "Error deleting item: " & ex.Message
            Return False
        End Try
    End Function

    Private Shared Function ValidateItem(title As String, description As String, ByRef errorMessage As String) As Boolean
        ' Title is required
        If String.IsNullOrWhiteSpace(title) Then
            errorMessage = Constants.ERROR_TITLE_REQUIRED
            Return False
        End If

        ' Title length validation
        Dim titleLength As Integer = title.Trim().Length
        If titleLength < Constants.TITLE_MIN_LENGTH Then
            errorMessage = Constants.ERROR_TITLE_TOO_SHORT
            Return False
        End If

        If titleLength > Constants.TITLE_MAX_LENGTH Then
            errorMessage = Constants.ERROR_TITLE_TOO_LONG
            Return False
        End If

        ' Description length validation (optional field)
        If Not String.IsNullOrEmpty(description) AndAlso description.Length > Constants.DESCRIPTION_MAX_LENGTH Then
            errorMessage = Constants.ERROR_DESCRIPTION_TOO_LONG
            Return False
        End If

        Return True
    End Function

    Private Shared Function SanitizeInput(input As String) As String
        If String.IsNullOrEmpty(input) Then
            Return String.Empty
        End If

        ' Trim whitespace and remove control characters
        Dim sanitized As String = input.Trim()
        sanitized = Regex.Replace(sanitized, "[\x00-\x08\x0B\x0C\x0E-\x1F]", String.Empty)
        Return sanitized
    End Function

End Class

