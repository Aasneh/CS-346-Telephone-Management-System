Imports MySql.Data.MySqlClient
Public Class Form13

    Dim Mysqlconn As New MySqlConnection
    Dim sqlCmd As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlRd2 As MySqlDataReader
    Dim sqlDt As New DataTable
    Dim Dta As New MySqlDataAdapter
    Dim SqlQuery As String

    Dim server As String = "localhost"
    Dim username As String = "root"
    Dim password As String = "Aasneh18"
    Dim database As String = "telephonedatabase"

    Public current_user As String = "a.abhi@iitg.ac.in"
    Public pay_amount As String = "Rs40"
    Public plan_id As Integer = 1
    Public current_issue_id As Integer

    Private Sub updateTable()
        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            DataGridView1.Rows.Remove(row)
        Next
        Try
            ' Close the connection before modifying the connection string property
            If Mysqlconn.State = ConnectionState.Open Then
                Mysqlconn.Close()
            End If

            ' Set the connection string property
            Mysqlconn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"

            ' Open the connection
            Mysqlconn.Open()

            sqlCmd.Connection = Mysqlconn
            sqlCmd.CommandText = "SELECT Issue FROM telephonedatabase.IssueTable where PerformedBy = '" & current_user & "';"

            sqlRd = sqlCmd.ExecuteReader
            sqlDt.Load(sqlRd)
            sqlRd.Close()

            ' Close the connection after use
            Mysqlconn.Close()

            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.DataSource = sqlDt

            'DataGridView1.Columns(0).Width = 60
            'DataGridView1.Columns(1).Width = 100
            'DataGridView1.Columns(2).Width = 10
            'DataGridView1.Columns(3).Width = 200

        Catch ex As Exception
            MessageBox.Show("Error updating table: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Form13_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim count As Integer = 0

        'If Mysqlconn.State = ConnectionState.Open Then
        'Mysqlconn.Close()
        ' End If

        ' Set the connection string property
        'Mysqlconn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"

        ' Open the connection
        'Mysqlconn.Open()

        'sqlCmd.Connection = Mysqlconn
        'sqlCmd.CommandText = "Select * FROM TelephoneDatabase.IssueTable where PerformedBy = '" & current_user & "'"

        'sqlRd = sqlCmd.ExecuteReader
        'sqlDt.Load(sqlRd)
        'While sqlRd.Read()
        'count = count + 1
        'End While
        'sqlRd.Close()


        ' Close the connection after use
        'Mysqlconn.Close()

        updateTable()

        TextBox1.Text = ""
        'If count > 0 Then
        'MessageBox.Show("Please wait before your earlier issue gets resolved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Form51.current_user = current_user
        'Me.Hide()
        'Form51.Show()
        'End If

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim issue As String = TextBox1.Text

        If issue.Length() > 0 Then

            If Mysqlconn.State = ConnectionState.Open Then
                Mysqlconn.Close()
            End If

            ' Set the connection string property
            Mysqlconn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"

            ' Open the connection
            Mysqlconn.Open()

            sqlCmd.Connection = Mysqlconn
            sqlCmd.CommandText = "Insert into IssueTable(PerformedBy,Issue,ActionTimestamp) values ('" & current_user & "','" & issue & "',NOW());"

            sqlRd = sqlCmd.ExecuteReader

            sqlRd.Close()
            Mysqlconn.Close()

            MessageBox.Show("Query sent successfully")
            Form51.current_user = current_user
            Me.Close()
            Form51.Show()
        Else
            MessageBox.Show("Enter a Query!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        'For Each row As DataGridViewRow In DataGridView1.SelectedRows
        'DataGridView1.Rows.Remove(row)
        'Next
        'updateTable()

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub BackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackButton.Click
        Form51.current_user = current_user
        Me.Close()
        Form51.Show()
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub
End Class