Imports System.Data
Imports System.Data.OleDb
Imports DevExpress.XtraBars

Public Class login

    Dim fmfile As Integer = 0
    Dim fmmaster As Integer = 0
    Dim fmtransaksi As Integer = 0

    Private Sub login_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        tuser.Focus()
    End Sub

    Private Sub btbatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btbatal.Click
        Application.Exit()
    End Sub

    Private Sub btmasuk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btmasuk.Click

        If tuser.Text = "" Then
            MsgBox("Nama user harus diisi", MsgBoxStyle.Information, "Informasi")
            tuser.Focus()
            Exit Sub
        End If

        If tpwd.Text = "" Then
            MsgBox("Password harus diisi", MsgBoxStyle.Information, "Informasi")
            tpwd.Focus()
            Exit Sub
        End If


        Dim cn As OleDbConnection = New OleDbConnection
        userprog = tuser.Text.Trim
        pwd = tpwd.Text.Trim

        Try


            cn = ClassMy.open_conn

            Dim sql As String = String.Format("select * from ms_usersys where namauser='{0}' and pwd='{1}'", tuser.Text.Trim, tpwd.Text.Trim)
            Dim comd = New OleDbCommand(sql, cn)
            Dim dre As OleDbDataReader = comd.ExecuteReader

            If dre.Read Then

                If Not dre(0).ToString.Equals("") Then


                    fmenu2.fmaster.Enabled = True
                    fmenu2.ftrans.Enabled = True
                    fmenu2.flap.Enabled = True

                    Me.Close()


                End If

            Else
                MsgBox("User/Password tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                tuser.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Informasi")

        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                    cn.Dispose()
                End If
            End If

        End Try


    End Sub

    Private Sub tuser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tuser.KeyDown
        If e.KeyCode = 13 Then
            tpwd.Focus()
        End If
    End Sub

    Private Sub tpwd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tpwd.KeyDown
        If e.KeyCode = 13 Then
            btmasuk.PerformClick()
        End If
    End Sub



End Class