Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class Form21

    Private Sub Form21_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '  Me.Height = 730
        ' Me.Width = 1000


        ' Add options to the ComboBox
        ComboBox1.Items.Add("Student")
        ComboBox1.Items.Add("Faculty")

        ' Set default selection
        ComboBox1.SelectedIndex = 0

        ' Add options to the ComboBox
        ComboBox2.Items.Add("Biosciences and Bioengineering")
        ComboBox2.Items.Add("Chemical Engineering")
        ComboBox2.Items.Add("Chemical Science and Technology")
        ComboBox2.Items.Add("Civil Engineering")
        ComboBox2.Items.Add("Computer Science & Engineering")
        ComboBox2.Items.Add("Design")
        ComboBox2.Items.Add("Electronics and Electrical Engineering")
        ComboBox2.Items.Add("Humanities and Social Sciences")
        ComboBox2.Items.Add("Mathematics")
        ComboBox2.Items.Add("Mechanical Engineering")
        ComboBox2.Items.Add("Physics")


        ' Set default selection
        ComboBox2.SelectedIndex = 4

        'Add items to User Visibility
        ComboBox3.Items.Add("Public")
        ComboBox3.Items.Add("Private")


        ' Set default selection
        ComboBox3.SelectedIndex = 0


        'Restart the database
        'empty_table()

        ' Set the PasswordChar for the password TextBox
        TextBox3.PasswordChar = "*"
        TextBox4.PasswordChar = "*"

    End Sub

    Dim Mysqlconn As New MySqlConnection
    Dim sqlCmd As New MySqlCommand
    Dim sqlCmd2 As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlRd2 As MySqlDataReader
    Dim sqlDt As New DataTable
    Dim Dta As New MySqlDataAdapter
    Dim SqlQuery As String
    Dim SqlQuery2 As String


    Private Sub viewtable()
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"
        Mysqlconn.Open()

        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = "Select * from UserData;"

        sqlRd = sqlCmd.ExecuteReader
        sqlDt.Load(sqlRd)

        sqlRd.Close()
        Mysqlconn.Close()
        'MessageBox.Show("Successful Connection")
        'DataGridView1.DataSource = sqlDt
    End Sub


    Private Sub empty_table()
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"
        Mysqlconn.Open()

        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = "Truncate UserData;"

        sqlRd = sqlCmd.ExecuteReader
        'sqlDt.Load(sqlRd)

        sqlRd.Close()
        Mysqlconn.Close()
        MessageBox.Show("Database Restarted")

    End Sub



    Private Sub insert_table()
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"
        Mysqlconn.Open()

        'SqlQuery = "Insert into UserData(name,IITG_email,phonenumber,role,password,department,plan_id,expiry_date,talktimeLeft,dataLeft,user_visibility) values ('Aasneh','p.aasneh@iitg.ac.in','7021901677','Student','Aasneh','CSE',0,'03-03-2024',0,0,'Public');"


        sqlCmd.Connection = Mysqlconn
        sqlCmd2.Connection = Mysqlconn

        sqlCmd.CommandText = SqlQuery
        sqlCmd2.CommandText = SqlQuery2

        sqlRd = sqlCmd.ExecuteReader
        sqlRd.Close()
        sqlRd2 = sqlCmd2.ExecuteReader
        sqlRd2.Close()
        'sqlDt.Load(sqlRd)


        Mysqlconn.Close()
        'MessageBox.Show("Successful Connection")
        'DataGridView1.DataSource = sqlDt




    End Sub

    Private Sub insert_in_log_table()
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"
        Mysqlconn.Open()

        'SqlQuery = "Insert into UserData(name,IITG_email,phonenumber,role,password,department,plan_id,expiry_date,talktimeLeft,dataLeft,user_visibility) values ('Aasneh','p.aasneh@iitg.ac.in','7021901677','Student','Aasneh','CSE',0,'03-03-2024',0,0,'Public');"


        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = SqlQuery

        sqlRd = sqlCmd.ExecuteReader
        'sqlDt.Load(sqlRd)

        sqlRd.Close()
        Mysqlconn.Close()
        'MessageBox.Show("Successful Connection")
        'DataGridView1.DataSource = sqlDt



    End Sub

    Private Sub ValidateUsername()
        Dim enteredUsername As String = TextBox1.Text.Trim()

        ' Check if the entered username ends with "@iitg.ac.in"
        If Not enteredUsername.EndsWith("@iitg.ac.in", StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("Username must end with @iitg.ac.in")
            ' You can also clear the text field or take other actions based on your requirements
            TextBox1.Clear()
        Else
            ' Username is valid, you can proceed with further actions
            MessageBox.Show("Valid username")
        End If
    End Sub

    Private Function EntryExists(ByVal mailId As String) As Boolean
        Dim result As Boolean = False

        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"
        Mysqlconn.Open()

        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = "SELECT COUNT(*) FROM UserData WHERE IITG_email = @MailId"

        ' Clear existing parameters before adding new ones
        sqlCmd.Parameters.Clear()

        sqlCmd.Parameters.AddWithValue("@MailId", mailId)

        Try
            Dim count As Integer = CInt(sqlCmd.ExecuteScalar())
            result = (count > 0)

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)

        Finally
            Mysqlconn.Close()
        End Try

        Return result
    End Function






    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'viewtable()
        Dim name As String = TextBox1.Text
        Dim IITG_mail_id As String = TextBox2.Text.Trim()
        Dim Role As String = ComboBox1.SelectedItem.ToString()
        Dim Department As String = ComboBox2.SelectedItem.ToString()
        Dim Password As String = TextBox3.Text
        Dim Confirm_Password As String = TextBox4.Text
        Dim userVisibility As String = ComboBox3.SelectedItem.ToString()

        'Hash the password
        Dim enteredPassword As String = TextBox3.Text
        Dim hashedPassword As String = HashPassword(enteredPassword)

        ' Get the current date
        Dim currentDate As DateTime = DateTime.Now

        ' Format the current date as DD-MM-YYYY
        Dim formattedDate As String = currentDate.ToString("dd-MM-yyyy")

        Dim errorFlag As Boolean = False
        Dim validUsernameError As String = ""
        Dim UsernameAlreadyExists As Boolean = EntryExists(IITG_mail_id)
        Dim passwordMatchError As String = ""
        Dim errorMessageBoxString As String = ""


        'Implement for checking whether all the fields are filled or not
        If String.IsNullOrEmpty(name) Then
            'Set error to true
            errorFlag = True
            errorMessageBoxString &= "Name cannot be empty. Please enter a name." & vbCrLf
        End If

        If String.IsNullOrEmpty(IITG_mail_id) Then
            'Set error to true
            errorFlag = True
            errorMessageBoxString &= "IITG Mail ID cannot be empty. Please enter a valid IITG Mail ID." & vbCrLf
        End If

        If String.IsNullOrEmpty(Password) Then
            'Set error to true
            errorFlag = True
            errorMessageBoxString &= "Password cannot be empty. Please enter a password." & vbCrLf
        End If

        If String.IsNullOrEmpty(Confirm_Password) Then
            'Set error to true
            errorFlag = True
            errorMessageBoxString &= "Confirm Password cannot be empty. Please confirm your password." & vbCrLf
        End If

        'Implement to check whether the username already exists or not
        If UsernameAlreadyExists Then
            'Set error to true
            errorFlag = True
            errorMessageBoxString &= "Username Already exists!! Enter a new Username." & vbCrLf
        End If

        '///////////////////////////////////////////////////////////////////////////////
        ' Check if the entered username ends with "@iitg.ac.in"
        If Not IITG_mail_id.EndsWith("@iitg.ac.in", StringComparison.Ordinal) Then
            'MessageBox.Show("Username must end with @iitg.ac.in")
            ' You can also clear the text field or take other actions based on your requirements
            'TextBox1.Clear()
            'Set error to true
            errorFlag = True
            TextBox2.Text = ""

            validUsernameError = "Username must end with @iitg.ac.in"
        Else
            ' Username is valid, you can proceed with further actions
            'MessageBox.Show("Valid username")
        End If
        '/////////////////////////////////////////////////////////////////////////////////
        ' Check if both strings are the same, considering case sensitivity
        If String.Equals(Password, Confirm_Password, StringComparison.Ordinal) Then
            'MessageBox.Show("Passwords match.")
        Else
            'MessageBox.Show("Passwords do not match.")
            'Set error to true
            errorFlag = True
            TextBox3.Text = ""
            TextBox4.Text = ""
            passwordMatchError = "Passwords do not match."

        End If
        '///////////////////////////////////////////////////////////////////////////////////

        If errorFlag Then

            If validUsernameError <> "" Then
                If errorMessageBoxString <> "" Then
                    errorMessageBoxString &= vbCrLf  ' Add a newline only if there's a previous error
                End If
                errorMessageBoxString &= validUsernameError
            End If



            If passwordMatchError <> "" Then
                If errorMessageBoxString <> "" Then
                    errorMessageBoxString &= vbCrLf  ' Add a newline only if there's a previous error
                End If
                errorMessageBoxString &= passwordMatchError
            End If

            MessageBox.Show(errorMessageBoxString, "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Else

            'call the phone number assignment function
            Dim phoneNumber As String = AssignPhoneNumber(Department, Role)

            MessageBox.Show("Assigned Phone Number: " & phoneNumber)

            'set the sql query
            SqlQuery = "INSERT INTO userdata(name, IITG_email, phonenumber, role, password, department, plan_id, expiry_date,talktimeLeft,dataLeft,user_visibility) " &
                             "VALUES ('" & name & "', '" & IITG_mail_id & "', '" & phoneNumber & "', '" & Role & "', '" & hashedPassword & "', '" & Department & "', 0, '" & formattedDate & "',0,0,'" & userVisibility & "');"
            'SqlQuery = "Insert into UserData(name,IITG_email,phonenumber,role,password,department,plan_id,expiry_date,talktimeLeft,dataLeft,user_visibility) values ('Aasneh','p.aasneh@iitg.ac.in','7021901677','Student','Aasneh','CSE',0,'03-03-2024');"



            'set the sql query
            SqlQuery = "INSERT INTO TelephoneDatabase.UserData(name, IITG_email, phonenumber, role, password, department, plan_id, expiry_date,talktimeLeft,dataLeft,user_visibility) " &
             "VALUES ('" & name & "', '" & IITG_mail_id & "', '" & phoneNumber & "', '" & Role & "', '" & hashedPassword & "', '" & Department & "', 0, '" & formattedDate & "', 0 , 0 ,'" & userVisibility & "');"
            'SqlQuery = "Insert into UserData(name,IITG_email,phonenumber,role,password,department,plan_id,expiry_date,talktimeLeft,dataLeft,user_visibility) values ('Aasneh','p.aasneh@iitg.ac.in','7021901677','Student','Aasneh','CSE',0,'03-03-2024');"


            Dim timestamp As DateTime = DateTime.Now

            'set the sql query
            SqlQuery2 = "INSERT INTO TelephoneDatabase.AuditTrail( PerformedBy, Action, TargetIITG_email, Details, ActionTimestamp) " &
                             "VALUES ( '" & IITG_mail_id & "', 'NEW USER',  '" & IITG_mail_id & "', 'New user added.',NOW());"



            'call insert table function
            insert_table()
            Me.Hide()
            Form41.Show()

        End If








        '///////////////////////////////////////////////////////////////////////////////////


    End Sub

    '///////////////////////////////////////////////////////////////////
    'PHONE NUMBER ASSIGNMENT FUNCTION
    Private Shared lastAssignedNumber As Integer

    Public Function AssignPhoneNumber(ByVal department As String, ByVal role As String) As String
        Dim departmentCode As String = GetDepartmentCode(department)
        Dim roleDigit As String = GetRoleDigit(role)
        Dim counterDigits As String = GetCounterDigits(department)

        Dim phoneNumber As String = departmentCode & roleDigit & counterDigits
        'lastAssignedNumber += 1

        Return phoneNumber
    End Function

    Private Shared Function GetDepartmentCode(ByVal department As String) As String
        Select Case department
            Case "Biosciences and Bioengineering"
                Return "901"
            Case "Chemical Engineering"
                Return "902"
            Case "Chemical Science and Technology"
                Return "903"
            Case "Civil Engineering"
                Return "904"
            Case "Computer Science & Engineering"
                Return "905"
            Case "Design"
                Return "906"
            Case "Electronics and Electrical Engineering"
                Return "907"
            Case "Humanities and Social Sciences"
                Return "908"
            Case "Mathematics"
                Return "909"
            Case "Mechanical Engineering"
                Return "910"
            Case "Physics"
                Return "911"
                ' Add more cases for additional departments if needed
            Case Else
                Return "000" ' Default code for unknown department
        End Select
    End Function

    Private Shared Function GetRoleDigit(ByVal role As String) As String
        Select Case role
            Case "Faculty"
                Return "0"
            Case "Student"
                Return "1"
                ' Add more cases for additional roles if needed
            Case Else
                Return "9" ' Default digit for unknown role
        End Select
    End Function

    Public Function last6Digits(ByVal department As String) As Integer
        Dim result As Integer

        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"
        Mysqlconn.Open()

        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = "SELECT phonenumber FROM deptPhoneNumberTracker WHERE department = @dept"

        ' Clear existing parameters before adding new ones
        sqlCmd.Parameters.Clear()

        sqlCmd.Parameters.AddWithValue("@dept", department)

        Try
            'Dim count As Integer = CInt(sqlCmd.ExecuteScalar())
            'result = (count)

            ' Read the current value
            Dim currentNumber As Integer = CInt(sqlCmd.ExecuteScalar())
            result = currentNumber

            ' Increment the value by 1
            Dim incrementedNumber As Integer = currentNumber + 1

            ' Update the database with the incremented value
            sqlCmd.CommandText = "UPDATE deptPhoneNumberTracker SET phonenumber = @incrementedNumber WHERE department = @dept"
            sqlCmd.Parameters.Clear()
            sqlCmd.Parameters.AddWithValue("@incrementedNumber", incrementedNumber)
            sqlCmd.Parameters.AddWithValue("@dept", department)

            ' Execute the update query
            sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)

        Finally
            Mysqlconn.Close()
        End Try

        Return result
    End Function

    Private Function GetCounterDigits(ByVal department As String) As String
        lastAssignedNumber = last6Digits(department)
        Return lastAssignedNumber.ToString("000000")
    End Function


    '////////////////////////////////////////////////

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        ' Your tracing logic here
        password_strength_check()
    End Sub

    Private Sub password_strength_check()
        Dim password As String = TextBox3.Text
        Dim password_len As Integer = password.Length

        ' Checking lower alphabet in string 
        Dim hasLower As Boolean = False, hasUpper As Boolean = False
        Dim hasDigit As Boolean = False, specialChar As Boolean = False
        Dim normalChars As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 "

        For i As Integer = 0 To password_len - 1
            If Char.IsLower(password(i)) Then
                hasLower = True
            End If
            If Char.IsUpper(password(i)) Then
                hasUpper = True
            End If
            If Char.IsDigit(password(i)) Then
                hasDigit = True
            End If

            Dim special As Integer = password.IndexOfAny(normalChars.ToCharArray())
            If special <> -1 Then
                specialChar = True
            End If
        Next

        If hasLower AndAlso hasUpper AndAlso hasDigit AndAlso specialChar AndAlso (password_len >= 8) Then
            Label9.Text = "Strong"
            Label9.ForeColor = Color.Green
            'Console.WriteLine("Strong")
        ElseIf (hasLower OrElse hasUpper) AndAlso specialChar AndAlso (password_len >= 6) Then
            Label9.Text = "Moderate"
            Label9.ForeColor = Color.Blue
            'Console.WriteLine("Moderate")
        Else
            Label9.Text = "Weak"
            Label9.ForeColor = Color.Red
            'Console.WriteLine("Weak")
        End If
    End Sub

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

    '///////////////////////////////////////////////////////////
    'Show password during typing functionality
    Private Sub btnTogglePasswordVisibility_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        If TextBox3.PasswordChar = "*"c Then
            ' Password is currently hidden, show it
            TextBox3.PasswordChar = ControlChars.NullChar
        Else
            ' Password is currently visible, hide it
            TextBox3.PasswordChar = "*"c
        End If
    End Sub




    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
        Form41.Show()
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click

    End Sub
End Class