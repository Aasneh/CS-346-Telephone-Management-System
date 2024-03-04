Imports MySql.Data.MySqlClient
Public Class Form11

    Dim Mysqlconn As New MySqlConnection
    Dim sqlCmd As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlCmd2 As New MySqlCommand
    Dim sqlRd2 As MySqlDataReader
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
    Public talktimeLeft As String = "1"
    Public dataLeft As String = "12"
    Public expiry_date As String = "18-12-2024"



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

    Private Sub insert_table()
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=telephonedatabase;"
        Mysqlconn.Open()

        SqlQuery = "Insert into UserData(name,IITG_email,phonenumber,role,password,department,plan_id,expiry_date) values ('Aasneh','p.aasneh@iitg.ac.in','7021901677','Student','Aasneh','CSE',0,'03-03-2024');"


        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = SqlQuery

        sqlRd = sqlCmd.ExecuteReader
        'sqlDt.Load(sqlRd)

        sqlRd.Close()
        Mysqlconn.Close()
        MessageBox.Show("Successful Connection")
        'DataGridView1.DataSource = sqlDt
    End Sub

    Private Sub Make_Updates()
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=telephonedatabase;"
        Mysqlconn.Open()

        'SqlQuery = "UPDATE PlanData JOIN UserData ON UserData.plan_id = PlanData.plan_id Set PlanData.count = PlanData.count - 1 WHERE UserData.IITG_email = '" & current_user & "'  AND UserData.plan_id IS NOT NULL;"

        'sqlCmd.Connection = Mysqlconn
        'sqlCmd.CommandText = SqlQuery

        'sqlRd = sqlCmd.ExecuteReader
        'sqlRd.Close()

        SqlQuery = "Update UserData Set plan_id = " & plan_id & ", talktimeLeft =" & talktimeLeft & ", dataLeft='" & dataLeft & "', expiry_date = '" & expiry_date & "'  where IITG_email like '" & current_user & "';"

        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = SqlQuery

        sqlRd = sqlCmd.ExecuteReader
        sqlRd.Close()

        'SqlQuery = "UPDATE PlanData JOIN UserData ON UserData.plan_id = PlanData.plan_id Set PlanData.count = PlanData.count + 1 WHERE UserData.IITG_email = '" & current_user & "'  AND UserData.plan_id IS NOT NULL;"

        'sqlCmd.Connection = Mysqlconn
        'sqlCmd.CommandText = SqlQuery

        'sqlRd = sqlCmd.ExecuteReader
        'sqlRd.Close()

        SqlQuery = "INSERT INTO TelephoneDatabase.AuditTrail (PerformedBy, Action, TargetIITG_email, Details, ActionTimestamp) VALUES (@PerformedBy, @Action, @TargetIITG_email, @Details, NOW())"

        sqlCmd2.CommandText = SqlQuery
        sqlCmd2.Connection = Mysqlconn
        sqlCmd2.Parameters.AddWithValue("@PerformedBy", current_user)
        sqlCmd2.Parameters.AddWithValue("@Action", "UPDATE")
        sqlCmd2.Parameters.AddWithValue("@TargetIITG_email", current_user)
        sqlCmd2.Parameters.AddWithValue("@Details", "Successful Payment made!")

        'sqlCmd2.Connection = Mysqlconn
        'sqlCmd2.CommandText = SqlQuery

        sqlRd2 = sqlCmd2.ExecuteReader
        sqlRd2.Close()

        Mysqlconn.Close()

    End Sub

    Private Sub Form11_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RichTextBox1.Text = pay_amount
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged

    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged

    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Timer1.Start()
    End Sub

    Private Sub ProgressBar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProgressBar1.Click

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If ProgressBar1.Value + 1 <= ProgressBar1.Maximum Then
            ProgressBar1.Value += 1
        End If

        If ProgressBar1.Value >= ProgressBar1.Maximum Then
            ' Stop the timer
            Timer1.Stop()

            Make_Updates()

            MessageBox.Show("Successful Payment")

            Dim form51 As New Form51()
            Me.Hide()
            form51.current_user = current_user
            form51.Show()

        End If

    End Sub

    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged

    End Sub


    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub RectangleShape1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RectangleShape1.Click

    End Sub

    Private Sub TextBox1_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged

    End Sub

End Class
