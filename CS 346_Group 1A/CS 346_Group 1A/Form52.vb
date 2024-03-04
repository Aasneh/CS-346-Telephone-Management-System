Imports MySql.Data.MySqlClient

Public Class Form52

    Public user_name As String = "Aasneh"
    Public phone As String = "9051000000"
    Public talktimeLeft As Integer = 0
    Public timeTalked As Integer = 0
    Public current_user As String = "p.aasneh@iitg.ac.in"
    Dim Mysqlconn As New MySqlConnection
    Dim sqlCmd As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlDt As New DataTable
    Dim Dta As New MySqlDataAdapter
    Dim SqlQuery As String


    Private Sub Form52_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        timeTalked = 0
        Label4.Text = timeTalked.ToString
        Label1.Text = user_name
        Label2.Text = phone
        Timer1.Dispose()
        Timer1.Interval = 1000
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        timeTalked = timeTalked + 1
        talktimeLeft = talktimeLeft - 1
        Label4.Text = timeTalked.ToString
        If talktimeLeft <= 0 Then
            Timer1.Stop()
            MessageBox.Show("Your talktime has finished!")
            Form51.talktime = talktimeLeft
            talktimeLeft = 0
            Form51.TalktimeLabel.Text = talktimeLeft.ToString & " Minutes"
            Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"

            Try
                Mysqlconn.Open()
                '.Show("Connection successful")
                sqlCmd.Connection = Mysqlconn
                sqlCmd.CommandText = "UPDATE userData SET talktimeLeft=" & talktimeLeft & " WHERE IITG_email ='" & current_user & "';"
                'sqlCmd.Parameters.AddWithValue("@EnteredEmail", enteredEmail)



                sqlRd = sqlCmd.ExecuteReader()
                sqlDt.Load(sqlRd)


                sqlRd.Close()
                Mysqlconn.Close() ' Close the connection after using it
            Catch ex As Exception
                MessageBox.Show("Error connecting to database: " & ex.Message)
            Finally
                Mysqlconn.Dispose() ' Dispose of the connection object
            End Try
            Me.Hide()
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Timer1.Stop()
        Form51.talktime = talktimeLeft
        Form51.TalktimeLabel.Text = talktimeLeft.ToString & " Minutes"
        Mysqlconn.ConnectionString = "server=localhost;userid=root;password=Aasneh18;database=TelephoneDatabase;"

        Try
            Mysqlconn.Open()
            '.Show("Connection successful")
            sqlCmd.Connection = Mysqlconn
            sqlCmd.CommandText = "UPDATE userData SET talktimeLeft=" & talktimeLeft & " WHERE IITG_email ='" & current_user & "';"
            'sqlCmd.Parameters.AddWithValue("@EnteredEmail", enteredEmail)



            sqlRd = sqlCmd.ExecuteReader()
            sqlDt.Load(sqlRd)

            sqlRd.Close()
            Mysqlconn.Close() ' Close the connection after using it
        Catch ex As Exception
            MessageBox.Show("Error connecting to database: " & ex.Message)
        Finally
            Mysqlconn.Dispose() ' Dispose of the connection object
        End Try
        Me.Hide()
    End Sub

    Private Sub Form52_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class