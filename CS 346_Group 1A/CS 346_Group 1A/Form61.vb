Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32
Imports System.Security.Cryptography
Imports System.Text




Public Class Form61


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

    ' some more variables
    Dim old2name As String
    Dim old2IITG_email As String
    Dim old2phonenumber As String
    Dim old2role As String
    Dim old2password As String
    Dim old2department As String
    Dim old2plan_id As String
    Dim old2expiry_date As String
    Dim old2talktimeLeft As String
    Dim old2dataLeft As String
    Dim old2user_visibility As String


    Private bitmap As Bitmap



    'HASHING

    '////////////////////////HASH FUNCTION

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

    'Use the below two statements at appropriate positions
    'Dim enteredPassword As String = TextBox3.Text
    'Dim hashedPassword As String = PasswordHashing.HashPassword(enteredPassword)



    Private Function ValidateEmail(ByVal email As String) As Boolean
        ' Check if the email ends with "@iitg.ac.in"
        Return email.EndsWith("@iitg.ac.in")
    End Function

    Private Function ValidatePhoneNumber(ByVal phoneNumber As String) As Boolean
        ' Check if the phone number has 10 digits
        Return phoneNumber.Length = 10 AndAlso IsNumeric(phoneNumber)
    End Function

    Private Function ValidateRole(ByVal role As String) As Boolean
        ' Check if the role is either "Teacher" or "Student"
        Return role.Equals("Student", StringComparison.OrdinalIgnoreCase) OrElse role.Equals("Faculty", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Function ValidateExpiryDate(ByVal expiryDate As String) As Boolean
        ' Check if the expiry date is in the format "dd-mm-yyyy"
        Dim parsedDate As DateTime
        Return DateTime.TryParseExact(expiryDate, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, parsedDate)
    End Function

    Private Function ValidateTalktimeLeft(ByVal talktimeLeft As String) As Boolean
        ' Check if the talktime left is a non-negative integer
        Dim parsedLeft As Integer
        Return Integer.TryParse(talktimeLeft, parsedLeft) AndAlso parsedLeft >= 0
    End Function

    Private Function ValidateDataLeft(ByVal dataLeft As String) As Boolean
        ' Check if the data left is a non-negative float
        Dim parsedLeft As Single
        Return Single.TryParse(dataLeft, parsedLeft) AndAlso parsedLeft >= 0
    End Function

    Private Function ValidateUserVisibility(ByVal userVisibility As String) As Boolean
        ' Check if the user visibility is either "public" or "private"
        Return userVisibility.Equals("Public", StringComparison.OrdinalIgnoreCase) OrElse userVisibility.Equals("Private", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Function ValidateDepartment(ByVal department As String) As Boolean
        Return department.Equals("BioSciences and Bioengineering", StringComparison.OrdinalIgnoreCase) OrElse department.Equals("Chemical Engineering", StringComparison.OrdinalIgnoreCase) OrElse department.Equals("Chemical Science and Technology", StringComparison.OrdinalIgnoreCase) OrElse department.Equals("Chemical Engineering", StringComparison.OrdinalIgnoreCase) OrElse department.Equals("Civil Engineering", StringComparison.OrdinalIgnoreCase) OrElse department.Equals("Computer Science & Engineering", StringComparison.OrdinalIgnoreCase) OrElse department.Equals("Electronics and Electrical Engineering", StringComparison.OrdinalIgnoreCase) OrElse department.Equals("Design", StringComparison.OrdinalIgnoreCase) OrElse department.Equals("Humanities and Social Sciences", StringComparison.OrdinalIgnoreCase) OrElse department.Equals("Mathematics", StringComparison.OrdinalIgnoreCase) OrElse department.Equals("Mechanical Engineering", StringComparison.OrdinalIgnoreCase) OrElse department.Equals("Physics", StringComparison.OrdinalIgnoreCase)
    End Function


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
            sqlCmd.CommandText = "SELECT * FROM TelephoneDatabase.UserData"

            sqlRd = sqlCmd.ExecuteReader
            sqlDt.Load(sqlRd)
            sqlRd.Close()

            ' Close the connection after use
            sqlConn.Close()

            DataGridView1.DataSource = sqlDt

        Catch ex As Exception
            MessageBox.Show("Error updating table: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub deleteTable()
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
            sqlCmd.CommandText = "Delete FROM Telephonedatabase.UserData where IITG_email='" & old2IITG_email & "';"

            sqlRd = sqlCmd.ExecuteReader
            sqlDt.Load(sqlRd)
            sqlRd.Close()

            ' Close the connection after use
            sqlConn.Close()

            DataGridView1.DataSource = sqlDt

        Catch ex As Exception
            MessageBox.Show("Error updating table: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        updateTable()
    End Sub


    Private Sub Logout_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Logout_Button.Click

        Dim iExit As DialogResult

        iExit = MessageBox.Show("Confirm if you want to Logout", "MYSQL Connector", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If iExit = DialogResult.Yes Then
            Me.Close()
            Form41.Show()

        End If


    End Sub


    Private Sub LogAuditTrail(ByVal action As String, ByVal IITG_email As String, ByVal details As String)
        Try
            Dim sqlConn As New MySqlConnection
            Dim sqlCmd As New MySqlCommand

            ' Set your connection string
            Dim connectionString As String = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"

            ' SQL query to insert into the AuditTrail table
            Dim sqlQuery As String = "INSERT INTO TelephoneDatabase.AuditTrail (PerformedBy, Action, TargetIITG_email, Details, ActionTimestamp) VALUES (@PerformedBy, @Action, @TargetIITG_email, @Details, NOW())"

            ' Create and configure the MySqlCommand object
            sqlCmd.CommandText = sqlQuery
            sqlCmd.Connection = sqlConn
            sqlCmd.Parameters.AddWithValue("@PerformedBy", "ADMIN")
            sqlCmd.Parameters.AddWithValue("@Action", action)
            sqlCmd.Parameters.AddWithValue("@TargetIITG_email", IITG_email)
            sqlCmd.Parameters.AddWithValue("@Details", details)

            ' Open the connection, execute the query, and then close the connection
            sqlConn.ConnectionString = connectionString
            sqlConn.Open()
            sqlCmd.ExecuteNonQuery()
            sqlConn.Close()

        Catch ex As Exception
            MessageBox.Show("Error logging to AuditTrail: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    Private Sub Add_New_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Add_New_Button.Click

        If Name_TextBox.Text = "" OrElse IITG_Email_TextBox.Text = "" OrElse Phone_Number_TextBox.Text = "" OrElse _
      Role_TextBox.Text = "" OrElse Password_TextBox.Text = "" OrElse Department_TextBox.Text = "" OrElse _
      Current_Plan_TextBox.Text = "" OrElse Expiry_TextBox.Text = "" Then
            MessageBox.Show("Please fill in all the fields before adding a new record.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If



        sqlConn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"


        If Not ValidateEmail(IITG_Email_TextBox.Text) Then
            MessageBox.Show("Invalid IITG Email ID. It must end with '@iitg.ac.in'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not ValidatePhoneNumber(Phone_Number_TextBox.Text) Then
            MessageBox.Show("Invalid phone number. It must have 10 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not ValidateRole(Role_TextBox.Text) Then
            MessageBox.Show("Invalid role. It must be either 'Student' or 'Faculty'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not ValidateDepartment(Department_TextBox.Text) Then
            MessageBox.Show("Enter a Valid Department!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not ValidateExpiryDate(Expiry_TextBox.Text) Then
            MessageBox.Show("Invalid expiry date format. It must be in the format 'dd-mm-yyyy'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not ValidateTalktimeLeft(Talk_Time_TextBox.Text) Then
            MessageBox.Show("Invalid talktime left. It must be a non-negative integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not ValidateDataLeft(Data_Left_TextBox.Text) Then
            MessageBox.Show("Invalid data left. It must be a non-negative float.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not ValidateUserVisibility(Visibility_TextBox.Text) Then
            MessageBox.Show("Invalid user visibility. It must be either 'Public' or 'Private'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If







        Try
            sqlConn.Open()
            sqlQuery = "Insert into TelephoneDatabase.UserData(name, IITG_email, phonenumber, role, password, department, plan_id, expiry_date, talktimeLeft, dataLeft, user_visibility) value('" & Name_TextBox.Text & "','" & IITG_Email_TextBox.Text & "','" & Phone_Number_TextBox.Text & "','" & Role_TextBox.Text & "','" & HashPassword(Password_TextBox.Text) & "','" & Department_TextBox.Text & "','" & Current_Plan_TextBox.Text & "','" & Expiry_TextBox.Text & "','" & Talk_Time_TextBox.Text & "','" & Data_Left_TextBox.Text & "','" & Visibility_TextBox.Text & "');"

            sqlCmd = New MySqlCommand(sqlQuery, sqlConn)
            sqlRd = sqlCmd.ExecuteReader
            sqlConn.Close()

            MessageBox.Show("New record added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)



            ' Log the action to the AuditTrail
            LogAuditTrail("NEW USER", IITG_Email_TextBox.Text, "New record added for user: " & IITG_Email_TextBox.Text)


        Catch ex As Exception
            MessageBox.Show(ex.Message, "MYSQL Connector", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            sqlConn.Dispose()
        End Try

        updateTable()


    End Sub



    Private Sub Reset_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Reset_Button.Click
        Try
            For Each txt In Panel4.Controls
                If TypeOf txt Is TextBox Then
                    txt.text = ""
                End If
            Next
            Search_TextBox.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub






    Private Sub Update_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_Button.Click


        If Name_TextBox.Text = "" OrElse IITG_Email_TextBox.Text = "" OrElse Phone_Number_TextBox.Text = "" OrElse _
      Role_TextBox.Text = "" OrElse Password_TextBox.Text = "" OrElse Department_TextBox.Text = "" OrElse _
      Current_Plan_TextBox.Text = "" OrElse Expiry_TextBox.Text = "" Then
            MessageBox.Show("Please fill in all the fields before adding a new record.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If


        If old2IITG_email <> IITG_Email_TextBox.Text Then
            MessageBox.Show("Can't change emailID for Update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not ValidatePhoneNumber(Phone_Number_TextBox.Text) Then
            MessageBox.Show("Invalid phone number. It must have 10 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not ValidateRole(Role_TextBox.Text) Then
            MessageBox.Show("Invalid role. It must be either 'Student'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not ValidateDepartment(Department_TextBox.Text) Then
            MessageBox.Show("Enter a Valid Department!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not ValidateExpiryDate(Expiry_TextBox.Text) Then
            MessageBox.Show("Invalid expiry date format. It must be in the format 'dd-mm-yyyy'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If Not ValidateTalktimeLeft(Talk_Time_TextBox.Text) Then
            MessageBox.Show("Invalid talktime left. It must be a non-negative integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not ValidateDataLeft(Data_Left_TextBox.Text) Then
            MessageBox.Show("Invalid data left. It must be a non-negative float.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not ValidateUserVisibility(Visibility_TextBox.Text) Then
            MessageBox.Show("Invalid user visibility. It must be either 'Public' or 'Private'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If



        Try
            sqlConn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"
            sqlConn.Open()

            sqlCmd.Connection = sqlConn

            With sqlCmd
                .CommandText = "UPDATE TelephoneDatabase.UserData SET name = @name, IITG_email=@NewIITG_email, phonenumber=@phonenumber, role=@role, password=@password, department=@department, plan_id=@plan_id, expiry_date=@expiry_date, talktimeLeft=@talktimeLeft, dataLeft=@dataLeft, user_visibility=@user_visibility WHERE IITG_email=@OldIITG_email"
                .CommandType = CommandType.Text

                ' Add parameters within the With block only
                .Parameters.AddWithValue("@NewIITG_email", IITG_Email_TextBox.Text)
                .Parameters.AddWithValue("@name", Name_TextBox.Text)
                .Parameters.AddWithValue("@phonenumber", Phone_Number_TextBox.Text)
                .Parameters.AddWithValue("@role", Role_TextBox.Text)
                .Parameters.AddWithValue("@password", HashPassword(Password_TextBox.Text))
                .Parameters.AddWithValue("@department", Department_TextBox.Text)
                .Parameters.AddWithValue("@plan_id", Current_Plan_TextBox.Text)
                .Parameters.AddWithValue("@expiry_date", Expiry_TextBox.Text)
                .Parameters.AddWithValue("@talktimeLeft", Talk_Time_TextBox.Text)
                .Parameters.AddWithValue("@dataLeft", Data_Left_TextBox.Text)
                .Parameters.AddWithValue("@user_visibility", Visibility_TextBox.Text)
                ' Add the parameter for OldStudentid
                .Parameters.AddWithValue("@OldIITG_email", IITG_Email_TextBox.Text)

                ' Execute the command
                .ExecuteNonQuery()

                ' Clear parameters for next update operation
                .Parameters.Clear()
            End With



            ' Determine which fields were updated
            Dim updatedFields As New List(Of String)
            If old2IITG_email <> IITG_Email_TextBox.Text Then
                updatedFields.Add("IITG Email")
            End If
            '^^redundant, should not happen

            If old2name <> Name_TextBox.Text Then
                updatedFields.Add("Name")
            End If
            If old2phonenumber <> Phone_Number_TextBox.Text Then
                updatedFields.Add("Phone Number")
            End If
            ' Add similar checks for other fields...
            If old2role <> Role_TextBox.Text Then
                updatedFields.Add("Role")
            End If
            If old2password <> Password_TextBox.Text Then
                updatedFields.Add("Password")
            End If
            If old2department <> Department_TextBox.Text Then
                updatedFields.Add("Department")
            End If
            If old2plan_id <> Current_Plan_TextBox.Text Then
                updatedFields.Add("Plan ID")
            End If
            If old2expiry_date <> Expiry_TextBox.Text Then
                updatedFields.Add("Expiry Date")
            End If
            If old2talktimeLeft <> Talk_Time_TextBox.Text Then
                updatedFields.Add("Talk Time")
            End If
            If old2dataLeft <> Data_Left_TextBox.Text Then
                updatedFields.Add("Data Left")
            End If
            If old2user_visibility <> Visibility_TextBox.Text Then
                updatedFields.Add("Visibility")
            End If

            ' Log the updated fields
            Dim updatedFieldsString As String = String.Join(", ", updatedFields)




            MessageBox.Show("Record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'LogAuditTrail("UPDATE", IITG_Email_TextBox.Text, "Record updated for user: " & Name_TextBox.Text)
            LogAuditTrail("UPDATE", IITG_Email_TextBox.Text, "Updated Fields: " & updatedFieldsString)

            ' Refresh DataGridView to reflect the updated data
            updateTable()

        Catch ex As Exception
            MessageBox.Show("Error updating record: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
        End Try
    End Sub








    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ' Check if the clicked cell is in the leftmost column
        If e.ColumnIndex >= 0 Then
            ' If not, exit the event handler without performing any action
            Return
        End If

        ' Now proceed to load data into text boxes only if the clicked cell is in the leftmost column
        Try
            Name_TextBox.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString
            IITG_Email_TextBox.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString
            Phone_Number_TextBox.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString
            Role_TextBox.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString
            Password_TextBox.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString
            Department_TextBox.Text = DataGridView1.SelectedRows(0).Cells(5).Value.ToString
            Current_Plan_TextBox.Text = DataGridView1.SelectedRows(0).Cells(6).Value.ToString
            Expiry_TextBox.Text = DataGridView1.SelectedRows(0).Cells(7).Value.ToString
            Talk_Time_TextBox.Text = DataGridView1.SelectedRows(0).Cells(8).Value.ToString
            Data_Left_TextBox.Text = DataGridView1.SelectedRows(0).Cells(9).Value.ToString
            Visibility_TextBox.Text = DataGridView1.SelectedRows(0).Cells(10).Value.ToString



            'to compare changed values
            old2name = Name_TextBox.Text
            old2IITG_email = IITG_Email_TextBox.Text
            old2phonenumber = Phone_Number_TextBox.Text
            old2role = Role_TextBox.Text
            old2password = Password_TextBox.Text
            old2department = Department_TextBox.Text
            old2plan_id = Current_Plan_TextBox.Text
            old2expiry_date = Expiry_TextBox.Text
            old2talktimeLeft = Talk_Time_TextBox.Text
            old2dataLeft = Data_Left_TextBox.Text
            old2user_visibility = Visibility_TextBox.Text


            'store in variables

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub Delete_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_Button.Click

        LogAuditTrail("DELETE", IITG_Email_TextBox.Text, "Deleted User: " & IITG_Email_TextBox.Text)
        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            DataGridView1.Rows.Remove(row)
        Next
        deleteTable()
        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)




    End Sub

    Private Sub Print_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Print_Button.Click
        Dim height As Integer = DataGridView1.Height
        DataGridView1.Height = (DataGridView1.RowCount + 1) * DataGridView1.RowTemplate.Height
        bitmap = New Bitmap(Me.DataGridView1.Width, Me.DataGridView1.Height)
        DataGridView1.DrawToBitmap(bitmap, New Rectangle(0, 0, Me.DataGridView1.Width, Me.DataGridView1.Height))
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.PrintPreviewControl.Zoom = 1
        PrintPreviewDialog1.ShowDialog()
        DataGridView1.Height = height
    End Sub




    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

        e.Graphics.DrawImage(bitmap, 0, 0)
        Dim recP As RectangleF = e.PageSettings.PrintableArea

        If Me.DataGridView1.Height - recP.Height > 0 Then e.HasMorePages = True

    End Sub

    Private Sub Search_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Search_Button.Click
        Try
            Dim dv As DataView
            dv = sqlDt.DefaultView
            dv.RowFilter = String.Format("name like '%{0}%'", Search_TextBox.Text)
            DataGridView1.DataSource = dv.ToTable
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    'or one more, i.e on key press (pressing enter leads to searching!)

    Private Sub AuditLog_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AuditLog_Button.Click
        ' Create an instance of the new form
        Dim newForm As New Form62()

        ' Show the new form
        newForm.Show()
    End Sub




    Private Sub QueryTab_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QueryTab_Button.Click
        Me.Hide()
        Form12.Show()
    End Sub

    Private Sub Stats_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Stats_Button.Click
        Me.Hide()
        Form32.back_to = 0
        Form32.Show()
    End Sub
End Class
