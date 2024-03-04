Imports MySql.Data.MySqlClient
Public Class Form32
    Dim isCollasped As Boolean = True
    Dim Mysqlconn As New MySqlConnection
    Dim sqlCmd As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlDt As New DataTable
    Dim Dta As New MySqlDataAdapter
    Dim SqlQuery As String

    Dim server As String = "localhost"
    Dim username As String = "root"
    Dim password As String = "Aasneh18"
    Dim database As String = "telephonedatabase"

    Public back_to As Integer = 1
    Public current_user As String = "p.aasneh@iitg.ac.in"

    Private Sub BackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackButton.Click
        If back_to = 0 Then
            Me.Close()
            Form61.Show()
        ElseIf back_to = 1 Then
            Form31.current_user = current_user
            Me.Close()
            Form31.Show()
        End If
    End Sub

    Private Sub Form32_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = 1300
        Button16.Text = "Column Chart"
        Label6.Text = "Department vs Number of Users"
        Label5.Text = "Department Stats"
        Panel1.Size = Panel1.MinimumSize
        createChart()
        loadStats()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        createChart()
        loadStats()
        changeColor()
        Button1.BackColor = Color.LightSteelBlue
        Label5.Text = "Department Stats"
        Label6.Text = "Department vs Number of Users"
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        planChart()
        loadStats()
        changeColor()
        Button3.BackColor = Color.LightSteelBlue
        Label6.Text = "Plan Name vs Number of Users"
        Label5.Text = "Plan Statistics"
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If isCollasped Then
            Panel1.Size = Panel1.MaximumSize
            isCollasped = False
        Else
            Panel1.Size = Panel1.MinimumSize
            isCollasped = True
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        changeColor()
        Button4.BackColor = Color.LightSteelBlue
        Label6.Text = "Plan Name vs Number of Users"
        Label5.Text = "Plan Statistics"
        planChart()
        loadStats()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        changeColor()
        Button5.BackColor = Color.LightSteelBlue
        Label6.Text = "Plan Name vs Number of Users"
        Label5.Text = "Chemical Science and Technology"
        planChart_temp("Chemical Science and Technology")
        loadStats_temp("Chemical Science and Technology")
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        changeColor()
        Button6.BackColor = Color.LightSteelBlue
        Label6.Text = "Plan Name vs Number of Users"
        Label5.Text = "Biosciences and Bioengineering"
        planChart_temp("Biosciences and Bioengineering")
        loadStats_temp("Biosciences and Bioengineering")
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        changeColor()
        Button7.BackColor = Color.LightSteelBlue
        Label6.Text = "Plan Name vs Number of Users"
        Label5.Text = "Chemical Engineering"
        planChart_temp("Chemical Engineering")
        loadStats_temp("Chemical Engineering")
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        changeColor()
        Button8.BackColor = Color.LightSteelBlue
        Label6.Text = "Plan Name vs Number of Users"
        Label5.Text = "Civil Engineering"
        planChart_temp("Civil Engineering")
        loadStats_temp("Civil Engineering")
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        changeColor()
        Button9.BackColor = Color.LightSteelBlue
        Label6.Text = "Plan Name vs Number of Users"
        Label5.Text = "Computer Science & Engineering"
        planChart_temp("Computer Science & Engineering")
        loadStats_temp("Computer Science & Engineering")
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        changeColor()
        Button10.BackColor = Color.LightSteelBlue
        Label6.Text = "Plan Name vs Number of Users"
        Label5.Text = "Design"
        planChart_temp("Design")
        loadStats_temp("Design")
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        changeColor()
        Button11.BackColor = Color.LightSteelBlue
        Label6.Text = "Plan Name vs Number of Users"
        Label5.Text = "Electronics and Electrical Engineering"
        planChart_temp("Electronics and Electrical Engineering")
        loadStats_temp("Electronics and Electrical Engineering")
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        changeColor()
        Button12.BackColor = Color.LightSteelBlue
        Label6.Text = "Plan Name vs Number of Users"
        Label5.Text = "Humanities and Social Sciences"
        planChart_temp("Humanities and Social Sciences")
        loadStats_temp("Humanities and Social Sciences")
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        changeColor()
        Button13.BackColor = Color.LightSteelBlue
        Label6.Text = "Plan Name vs Number of Users"
        Label5.Text = "Mathematics"
        planChart_temp("Mathematics")
        loadStats_temp("Mathematics")
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        changeColor()
        Button14.BackColor = Color.LightSteelBlue
        Label6.Text = "Plan Name vs Number of Users"
        Label5.Text = "Mechanical Engineering"
        planChart_temp("Mechanical Engineering")
        loadStats_temp("Mechanical Engineering")
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        changeColor()
        Button15.BackColor = Color.LightSteelBlue
        Label6.Text = "Plan Name vs Number of Users"
        Label5.Text = "Physics"
        planChart_temp("Physics")
        loadStats_temp("Physics")
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        If Chart1.Series.Count > 0 Then
            Select Case Chart1.Series(0).ChartType
                Case DataVisualization.Charting.SeriesChartType.Column
                    Chart1.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.Bar
                    Button16.Text = "Bar Chart"
                Case DataVisualization.Charting.SeriesChartType.Bar
                    Chart1.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.Pie
                    Button16.Text = "Pie Chart"
                Case DataVisualization.Charting.SeriesChartType.Pie
                    Chart1.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.Column
                    Button16.Text = "Column Chart"
                Case Else
                    ' Set a default chart type
                    Chart1.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.Column
                    Button16.Text = "Column Chart"
            End Select
        End If
    End Sub

    Private Sub changeColor()
        Button1.BackColor = Color.Gainsboro
        Button2.BackColor = Color.Gainsboro
        Button3.BackColor = Color.Gainsboro
        Button4.BackColor = Color.Gainsboro
        Button5.BackColor = Color.Gainsboro
        Button6.BackColor = Color.Gainsboro
        Button7.BackColor = Color.Gainsboro
        Button8.BackColor = Color.Gainsboro
        Button9.BackColor = Color.Gainsboro
        Button10.BackColor = Color.Gainsboro
        Button11.BackColor = Color.Gainsboro
        Button12.BackColor = Color.Gainsboro
        Button13.BackColor = Color.Gainsboro
        Button14.BackColor = Color.Gainsboro
        Button15.BackColor = Color.Gainsboro
    End Sub

    Private Sub createChart()
        Mysqlconn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"
        Mysqlconn.Open()

        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = "SELECT department, COUNT(*) AS department_count FROM UserData GROUP BY department ORDER BY department;"

        sqlRd = sqlCmd.ExecuteReader

        ' Clear existing data in the chart
        Chart1.Series("chart").Points.Clear()

        ' Process the data and populate the chart
        While sqlRd.Read()
            Dim department As String = sqlRd.GetString("department")
            Dim count As Integer = sqlRd.GetInt32("department_count")

            ' Convert the department name to initials
            Dim departmentInitials As String = GetDepartmentInitials(department)

            ' Add data to the chart
            Chart1.Series("chart").Points.AddXY(departmentInitials, count)
        End While

        sqlRd.Close()
        Mysqlconn.Close()
        'MessageBox.Show("Successful Connection")
    End Sub

    Private Function GetDepartmentInitials(ByVal department As String) As String
        ' Add a mapping or use a switch statement to get the initials based on the department name
        Select Case department
            Case "Biosciences and Bioengineering"
                Return "BSBE"
            Case "Chemical Engineering"
                Return "CE"
            Case "Chemical Science and Technology"
                Return "CST"
            Case "Civil Engineering"
                Return "Civil"
            Case "Computer Science & Engineering"
                Return "CSE"
            Case "Design"
                Return "DOD"
            Case "Electronics and Electrical Engineering"
                Return "EEE"
            Case "Humanities and Social Sciences"
                Return "HSS"
            Case "Mathematics"
                Return "MNC"
            Case "Mechanical Engineering"
                Return "ME"
            Case "Physics"
                Return "EP"
                ' Add more cases as needed
            Case Else
                Return department ' Use the original department name if not found in the mapping
        End Select
    End Function

    Private Function GetPlan(ByVal departmentCode As Integer) As String
        ' Add a mapping or use a switch statement to get the initials based on the department code
        Select Case departmentCode
            Case 1
                Return "Basic"
            Case 2
                Return "Standard"
            Case 3
                Return "Premium"
            Case 4
                Return "Ultra"
            Case 5
                Return "Ultimate"
            Case 0
                Return "No Plan"
            Case Else
                Return departmentCode.ToString() ' Use the original department code as a string if not found in the mapping
        End Select
    End Function



    Private Sub planChart()
        Mysqlconn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"
        Mysqlconn.Open()

        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = "SELECT plan_id, COUNT(*) AS plan_count FROM UserData GROUP BY plan_id ORDER BY plan_id;"

        sqlRd = sqlCmd.ExecuteReader

        ' Clear existing data in the chart
        Chart1.Series("chart").Points.Clear()

        ' Process the data and populate the chart
        While sqlRd.Read()
            Dim department As Integer = sqlRd.GetInt32("plan_id")
            Dim count As Integer = sqlRd.GetInt32("plan_count")
            Dim planName As String = GetPlan(department)

            ' Add data to the chart
            Chart1.Series("chart").Points.AddXY(planName, count)
        End While


        sqlRd.Close()
        Mysqlconn.Close()
        'MessageBox.Show("Successful Connection")
    End Sub

    Private Sub loadStats()
        Mysqlconn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"
        Mysqlconn.Open()

        sqlCmd.Connection = Mysqlconn
        sqlCmd.CommandText = "SELECT COUNT(*) AS total_users, " &
                             "SUM(CASE WHEN role = 'Student' THEN 1 ELSE 0 END) AS student_count, " &
                             "SUM(CASE WHEN role = 'Faculty' THEN 1 ELSE 0 END) AS faculty_count " &
                             "FROM UserData;"

        sqlRd = sqlCmd.ExecuteReader

        If sqlRd.Read() Then
            ' Display total users count
            RichTextBox1.Text = sqlRd("total_users").ToString()

            ' Display student count
            RichTextBox2.Text = sqlRd("student_count").ToString()

            ' Display faculty count
            RichTextBox3.Text = sqlRd("faculty_count").ToString()
        End If

        sqlRd.Close()
        Mysqlconn.Close()
    End Sub


    Private Sub planChart_temp(ByVal departmentName As String)
        Mysqlconn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"
        Mysqlconn.Open()

        sqlCmd.Connection = Mysqlconn
        sqlCmd.Parameters.Clear()
        ' Use parameterized query to prevent SQL injection
        sqlCmd.CommandText = "SELECT plan_id, COUNT(*) AS plan_count FROM UserData WHERE department = @DepartmentName GROUP BY plan_id ORDER BY plan_id;"
        sqlCmd.Parameters.AddWithValue("@DepartmentName", departmentName)

        sqlRd = sqlCmd.ExecuteReader

        ' Clear existing data in the chart
        Chart1.Series("chart").Points.Clear()

        ' Process the data and populate the chart
        While sqlRd.Read()
            Dim planId As Integer = sqlRd.GetInt32("plan_id")
            Dim count As Integer = sqlRd.GetInt32("plan_count")
            Dim planName As String = GetPlan(planId)
            ' Add data to the chart
            Chart1.Series("chart").Points.AddXY(planName, count)
        End While

        sqlRd.Close()
        Mysqlconn.Close()
        'MessageBox.Show("Successful Connection")
    End Sub


    Private Sub loadStats_temp(ByVal departmentName As String)
        Mysqlconn.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";"
        Mysqlconn.Open()

        sqlCmd.Connection = Mysqlconn

        ' Clear existing parameters
        sqlCmd.Parameters.Clear()

        ' Use parameterized query to prevent SQL injection
        sqlCmd.CommandText = "SELECT COUNT(*) AS total_users, " &
                             "SUM(CASE WHEN role = 'Student' THEN 1 ELSE 0 END) AS student_count, " &
                             "SUM(CASE WHEN role = 'Faculty' THEN 1 ELSE 0 END) AS faculty_count " &
                             "FROM UserData WHERE department = @DepartmentName;"
        sqlCmd.Parameters.AddWithValue("@DepartmentName", departmentName)

        sqlRd = sqlCmd.ExecuteReader

        If sqlRd.Read() Then
            ' Display total users count
            RichTextBox1.Text = sqlRd("total_users").ToString()

            ' Display student count
            RichTextBox2.Text = sqlRd("student_count").ToString()

            ' Display faculty count
            RichTextBox3.Text = sqlRd("faculty_count").ToString()
        End If

        sqlRd.Close()
        Mysqlconn.Close()
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class