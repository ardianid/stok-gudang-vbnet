Imports System.Data
Imports System.Data.OleDb

Public Class fkelompok

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub opendata()

        Const sql As String = "select * from ms_kelompok"
        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            dv1 = Nothing

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            ds = New DataSet()
            ds = ClassMy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1 = New BindingSource
            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1


        Catch ex As OleDb.OleDbException
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub hapus()



        Dim sql As String = String.Format("delete from ms_kelompok where nama_kelompok='{0}'", dv1(Me.BindingContext(bs1).Position)("nama_kelompok").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            '  Clsmy.InsertToLog(cn, "btkelompok", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nama_kelompok").ToString, "", sqltrans)

            sqltrans.Commit()

            opendata()

            MsgBox("Data telah dihapus...", vbOKOnly + vbInformation, "Informasi")

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try


    End Sub

    'Private Sub Get_Aksesform()

    '    Dim rows() As DataRow = dtmenu.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

    '    If Convert.ToInt16(rows(0)("t_add")) = 1 Then
    '        tsadd.Enabled = True
    '    Else
    '        tsadd.Enabled = False
    '    End If

    '    If Convert.ToInt16(rows(0)("t_del")) = 1 Then
    '        tsdel.Enabled = True
    '    Else
    '        tsdel.Enabled = False
    '    End If

    'End Sub

    Private Sub fuser_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        opendata()
        '   Get_Aksesform()
    End Sub

    Private Sub tsref_Click(sender As System.Object, e As System.EventArgs) Handles tsref.Click
        opendata()
    End Sub

    Private Sub tsdel_Click(sender As System.Object, e As System.EventArgs) Handles tsdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        If MsgBox("Yakin akan dihapus ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click
        Using fkar2 As New fkelompok2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1}
            fkar2.ShowDialog()
        End Using

    End Sub


End Class