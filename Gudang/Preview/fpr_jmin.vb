Imports System.Data.OleDb

Public Class fpr_jmin

    Public jenislap As Integer

    Private Sub tkode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_barang.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_barang_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkode_Validated(sender As System.Object, e As System.EventArgs) Handles tkd_barang.Validated
        If tkd_barang.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Try

                cn = New OleDbConnection
                cn = ClassMy.open_conn

                Dim sql As String = String.Format("select kd_barang,nama_barang,satuan1,satuan2,qty1,qty2,hargabeli from ms_barang where kd_barang='{0}'", tkd_barang.Text.Trim)
                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim drd As OleDbDataReader = cmd.ExecuteReader

                Dim ada As Boolean = False

                If drd.Read Then
                    If Not drd("kd_barang").ToString.Equals("") Then

                        ada = True
                        tkd_barang.Text = drd("kd_barang").ToString
                        tbarang.Text = drd("nama_barang").ToString

                    End If
                End If
                drd.Close()

                If ada = False Then
                    tkd_barang.Text = ""
                    tbarang.Text = ""
                End If

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally

                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try

        End If
    End Sub

    Private Sub bts_barang_Click(sender As System.Object, e As System.EventArgs) Handles bts_barang.Click

        Dim fs As New fsbarang With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_barang.EditValue = fs.get_KODE

        tkode_Validated(sender, Nothing)

    End Sub

    Private Sub fpr_jmin_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_jmin_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If jenislap = 0 Then
            Me.Text = "Lap Barang Masuk"
        Else
            Me.Text = "Lap Barang Keluar"
        End If

        ttgl.EditValue = DateValue(Date.Now)
        ttgl2.EditValue = DateValue(Date.Now)
    End Sub

    Private Sub btkeluar_Click(sender As System.Object, e As System.EventArgs) Handles btkeluar.Click
        Me.Close()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Dim sql As String = ""

        If jenislap = 0 Then
            sql = String.Format("SELECT tradm_gud.nobukti, tradm_gud.tanggal, tradm_gud.nobukti_trans, tradm_gud.nopol, tradm_gud2.kd_barang, ms_barang.nama_lap as nama_barang, tradm_gud2.qtyin as qty, tradm_gud2.satuan " & _
          "FROM (tradm_gud INNER JOIN tradm_gud2 ON tradm_gud.nobukti = tradm_gud2.nobukti) INNER JOIN ms_barang ON tradm_gud2.kd_barang = ms_barang.kd_barang " & _
          " WHERE tradm_gud2.qtyin > 0 and tradm_gud.sbatal=0 and  tradm_gud.tanggal>=#{0}# and tradm_gud.tanggal<=#{1}#", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        Else
            sql = String.Format("SELECT tradm_gud.nobukti, tradm_gud.tanggal, tradm_gud.nobukti_trans, tradm_gud.nopol, tradm_gud2.kd_barang, ms_barang.nama_lap as nama_barang, tradm_gud2.qtyout as qty, tradm_gud2.satuan " & _
          "FROM (tradm_gud INNER JOIN tradm_gud2 ON tradm_gud.nobukti = tradm_gud2.nobukti) INNER JOIN ms_barang ON tradm_gud2.kd_barang = ms_barang.kd_barang " & _
          " WHERE tradm_gud2.qtyout > 0 and tradm_gud.sbatal=0 and  tradm_gud.tanggal>=#{0}# and tradm_gud.tanggal<=#{1}#", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        End If

        If tbukti.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and tradm_gud.nobukti like '%{1}%'", sql, tbukti.Text.Trim)
        End If

        If tnota.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and tradm_gud.nobukti_trans like '%{1}%'", sql, tnota.Text.Trim)
        End If

        If tnopol.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and tradm_gud.nopol like '%{1}%'", sql, tnopol.Text.Trim)
        End If

        If tkd_barang.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and tradm_gud2.kd_barang='{1}'", sql, tkd_barang.Text.Trim)
        End If

        Dim periode As String = String.Format("Periode : {0} s.d {1}", ttgl.Text, ttgl2.Text)

        Using fkar2 As New fpr_jmin2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .periode = periode, .jenislap = jenislap}
            fkar2.ShowDialog(Me)
        End Using

    End Sub

    Private Sub tkd_barang_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_barang.LostFocus
        If tkd_barang.Text.Trim.Length = 0 Then
            tbarang.Text = ""
        End If
    End Sub

End Class