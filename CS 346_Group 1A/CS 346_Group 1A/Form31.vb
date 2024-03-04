Public Class Form31
    ' Variables to store selected plan details
    Public selectedPlan As String
    Public selectedCost As String
    Public selectedData As String
    Public selectedTalktime As String
    Public current_user As String = "p.aasneh@iitg.ac.in"
    Public plan_id As Integer
    Public expiry_date As String = "18-12-2024"


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        selectedPlan = "Basic Plan"
        selectedCost = "Rs 20"
        selectedData = "10GB"
        selectedTalktime = "100 minutes"
        RichTextBox1.Text = selectedCost
        'Label1.Location = New Point((Me.Width - Label1.Width) / 2, Label1.Location.Y)
    End Sub
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub BackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackButton.Click
        Form51.current_user = current_user
        Me.Hide()
        Form51.Show()
        'Application.OpenForms("Form61").Show()
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Set selected plan details when Button1 is clicked
        selectedPlan = "Basic Plan"
        selectedCost = "Rs 20"
        selectedData = "10"
        selectedTalktime = "100"
        plan_id = 1
        expiry_date = "03-04-2024"

        ' Update TextBox with the selected plan's cost
        RichTextBox1.Text = selectedCost
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' Set selected plan details when Button2 is clicked
        selectedPlan = "Standard Plan"
        selectedCost = "Rs 30"
        selectedData = "20"
        selectedTalktime = "200"
        plan_id = 2
        expiry_date = "18-04-2024"

        ' Update TextBox with the selected plan's cost
        RichTextBox1.Text = selectedCost
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ' Set selected plan details when Button3 is clicked
        selectedPlan = "Premium Plan"
        selectedCost = "Rs 40"
        selectedData = "30"
        selectedTalktime = "300"
        plan_id = 3
        expiry_date = "03-05-2024"

        ' Update TextBox with the selected plan's cost
        RichTextBox1.Text = selectedCost
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        ' Set selected plan details when Button4 is clicked
        selectedPlan = "Ultra Plan"
        selectedCost = "Rs 50"
        selectedData = "40"
        selectedTalktime = "400"
        plan_id = 4
        expiry_date = "02-06-2024"
        ' Update TextBox with the selected plan's cost
        RichTextBox1.Text = selectedCost
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ' Set selected plan details when Button5 is clicked
        selectedPlan = "Unlimited Plan"
        selectedCost = "Rs 100"
        selectedData = "1000"
        selectedTalktime = "1000"
        plan_id = 5
        expiry_date = "03-03-2025"
        ' Update TextBox with the selected plan's cost
        RichTextBox1.Text = selectedCost
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ' Display selected plan details in a message box when Button6 is clicked
        'Dim message As String = "Selected Plan: " & selectedPlan & vbCrLf &
        '                      "Cost: " & selectedCost & vbCrLf &
        '                     "Data: " & selectedData & vbCrLf &
        '                    "Talktime: " & selectedTalktime
        Dim form11 As New Form11()
        Me.Hide()
        form11.current_user = current_user
        form11.pay_amount = selectedCost
        form11.plan_id = plan_id
        form11.talktimeLeft = selectedTalktime
        form11.dataLeft = selectedData
        form11.expiry_date = expiry_date
        'MessageBox.Show(message, "Selected Plan Details", MessageBoxButtons.OK, MessageBoxIcon.Information)

        'selectedPlan = ""
        'selectedCost = ""
        'selectedData = ""
        'selectedTalktime = ""
        'RichTextBox1.Text = ""
        form11.Show()
    End Sub

    Private Sub RectangleShape1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RectangleShape1.Click

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Form32.current_user = current_user
        Form32.back_to = 1
        Me.Hide()
        Form32.Show()
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub
End Class