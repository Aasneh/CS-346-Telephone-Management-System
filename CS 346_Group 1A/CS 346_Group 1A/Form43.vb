Imports MySql.Data.MySqlClient
Public Class Form43
    Dim Mysqlconn As New MySqlConnection
    Dim sqlCmd As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlDt As New DataTable
    Dim Dta As New MySqlDataAdapter
    Dim SqlQuery As String

    Public current_user As String
    Private Sub Form43_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub RectangleShape1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RectangleShape1.Click, RectangleShape2.Click

    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim enteredEmail As String = TextBox1.Text ' Assuming TextBox1 is used for email input
        Dim enteredPassword As String = TextBox2.Text ' Assuming TextBox2 is used for password input
        'Form51.current_user = current_user
        'Form51.Show()
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"

        Try
            sqlDt.Clear()
            Mysqlconn.Open()
            '.Show("Connection successful")

            sqlCmd.Connection = Mysqlconn
            sqlCmd.CommandText = "SELECT * FROM AdminData WHERE username = '" & enteredEmail & "';"
            'sqlCmd.Parameters.AddWithValue("@EnteredEmail", enteredEmail)

            current_user = enteredEmail

            sqlRd = sqlCmd.ExecuteReader()
            sqlDt.Load(sqlRd)

            Dim rowCount As Integer = sqlDt.Rows.Count
            Dim columnCount As Integer = sqlDt.Columns.Count

            If rowCount = 0 Then
                MessageBox.Show("You have not registered yet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf rowCount > 1 Then
                MessageBox.Show("An error occurred in database. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                If sqlDt.Rows(0)(1).ToString() = enteredPassword.ToString() Then
                    'Form51.ReceivedEmail = enteredEmail
                    'Form61.current_user = current_user
                    Me.Hide()
                    Form61.Show()
                Else
                    MessageBox.Show("Invalid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If


            'MessageBox.Show("Number of Rows: " & rowCount.ToString() & vbCrLf & "Number of Columns: " & columnCount.ToString())

            sqlRd.Close()
            'DataGridView1.DataSource = sqlDt
            Mysqlconn.Close() ' Close the connection after using it
        Catch ex As Exception
            MessageBox.Show("Error connecting to database: " & ex.Message)
        Finally
            Mysqlconn.Dispose() ' Dispose of the connection object
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
        Form41.Show()
    End Sub

   
End Class