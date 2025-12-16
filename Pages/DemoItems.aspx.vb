Imports System.Data
Imports System.Web.UI.WebControls

''' <summary>
''' Code-behind for DemoItems.aspx
''' Handles GridView CRUD operations and form events
''' </summary>
Partial Class DemoItems
    Inherits System.Web.UI.Page

#Region "Page Events"

    ''' <summary>
    ''' Page Load event - Binds data on initial load
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub

#End Region

#Region "Data Binding"

    ''' <summary>
    ''' Binds the GridView with data from the BLL
    ''' </summary>
    Private Sub BindGrid()
        Try
            Dim items As List(Of DemoItem) = DemoItemBL.GetAllItems()
            gvItems.DataSource = items
            gvItems.DataBind()
        Catch ex As Exception
            ShowError(Constants.ERROR_LOAD_FAILED & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Button Events"

    ''' <summary>
    ''' Save button click - Handles both Insert and Update operations
    ''' </summary>
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If Not Page.IsValid Then
            Return
        End If

        Dim errorMessage As String = String.Empty
        Dim success As Boolean = False
        Dim itemId As Integer = Convert.ToInt32(hdnEditItemID.Value)

        Try
            If itemId = 0 Then
                ' Insert new item
                success = DemoItemBL.InsertItem(txtTitle.Text.Trim(), txtDescription.Text.Trim(), errorMessage)
                
                If success Then
                    ShowSuccess(Constants.SUCCESS_ITEM_ADDED)
                    ClearForm()
                Else
                    ShowError(If(String.IsNullOrEmpty(errorMessage), Constants.ERROR_INSERT_FAILED, errorMessage))
                End If
            Else
                ' Update existing item
                success = DemoItemBL.UpdateItem(itemId, txtTitle.Text.Trim(), txtDescription.Text.Trim(), errorMessage)
                
                If success Then
                    ShowSuccess(Constants.SUCCESS_ITEM_UPDATED)
                    ClearForm()
                Else
                    ShowError(If(String.IsNullOrEmpty(errorMessage), Constants.ERROR_UPDATE_FAILED, errorMessage))
                End If
            End If

            If success Then
                BindGrid()
            End If

        Catch ex As Exception
            ShowError("An error occurred: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Cancel button click - Clears the form
    ''' </summary>
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        ClearForm()
    End Sub

#End Region

#Region "GridView Events"

    ''' <summary>
    ''' GridView RowEditing event - Switches to edit mode
    ''' </summary>
    Protected Sub gvItems_RowEditing(sender As Object, e As GridViewEditEventArgs)
        gvItems.EditIndex = e.NewEditIndex
        BindGrid()
        HideMessages()
    End Sub

    ''' <summary>
    ''' GridView RowCancelingEdit event - Cancels edit mode
    ''' </summary>
    Protected Sub gvItems_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvItems.EditIndex = -1
        BindGrid()
        HideMessages()
    End Sub

    ''' <summary>
    ''' GridView RowUpdating event - Updates an item inline
    ''' </summary>
    Protected Sub gvItems_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Try
            Dim row As GridViewRow = gvItems.Rows(e.RowIndex)
            Dim itemId As Integer = Convert.ToInt32(gvItems.DataKeys(e.RowIndex).Value)
            
            ' Get the edited values from the GridView
            Dim txtEditTitle As TextBox = CType(row.FindControl("txtEditTitle"), TextBox)
            Dim txtEditDescription As TextBox = CType(row.FindControl("txtEditDescription"), TextBox)
            
            If txtEditTitle IsNot Nothing Then
                Dim errorMessage As String = String.Empty
                Dim success As Boolean = DemoItemBL.UpdateItem(itemId, txtEditTitle.Text.Trim(), txtEditDescription.Text.Trim(), errorMessage)
                
                If success Then
                    ShowSuccess(Constants.SUCCESS_ITEM_UPDATED)
                    gvItems.EditIndex = -1
                    BindGrid()
                Else
                    ShowError(If(String.IsNullOrEmpty(errorMessage), Constants.ERROR_UPDATE_FAILED, errorMessage))
                End If
            End If

        Catch ex As Exception
            ShowError(Constants.ERROR_UPDATE_FAILED & ": " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' GridView RowDeleting event - Deletes an item
    ''' </summary>
    Protected Sub gvItems_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Try
            Dim itemId As Integer = Convert.ToInt32(gvItems.DataKeys(e.RowIndex).Value)
            Dim errorMessage As String = String.Empty
            Dim success As Boolean = DemoItemBL.DeleteItem(itemId, errorMessage)
            
            If success Then
                ShowSuccess(Constants.SUCCESS_ITEM_DELETED)
                BindGrid()
            Else
                ShowError(If(String.IsNullOrEmpty(errorMessage), Constants.ERROR_DELETE_FAILED, errorMessage))
            End If

        Catch ex As Exception
            ShowError(Constants.ERROR_DELETE_FAILED & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Validation"

    ''' <summary>
    ''' Server-side validation for title length
    ''' </summary>
    Protected Sub ValidateTitleLength(source As Object, args As ServerValidateEventArgs)
        If String.IsNullOrWhiteSpace(args.Value) Then
            args.IsValid = False
        Else
            args.IsValid = (args.Value.Trim().Length >= Constants.TITLE_MIN_LENGTH)
        End If
    End Sub

#End Region

#Region "Helper Methods"

    ''' <summary>
    ''' Clears the form and resets to insert mode
    ''' </summary>
    Private Sub ClearForm()
        txtTitle.Text = String.Empty
        txtDescription.Text = String.Empty
        hdnEditItemID.Value = "0"
        lblFormTitle.Text = "Add New Item"
        btnSave.Text = "Save Item"
        btnCancel.Visible = False
        HideMessages()
    End Sub

    ''' <summary>
    ''' Displays a success message
    ''' </summary>
    Private Sub ShowSuccess(message As String)
        pnlSuccess.Visible = True
        lblSuccess.Text = message
        pnlError.Visible = False
    End Sub

    ''' <summary>
    ''' Displays an error message
    ''' </summary>
    Private Sub ShowError(message As String)
        pnlError.Visible = True
        lblError.Text = message
        pnlSuccess.Visible = False
    End Sub

    ''' <summary>
    ''' Hides all messages
    ''' </summary>
    Private Sub HideMessages()
        pnlSuccess.Visible = False
        pnlError.Visible = False
    End Sub

#End Region

End Class

