Imports System.Data
Imports System.Data.OleDb

Public Class fkelompok2

    Public dv As DataView

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew

        orow("noid") = 0
        orow("nama_kelompok") = tnama.Text.Trim
        orow("ket") = tket.EditValue

        dv.EndInit()

    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn


            Dim sqlc As String = String.Format("select nama_kelompok from ms_kelompok where nama_kelompok='{0}'", tnama.Text.Trim)
            Dim sql_insert As String = String.Format("insert into ms_kelompok (nama_kelompok,ket) values('{0}','{1}')", tnama.Text.Trim, tket.Text.Trim)

            
            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim comd As OleDbCommand

            Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
            Dim dread As OleDbDataReader = cmdc.ExecuteReader

            Dim ada As Boolean = False

            If dread.Read Then
                If Not dread(0).ToString.Equals("") Then
                    ada = True
                End If
            End If
            dread.Close()

            If ada = False Then

                comd = New OleDbCommand(sql_insert, cn, sqltrans)
                comd.ExecuteNonQuery()

                'ClassMy.InsertToLog(cn, "btkelompok", 1, 0, 0, 0, tnama.Text.Trim, "", sqltrans)

                insertview()

                sqltrans.Commit()
                MsgBox("Data telah disimpan...", vbOKOnly + vbInformation, "Informasi")

                tnama.Text = ""
                tket.Text = ""

                tnama.Focus()

            Else
                MsgBox("Nama kelompok sudah ada..", vbOKOnly + vbInformation, "Informasi")
                tnama.Focus()
            End If

            '-------------------------

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try


    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tnama.Text.Trim.Length = 0 Then
            MsgBox("Nama tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tnama.Focus()
            Return
        End If

        simpan()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fkab2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tnama.Focus()
    End Sub

End Class