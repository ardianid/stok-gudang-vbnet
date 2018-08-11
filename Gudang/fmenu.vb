Public Class fmenu 

    Private Sub SimpleButton3_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton3.Click

        'For Each f As Form In My.Application.OpenForms
        '    f.Close()
        'Next

        Application.Exit()

    End Sub


    Private Sub fmenu_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Me.Left = 0
        Me.Top = Screen.PrimaryScreen.Bounds.Height - (Me.Height * 2)

    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        Dim f2 As New finput
        f2.ShowDialog()
    End Sub

End Class