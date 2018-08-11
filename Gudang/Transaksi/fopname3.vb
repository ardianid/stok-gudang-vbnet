Imports System.Data
Imports System.Data.OleDb

Public Class fopname3

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private vqty1, vqty2 As Integer
    Private kqty_k, kqty_r, kqty_sl As Integer

    Private Sub kosongkan()

        tkode.Text = ""
        tnama.Text = ""

        tsat1.EditValue = ""
        tsat2.EditValue = ""

        tk1.EditValue = 0
        tk2.EditValue = 0

        tr1.EditValue = 0
        tr2.EditValue = 0

        ts1.EditValue = 0
        ts2.EditValue = 0

        vqty1 = 0
        vqty2 = 0


        kqty_k = 0
        kqty_r = 0
        kqty_sl = 0

    End Sub

    Private Sub kalkulasi()

        If vqty1 = 0 And vqty2 = 0 Then
            Return
        End If

        Dim totqty As Integer = vqty1 * vqty2

        Dim sr1 As Integer = 0
        Dim sr2 As Integer = 0

        If tr2.EditValue >= vqty2 Then

            Dim sisa2 As Integer = tr2.EditValue Mod totqty

            tr1.EditValue = tr1.EditValue + ((tr2.EditValue - sisa2) / totqty)
            tr2.EditValue = sisa2

        Else
            sr2 = tr2.EditValue
        End If

        sr1 = sr1 + (tr1.EditValue * totqty)

        kqty_r = sr1 + sr2

        kqty_sl = kqty_k - kqty_r

        Dim ssl1 As Integer
        Dim sel1 As Integer
        Dim sminus As Boolean = False

        ' jika hasilnya minus
        If kqty_sl < 0 Then

            totqty = -totqty

            If kqty_sl > totqty Then
                ssl1 = 0
                sel1 = kqty_sl
            Else
                sel1 = kqty_sl Mod totqty
                ssl1 = (kqty_sl - sel1) / totqty
                ssl1 = -ssl1
            End If

        Else

            ' jika hasilnya plus

            If kqty_sl < totqty Then
                ssl1 = 0
                sel1 = kqty_sl
            Else
                sel1 = kqty_sl Mod totqty
                ssl1 = (kqty_sl - sel1) / totqty
            End If

        End If

        ts1.EditValue = ssl1
        ts2.EditValue = sel1

    End Sub

    Private Sub insertview()

        Dim dta As DataTable = dv.ToTable

        Dim rows() As DataRow = dta.Select(String.Format("kd_barang='{0}'", tkode.EditValue))

        If rows.Length > 0 Then
            MsgBox("Barang sudah ada dalam daftar...", vbOKOnly + vbInformation, "Informasi")
            tkode.Focus()
            Return
        End If

        Dim orow As DataRowView = dv.AddNew
        orow("noid") = 0
        orow("kd_barang") = tkode.EditValue
        orow("nama_barang") = tnama.Text.Trim

        orow("qty_k1") = tk1.EditValue
        orow("qty_k2") = tk2.EditValue
        orow("qty_k") = kqty_k

        orow("qty_r1") = tr1.EditValue
        orow("qty_r2") = tr2.EditValue
        orow("qty_r") = kqty_r

        orow("qty_sel1") = ts1.EditValue
        orow("qty_sel2") = ts2.EditValue
        orow("qty_sel") = kqty_sl

        dv.EndInit()

        kosongkan()
        tkode.Focus()

    End Sub

    Private Sub tkode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkode.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_barang_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkode_Validated(sender As System.Object, e As System.EventArgs) Handles tkode.Validated
        If tkode.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Try

                cn = New OleDbConnection
                cn = ClassMy.open_conn

                Dim sql As String = String.Format("select kd_barang,nama_barang,satuan1,satuan2,qty1,qty2,jmlstok_f1 as jmlstok1,jmlstok_f2 as jmlstok2,jmlstok_f as jmlstok from ms_barang where kd_barang='{0}'", tkode.Text.Trim)
                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim drd As OleDbDataReader = cmd.ExecuteReader

                Dim ada As Boolean = False

                If drd.Read Then
                    If Not drd("kd_barang").ToString.Equals("") Then

                        ada = True
                        tkode.Text = drd("kd_barang").ToString
                        tnama.Text = drd("nama_barang").ToString

                        tsat1.EditValue = drd("satuan1").ToString
                        tsat2.EditValue = drd("satuan2").ToString

                        vqty1 = Integer.Parse(drd("qty1").ToString)
                        vqty2 = Integer.Parse(drd("qty2").ToString)

                        tk1.EditValue = drd("jmlstok1").ToString
                        tk2.EditValue = drd("jmlstok2").ToString
                        kqty_k = Integer.Parse(drd("jmlstok").ToString)

                        '   If tr1.EditValue > 0 Or tr2.EditValue > 0 Then
                        kalkulasi()
                        'End If

                    End If
                End If
                drd.Close()

                If ada = False Then
                    tkode.Text = ""
                    tnama.Text = ""

                    tsat1.EditValue = ""
                    tsat2.EditValue = ""

                    ts1.EditValue = 0
                    ts2.EditValue = 0
                    kqty_k = 0

                    tr1.EditValue = 0
                    tr2.EditValue = 0
                    kqty_r = 0

                    ts1.EditValue = 0
                    ts2.EditValue = 0
                    kqty_sl = 0

                    vqty1 = 0
                    vqty2 = 0

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

        tkode.EditValue = fs.get_KODE

        tkode_Validated(sender, Nothing)

    End Sub

    Private Sub tr1_LostFocus(sender As Object, e As System.EventArgs) Handles tr1.LostFocus
        kalkulasi()
    End Sub

    Private Sub tr1_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tr1.Validated
        kalkulasi()
    End Sub

    Private Sub tr2_LostFocus(sender As Object, e As System.EventArgs) Handles tr2.LostFocus
        kalkulasi()
    End Sub

    Private Sub tr2_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tr2.Validated
        kalkulasi()
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub ffaktur_to3_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tkode.Focus()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkode.EditValue = "" Then
            MsgBox("Barang harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkode.Focus()
            Return
        End If

        'If tr1.EditValue = 0 And tr2.EditValue = 0 Then
        '    MsgBox("Jml fisik tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
        '    tr1.Focus()
        '    Return
        'End If

        If addstat = True Then
            insertview()
        Else

        End If

    End Sub

End Class