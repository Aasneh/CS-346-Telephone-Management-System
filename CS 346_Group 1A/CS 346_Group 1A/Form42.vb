Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class Form42

    Dim Mysqlconn As New MySqlConnection
    Dim sqlCmd As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlDt As New DataTable
    Dim Dta As New MySqlDataAdapter
    Dim SqlQuery As String
    


    Public current_user As String = "Form42"

    Private Sub Form42_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub RectangleShape1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RectangleShape1.Click

    End Sub

    Public Shared Function HashPassword(ByVal password As String) As String
        Using sha256 As SHA256 = sha256.Create()
            Dim hashedBytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Dim builder As New StringBuilder()

            For Each b As Byte In hashedBytes
                builder.Append(b.ToString("x2"))
            Next

            Return builder.ToString()
        End Using
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim enteredEmail As String
        Dim enteredPassword As String
        sqlDt.Clear()
        enteredEmail = TextBox1.Text.Trim() ' Assuming TextBox1 is used for email input
        enteredPassword = TextBox2.Text.Trim() ' Assuming TextBox2 is used for password input

        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"
        Mysqlconn.Open()
        Try
            If enteredEmail = "" Then
                MessageBox.Show("Please Enter Email ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Mysqlconn.Close()
                Return
                Exit Try
            End If
            If Not enteredEmail.EndsWith("@iitg.ac.in", StringComparison.OrdinalIgnoreCase) Then
                MessageBox.Show("Username must end with @iitg.ac.in")
                Mysqlconn.Close()
                Return
                Exit Try
            End If
            If enteredPassword = "" Then
                MessageBox.Show("Please Enter Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Mysqlconn.Close()
                Return
                Exit Try
            End If

            

            Dim hashedPassword As String = HashPassword(enteredPassword)

            sqlCmd.Connection = Mysqlconn
            sqlCmd.CommandText = "SELECT * FROM userData WHERE IITG_email = '" & enteredEmail & "'"
            'sqlCmd.Parameters.AddWithValue("@EnteredEmail", enteredEmail)

            sqlRd = sqlCmd.ExecuteReader()
            sqlDt.Load(sqlRd)

            Dim rowCount As Integer = sqlDt.Rows.Count
            Dim columnCount As Integer = sqlDt.Columns.Count

            If rowCount = 0 Then
                MessageBox.Show("You have not registered yet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                sqlRd.Close()
                'DataGridView1.DataSource = sqlDt
                Mysqlconn.Close() ' Close the connection after using it
                Return
            ElseIf rowCount > 1 Then
                MessageBox.Show("An error occurred in database. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                sqlRd.Close()
                'DataGridView1.DataSource = sqlDt
                Mysqlconn.Close() ' Close the connection after using it
                Return
            Else
                If sqlDt.Rows(0)(4).ToString() = hashedPassword.ToString() Then
                    'Form51.ReceivedEmail = enteredEmail
                    Form51.current_user = enteredEmail
                    Me.Hide()
                    Form51.Show()
                Else
                    MessageBox.Show("Invalid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

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

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        TextBox2.UseSystemPasswordChar = Not CheckBox1.Checked
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub
End Class