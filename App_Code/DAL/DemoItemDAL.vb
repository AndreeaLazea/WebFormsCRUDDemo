Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class DemoItemDAL

    Private Shared ReadOnly Property ConnectionString As String
        Get
            Return ConfigurationManager.ConnectionStrings(Constants.CONNECTION_STRING_NAME).ConnectionString
        End Get
    End Property

    Public Shared Function GetAllItems() As List(Of DemoItem)
        Dim items As New List(Of DemoItem)()

        Using conn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand("SELECT ItemID, Title, Description, CreatedOn FROM DemoItems ORDER BY CreatedOn DESC", conn)
                Try
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim item As New DemoItem() With {
                                .ItemID = Convert.ToInt32(reader("ItemID")),
                                .Title = reader("Title").ToString(),
                                .Description = If(IsDBNull(reader("Description")), String.Empty, reader("Description").ToString()),
                                .CreatedOn = Convert.ToDateTime(reader("CreatedOn"))
                            }
                            items.Add(item)
                        End While
                    End Using
                Catch ex As SqlException
                    Throw New Exception("Error retrieving items from database", ex)
                End Try
            End Using
        End Using

        Return items
    End Function

    Public Shared Function GetItemById(itemId As Integer) As DemoItem
        Dim item As DemoItem = Nothing

        Using conn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand("SELECT ItemID, Title, Description, CreatedOn FROM DemoItems WHERE ItemID = @ItemID", conn)
                cmd.Parameters.AddWithValue("@ItemID", itemId)

                Try
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            item = New DemoItem() With {
                                .ItemID = Convert.ToInt32(reader("ItemID")),
                                .Title = reader("Title").ToString(),
                                .Description = If(IsDBNull(reader("Description")), String.Empty, reader("Description").ToString()),
                                .CreatedOn = Convert.ToDateTime(reader("CreatedOn"))
                            }
                        End If
                    End Using
                Catch ex As SqlException
                    Throw New Exception("Error retrieving item from database", ex)
                End Try
            End Using
        End Using

        Return item
    End Function

    Public Shared Function InsertItem(title As String, description As String) As Boolean
        Dim success As Boolean = False

        Using conn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand("INSERT INTO DemoItems (Title, Description) VALUES (@Title, @Description)", conn)
                cmd.Parameters.AddWithValue("@Title", title)
                cmd.Parameters.AddWithValue("@Description", If(String.IsNullOrEmpty(description), DBNull.Value, CObj(description)))

                Try
                    conn.Open()
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    success = (rowsAffected > 0)
                Catch ex As SqlException
                    Throw New Exception("Error inserting item into database", ex)
                End Try
            End Using
        End Using

        Return success
    End Function

    Public Shared Function UpdateItem(itemId As Integer, title As String, description As String) As Boolean
        Dim success As Boolean = False

        Using conn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand("UPDATE DemoItems SET Title = @Title, Description = @Description WHERE ItemID = @ItemID", conn)
                cmd.Parameters.AddWithValue("@ItemID", itemId)
                cmd.Parameters.AddWithValue("@Title", title)
                cmd.Parameters.AddWithValue("@Description", If(String.IsNullOrEmpty(description), DBNull.Value, CObj(description)))

                Try
                    conn.Open()
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    success = (rowsAffected > 0)
                Catch ex As SqlException
                    Throw New Exception("Error updating item in database", ex)
                End Try
            End Using
        End Using

        Return success
    End Function

    Public Shared Function DeleteItem(itemId As Integer) As Boolean
        Dim success As Boolean = False

        Using conn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand("DELETE FROM DemoItems WHERE ItemID = @ItemID", conn)
                cmd.Parameters.AddWithValue("@ItemID", itemId)

                Try
                    conn.Open()
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    success = (rowsAffected > 0)
                Catch ex As SqlException
                    Throw New Exception("Error deleting item from database", ex)
                End Try
            End Using
        End Using

        Return success
    End Function

End Class

