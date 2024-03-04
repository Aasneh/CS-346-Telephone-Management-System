Imports MySql.Data.MySqlClient
Public Class Form51

    ' Declare MySqlConnection object
    Dim Mysqlconn As New MySqlConnection

    ' Declare MySqlCommand object
    Dim sqlCmd As New MySqlCommand

    ' Declare MySqlDataReader object
    Dim sqlRd As MySqlDataReader

    ' Declare DataTable object
    Dim sqlDt As New DataTable

    ' Declare MySqlDataAdapter object
    Dim Dta As New MySqlDataAdapter

    ' Declare SQL query string
    Dim SqlQuery As String

    ' Declare and initialize public variables
    Public current_user As String = "john.doe@iitg.ac.in"
    Public talktime As Integer = 0
    Public currentdate As String = Date.Now.ToString("dd-MM-yyyy")

    Private Sub Form51_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set MySQL connection string
        'MessageBox.Show(current_user)
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"

        ' Attempt database connection
        Try
            Mysqlconn.Open() ' Open database connection

            ' Set MySQL command properties
            sqlCmd.Connection = Mysqlconn
            sqlCmd.CommandText = "SELECT name,IITG_email,phonenumber,role, department, talktimeLeft, dataLeft, expiry_date, plan_id FROM userData WHERE IITG_email = '" & current_user & "';"

            ' Execute SQL command and load data into DataTable
            sqlRd = sqlCmd.ExecuteReader()
            sqlDt.Load(sqlRd)

            ' Retrieve row and column counts from DataTable
            Dim rowCount As Integer = sqlDt.Rows.Count
            Dim columnCount As Integer = sqlDt.Columns.Count

            ' Populate labels with data from DataTable
            NameLabel.Text = "Name: " & sqlDt.Rows(0)(0).ToString
            PhoneLabel.Text = "Phone No.: " & sqlDt.Rows(0)(2).ToString
            EmailLabel.Text = "Email: " & sqlDt.Rows(0)(1).ToString
            RoleLabel.Text = "Role: " & sqlDt.Rows(0)(3).ToString
            DeptLabel.Text = "Department: " & sqlDt.Rows(0)(4).ToString
            TalktimeLabel.Text = sqlDt.Rows(0)(5).ToString & " Minutes"
            talktime = Integer.Parse(sqlDt.Rows(0)(5).ToString)
            DataLabel.Text = sqlDt.Rows(0)(6).ToString & " GB/day"
            Dim expiryDate As String = sqlDt.Rows(0)(7).ToString
            Dim plan_id As String = sqlDt.Rows(0)(8).ToString

            ' Calculate time difference between expiry date and current date
            Dim ts As TimeSpan = Date.ParseExact(expiryDate, "dd-MM-yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).Date - Date.ParseExact(currentdate, "dd-MM-yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).Date

            ' Check if plan is expired
            If plan_id = "0" OrElse Date.ParseExact(currentdate, "dd-MM-yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).Date > Date.ParseExact(expiryDate, "dd-MM-yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).Date Then
                ' Update labels and buttons accordingly
                TalktimeLabel.Text = "No plan selected"
                DataLabel.Visible = False
                SMSLabel.Visible = False
                ExpiryLabel.Text = "Your Pack has expired!"
                Button4.Text = "Buy Plan"
                Button3.Visible = False
            Else
                ' Update labels and buttons accordingly
                DataLabel.Visible = True
                SMSLabel.Visible = True
                If ts.Days = 0 Then
                    ExpiryLabel.Text = "Your pack expires today! Recharge Now"
                Else
                    ExpiryLabel.Text = "Expires in " & ts.Days & " days"
                End If
                Button4.Text = "Edit Plan"
                Button3.Visible = True
            End If

            ' Close data reader and database connection
            sqlRd.Close()
            Mysqlconn.Close()

        Catch ex As Exception
            ' Display error message if an exception occurs
            MessageBox.Show("Error connecting to database: " & ex.Message)

        Finally
            ' Dispose of the MySqlConnection object
            Mysqlconn.Dispose()

        End Try
    End Sub


    Private Sub BackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackButton.Click
        ' Hide current form and show Form41
        Me.Close()
        Form41.Show()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        ' Hide current form and show Form31
        Form31.current_user = Me.current_user
        Me.Close()
        Form31.Show()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ' Display selected cell value in TextBox2
        Try
            TextBox2.Text = DataGridView1.Rows(DataGridView1.SelectedCells(0).RowIndex).Cells(1).Value.ToString()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ' Set MySQL connection string
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"

        ' Attempt database connection
        Try
            Mysqlconn.Open() ' Open database connection

            ' Set MySQL command properties
            sqlCmd.Connection = Mysqlconn
            sqlCmd.CommandText = "SELECT * FROM userData WHERE user_visibility= 'Public';"

            ' Clear DataTable and load new data
            sqlDt.Clear()
            sqlRd = sqlCmd.ExecuteReader()
            sqlDt.Load(sqlRd)

            ' Retrieve row and column counts from DataTable
            Dim rowCount As Integer = sqlDt.Rows.Count
            Dim columnCount As Integer = sqlDt.Columns.Count

            ' Close data reader and database connection
            sqlRd.Close()
            Mysqlconn.Close()

        Catch ex As Exception
            ' Display error message if an exception occurs
            MessageBox.Show("Error connecting to database: " & ex.Message)
            Return

        Finally
            ' Dispose of the MySqlConnection object
            Mysqlconn.Dispose()

        End Try

        ' Filter DataTable based on TextBox1 input
        Try
            Dim dv As DataView
            dv = sqlDt.DefaultView
            dv.RowFilter = String.Format("name like '%{0}%'", TextBox1.Text)
            DataGridView1.DataSource = dv.ToTable(False, {"Name", "Phonenumber"})
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ' Retrieve phone number from TextBox2
        Dim phone As String = TextBox2.Text

        ' Validate phone number
        If phone.Length <> 10 Then
            MessageBox.Show("Not a valid phone number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Declare name variable
        Dim name As String = ""

        ' Set MySQL connection string
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"

        ' Attempt database connection
        Try
            Mysqlconn.Open() ' Open database connection

            ' Set MySQL command properties
            sqlCmd.Connection = Mysqlconn
            sqlCmd.CommandText = "SELECT name, IITG_email FROM userData WHERE phonenumber =" & phone & ";"

            ' Clear DataTable and load new data
            sqlDt.Clear()
            sqlRd = sqlCmd.ExecuteReader()
            sqlDt.Load(sqlRd)

            ' Retrieve row count from DataTable
            Dim rowCount As Integer = sqlDt.Rows.Count
            Dim columnCount As Integer = sqlDt.Columns.Count

            ' Check if phone number exists
            If rowCount = 0 Then
                MessageBox.Show("Phone Number not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            ElseIf rowCount >= 1 Then
                If sqlDt.Rows(0)(1).ToString = current_user Then
                    MessageBox.Show("You cannot call yourself!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                name = sqlDt.Rows(0)(0).ToString
            End If

            ' Close data reader and database connection
            sqlRd.Close()
            Mysqlconn.Close()

        Catch ex As Exception
            ' Display error message if an exception occurs
            MessageBox.Show("Error connecting to database: " & ex.Message)

        Finally
            ' Dispose of the MySqlConnection object
            Mysqlconn.Dispose()

        End Try

        ' Show Form52 with relevant data
        Form52.user_name = name
        Form52.current_user = current_user
        Form52.phone = phone
        Form52.talktimeLeft = talktime
        Form52.Show()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ' Prompt user for confirmation
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel your plan?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

        ' Process the user's selection
        Select Case result
            Case DialogResult.OK
                ' Update labels and buttons accordingly
                TalktimeLabel.Text = "No plan selected"
                DataLabel.Visible = False
                SMSLabel.Visible = False
                ExpiryLabel.Text = "Your Pack has expired!"
                Button4.Text = "Buy Plan"
                Button3.Visible = False

                ' Attempt database connection
                Try
                    Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"
                    Mysqlconn.Open() ' Open database connection

                    ' Set MySQL command properties
                    sqlCmd.Connection = Mysqlconn
                    sqlCmd.CommandText = "UPDATE userData SET talktimeLeft = 0, dataLeft=0, plan_id= -1 expiry_date = 01-01-1970 WHERE IITG_email = " & current_user & ";"

                    ' Clear DataTable and load new data
                    sqlRd = sqlCmd.ExecuteReader()
                    sqlDt.Load(sqlRd)

                    ' Retrieve row count from DataTable
                    Dim rowCount As Integer = sqlDt.Rows.Count
                    Dim columnCount As Integer = sqlDt.Columns.Count

                    ' Display error message if an exception occurs
                    If rowCount = 0 Then
                        MessageBox.Show("Phone Number not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ElseIf rowCount > 1 Then
                        Name = sqlDt.Rows(0)(0).ToString
                    End If

                    ' Close data reader and database connection
                    sqlRd.Close()
                    Mysqlconn.Close()

                Catch ex As Exception
                    MessageBox.Show("Error connecting to database: " & ex.Message)
                Finally
                    Mysqlconn.Dispose() ' Dispose of the connection object
                End Try

        End Select

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Form13.current_user = current_user
        Me.Hide()
        Form13.Show()
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class