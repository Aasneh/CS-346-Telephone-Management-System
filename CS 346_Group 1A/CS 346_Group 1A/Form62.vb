Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32



Public Class Form62


    Dim sqlConn As New MySqlConnection
    Dim sqlCmd As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlDt As New DataTable
    Dim DtA As New MySqlDataAdapter
    Dim sqlQuery As String


    Dim server As String = "localhost"
    Dim username As String = "root"
    Dim password As String = "Aasneh18"
    Dim database As String = "TelephoneDatabase"

    Private bitmap As Bitmap


    Private Sub updateTable()
        Try
            ' Close the connection before modifying the connection string property
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If

            ' Set the connection string property
            sqlConn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"

            ' Open the connection
            sqlConn.Open()

            sqlCmd.Connection = sqlConn
            sqlCmd.CommandText = "SELECT * FROM TelephoneDatabase.AuditTrail"

            sqlRd = sqlCmd.ExecuteReader
            sqlDt.Load(sqlRd)
            sqlRd.Close()

            ' Close the connection after use
            sqlConn.Close()

            ' Resize columns to fill the DataGridView width
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            'DataGridView1.Columns("Details").Width = 200 ' Set the width as per your requirement


            DataGridView1.DataSource = sqlDt


            DataGridView1.Columns(0).Width = 20
            DataGridView1.Columns(1).Width = 60
            DataGridView1.Columns(2).Width = 60
            DataGridView1.Columns(3).Width = 90
            DataGridView1.Columns(4).Width = 300
            DataGridView1.Columns(5).Width = 150

        Catch ex As Exception
            MessageBox.Show("Error updating table: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        updateTable()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()

    End Sub
End Class