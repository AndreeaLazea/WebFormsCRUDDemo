Imports Microsoft.VisualBasic

''' <summary>
''' Model class representing a DemoItem entity
''' </summary>
Public Class DemoItem
    Public Property ItemID As Integer
    Public Property Title As String
    Public Property Description As String
    Public Property CreatedOn As DateTime

    Public Sub New()
        ItemID = 0
        Title = String.Empty
        Description = String.Empty
        CreatedOn = DateTime.Now
    End Sub

    Public Sub New(itemId As Integer, title As String, description As String, createdOn As DateTime)
        Me.ItemID = itemId
        Me.Title = title
        Me.Description = description
        Me.CreatedOn = createdOn
    End Sub
End Class

