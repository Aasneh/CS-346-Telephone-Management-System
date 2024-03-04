Imports MySql.Data.MySqlClient
Public Class Form12
    Dim Mysqlconn As New MySqlConnection
    Dim sqlCmd As New MySqlCommand
    Dim sqlCmd2 As New MySqlCommand
    Dim sqlCmd3 As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlRd2 As MySqlDataReader
    Dim sqlRd3 As MySqlDataReader
    Dim sqlDt As New DataTable
    Dim Dta As New MySqlDataAdapter
    Dim SqlQuery As String

    Dim server As String = "localhost"
    Dim username As String = "root"
    Dim password As String = "Aasneh18"
    Dim database As String = "telephonedatabase"

    Public current_user As String = "PaymentGateway"
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
            sqlDt.Clear()
            ' Open the connection
            Mysqlconn.Open()

            sqlCmd.Connection = Mysqlconn
            sqlCmd.CommandText = "SELECT * FROM telephonedatabase.IssueTable"

            sqlRd = sqlCmd.ExecuteReader
            sqlDt.Load(sqlRd)
            sqlRd.Close()

            ' Close the connection after use
            Mysqlconn.Close()

            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.DataSource = sqlDt

            DataGridView1.Columns(0).Width = 60
            DataGridView1.Columns(1).Width = 100
            'DataGridView1.Columns(2).Width = 10
            DataGridView1.Columns(3).Width = 200

        Catch ex As Exception
            MessageBox.Show("Error updating table: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub viewtable()
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=telephonedatabase;"
        Mysqlconn.Open()

        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = "Select * from UserData;"

        sqlRd = sqlCmd.ExecuteReader
        sqlDt.Load(sqlRd)

        sqlRd.Close()
        Mysqlconn.Close()
        MessageBox.Show("Successful Connection")
        'DataGridView1.DataSource = sqlDt
    End Sub

    Private Sub initialize()
        'DataGridView1.DataSource = Nothing
        ' Close the connection before modifying the connection string property
        If Mysqlconn.State = ConnectionState.Open Then
            Mysqlconn.Close()
        End If

        ' Set the connection string property
        Mysqlconn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"

        ' Open the connection
        Mysqlconn.Open()

        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = "SELECT * FROM TelephoneDatabase.IssueTable"

        sqlRd = sqlCmd.ExecuteReader
        sqlDt.Load(sqlRd)
        sqlRd.Close()

        ' Close the connection after use
        Mysqlconn.Close()

        DataGridView1.DataSource = sqlDt


    End Sub


    Private Sub Form11_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        updateTable()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.ColumnIndex >= 0 Then
            ' If not, exit the event handler without performing any action
            Return
        End If

        Dim Name As String = ""
        Dim IITG_Email As String = ""
        Dim phonenumber As String = ""
        Dim Role As String = ""
        Dim CurrentPlan As String = ""
        Dim Expiry As String = ""
        Dim Raised As String = ""
        Dim Issue As String = ""

        current_issue_id = DataGridView1.SelectedRows(0).Cells(0).Value
        IITG_Email = DataGridView1.SelectedRows(0).Cells(1).Value.ToString
        Issue = DataGridView1.SelectedRows(0).Cells(2).Value.ToString
        Raised = DataGridView1.SelectedRows(0).Cells(3).Value.ToString

        If Mysqlconn.State = ConnectionState.Open Then
            Mysqlconn.Close()
        End If

        Mysqlconn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"

        ' Open the connection
        Mysqlconn.Open()

        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = "SELECT * FROM TelephoneDatabase.UserData where IITG_email = '" & IITG_Email & "'"

        sqlRd = sqlCmd.ExecuteReader
        'sqlDt.Load(sqlRd)
        While sqlRd.Read()
            Dim department As String = sqlRd.GetString("department")
            Name = sqlRd.GetString("Name")
            phonenumber = sqlRd.GetString("phonenumber")
            Role = sqlRd.GetString("role")
            CurrentPlan = sqlRd.GetString("plan_id")
            Expiry = sqlRd.GetString("expiry_date")
        End While

        sqlRd.Close()

        ' Close the connection after use
        Mysqlconn.Close()



        TextBox1.Text = Name
        TextBox2.Text = IITG_Email
        TextBox3.Text = phonenumber
        TextBox4.Text = Role
        TextBox5.Text = CurrentPlan
        TextBox6.Text = Expiry
        TextBox7.Text = Raised
        TextBox8.Text = Issue

    End Sub
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text.Length() > 0 Then
            If Mysqlconn.State = ConnectionState.Open Then
                Mysqlconn.Close()
            End If

            Mysqlconn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"

            ' Open the connection
            Mysqlconn.Open()

            sqlCmd.Connection = Mysqlconn
            sqlCmd.CommandText = "DELETE FROM TelephoneDatabase.IssueTable where IssueID = " & current_issue_id & ""
            sqlRd = sqlCmd.ExecuteReader
            sqlRd.Close()

            ' Close the connection after use
            Mysqlconn.Close()
            MessageBox.Show("Query resolved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            For Each row As DataGridViewRow In DataGridView1.SelectedRows
                DataGridView1.Rows.Remove(row)
            Next
            updateTable()
        Else
            MessageBox.Show("No Query Selected to Resolve", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        updateTable()
        

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged

    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim Name As String = ""
        Dim IITG_Email As String = ""
        Dim phonenumber As String = ""
        Dim Role As String = ""
        Dim CurrentPlan As String = ""
        Dim Expiry As String = ""
        Dim Raised As String = ""
        Dim Issue As String = ""
        Dim count As Integer = 0
        Dim issue_count As Integer = 0

        IITG_Email = TextBox9.Text

        If Mysqlconn.State = ConnectionState.Open Then
            Mysqlconn.Close()
        End If

        Mysqlconn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"

        ' Open the connection
        Mysqlconn.Open()

        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = "Select * FROM TelephoneDatabase.UserData where IITG_email = '" & IITG_Email & "'"
        sqlRd = sqlCmd.ExecuteReader()

        Dim Mysqlconn1 As New MySqlConnection

        Mysqlconn1.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"

        ' Open the connection
        Mysqlconn1.Open()

        sqlCmd.Connection = Mysqlconn1
        sqlCmd.CommandText = "Select * FROM TelephoneDatabase.IssueTable where PerformedBy = '" & IITG_Email & "'"
        sqlRd2 = sqlCmd.ExecuteReader()


        While sqlRd.Read()
            Name = sqlRd.GetString("Name")
            phonenumber = sqlRd.GetString("phonenumber")
            Role = sqlRd.GetString("role")
            CurrentPlan = sqlRd.GetString("plan_id")
            Expiry = sqlRd.GetString("expiry_date")
            count = count + 1
        End While

        
        'sqlDt.Load(sqlRd2)

        While sqlRd2.Read()
            Issue = sqlRd2.GetString("Issue")
            Raised = sqlRd2.GetString("ActionTimestamp")
            issue_count = issue_count + 1
        End While

        TextBox1.Text = Name
        TextBox2.Text = IITG_Email
        TextBox3.Text = phonenumber
        TextBox4.Text = Role
        TextBox5.Text = CurrentPlan
        TextBox6.Text = Expiry
        TextBox7.Text = Raised
        TextBox8.Text = Issue

        
        'sqlDt.Load(sqlRd)

        'Mysqlconn.Close()
        If IITG_Email.Length() = 0 Then
            'MessageBox.Show("No Email Entered!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            For Each row As DataGridViewRow In DataGridView1.SelectedRows
                DataGridView1.Rows.Remove(row)
            Next
            updateTable()
        ElseIf count = 0 Then
            MessageBox.Show("Enter correct IITG Email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf count > 1 Then
            MessageBox.Show("An error occurred in database. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf issue_count = 0 Then
            MessageBox.Show("This user has no issues/queries!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            'Dim dv As DataView
            'dv = sqlDt.DefaultView
            'dv.RowFilter = String.Format("PerformedBy like '%{0}%'", IITG_Email)
            'DataGridView1.DataSource = dv.ToTable

            For Each row As DataGridViewRow In DataGridView1.SelectedRows
                DataGridView1.Rows.Remove(row)
            Next

            If Mysqlconn.State = ConnectionState.Open Then
                Mysqlconn.Close()
            End If

            ' Set the connection string property
            'Mysqlconn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"

            ' Open the connection
            Mysqlconn.Open()

            sqlDt.Clear()
            sqlCmd3.Connection = Mysqlconn
            sqlCmd3.CommandText = "SELECT * FROM telephonedatabase.IssueTable where PerformedBy = '" & IITG_Email & "'"

            sqlRd3 = sqlCmd3.ExecuteReader
            sqlDt.Load(sqlRd3)
            sqlRd3.Close()

            ' Close the connection after use
            'Mysqlconn.Close()

            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.DataSource = sqlDt

            DataGridView1.Columns(0).Width = 60
            DataGridView1.Columns(1).Width = 100
            'DataGridView1.Columns(2).Width = 10
            DataGridView1.Columns(3).Width = 200


        End If

        'sqlRd3.Close()
        sqlRd2.Close()
        sqlRd.Close()
        Mysqlconn1.Close()
        Mysqlconn.Close()

        

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'DataGridView1.DataSource = Nothing
        'DataGridView1.Refresh()
        updateTable()




    End Sub

    Private Sub RectangleShape1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub BackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackButton.Click
        Me.Close()
        Form61.Show()
    End Sub
End Class