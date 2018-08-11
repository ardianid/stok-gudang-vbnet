Imports System.Data
Imports System.Data.OleDb

Public Class finput

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private statmanipulate As Boolean

    Private Sub opendata()

        Dim tglawal As String = DateAdd(DateInterval.Month, -5, Date.Now)
        Dim tglperiod2 As String = DateAdd(DateInterval.Day, +15, Date.Now)

        Dim sql As String = String.Format("select nobukti,tanggal,nobukti_trans,nopol,snote,sbatal from tradm_gud " & _
            "where tradm_gud.tanggal >=#{0}# and  tradm_gud.tanggal <=#{1}#", convert_date_to_eng(tglawal), convert_date_to_eng(tglperiod2))


        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            dv1 = Nothing

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim ds As DataSet = New DataSet()
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

        If dv1.Count > 0 Then
            bs1.MoveLast()
            opendata_br()
        Else
            opendata_br()
        End If

    End Sub

    Private Sub cari()

        'bs1.DataSource = Nothing
        grid1.DataSource = Nothing
        Dim cn As OleDbConnection = Nothing

        Dim sql As String = ""
        Dim scbo As Integer = tcbofind.SelectedIndex

        Dim tglawal As String = DateAdd(DateInterval.Year, -1, Date.Now)
        Dim tglperiod2 As String = Date.Now

        sql = String.Format("select nobukti,tanggal,nobukti_trans,nopol,snote,sbatal from tradm_gud " & _
            "where tradm_gud.tanggal >=#{0}# and  tradm_gud.tanggal <=#{1}#", convert_date_to_eng(tglawal), convert_date_to_eng(tglperiod2))

        Select Case tcbofind.SelectedIndex
            Case 0 ' nobukti
                sql = String.Format("{0} and nobukti like '%{1}%'", sql, tfind.Text.Trim)
            Case 1 ' tanggal

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and tanggal=#{1}#", sql, convert_date_to_eng(tfind.Text.Trim))
           
            Case 2 ' no trans
                sql = String.Format("{0} and nobukti_trans like '%{1}%'", sql, tfind.Text.Trim)
            Case 3 ' no polisi
                sql = String.Format("{0} and nopol like '%{1}%'", sql, tfind.Text.Trim)
            Case 4
                sql = String.Format("{0} and nobukti in (select nobukti from tradm_gud2 inner join ms_barang on tradm_gud2.kd_barang=ms_barang.kd_barang where ms_barang.nama_barang like '%{1}%')", sql, tfind.Text.Trim)
            Case 5
                sql = String.Format("{0} and snote like '%{1}%'", sql, tfind.Text.Trim)
        End Select

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim ds As DataSet = New DataSet()
            ds = ClassMy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

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

    Private Sub opendata_br()

        grid2.DataSource = Nothing
        dv2 = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim sql As String = String.Format("select ms_barang.kd_barang,ms_barang.nama_barang,tradm_gud2.qtyin,tradm_gud2.qtyout,tradm_gud2.satuan " & _
        "from tradm_gud2 inner join ms_barang on tradm_gud2.kd_barang=ms_barang.kd_barang " & _
        "where tradm_gud2.nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim ds As DataSet = New DataSet()
            ds = ClassMy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            grid2.DataSource = dv2

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

        Dim alasan = InputBox("Alasan Batal : ", "Alasan")

        If alasan = "" Then
            Return
        End If


        Dim sql As String = String.Format("update tradm_gud set sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan.ToUpper, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing
        Dim cmdtoko As OleDbCommand = Nothing

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            If hapus_detail(cn, sqltrans) = True Then
                '  Clsmy.InsertToLog(cn, "btadm_g", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("tanggal").ToString, sqltrans)

                sqltrans.Commit()

                dv1(bs1.Position)("sbatal") = 1

                MsgBox("Data telah dibatalkan...", vbOKOnly + vbInformation, "Informasi")
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


    End Sub

    Private Function hapus_detail(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As Boolean

        Dim hasil As Boolean = True

        Dim nobukti As String = dv1(Me.BindingContext(bs1).Position)("nobukti").ToString
        Dim nobukti2 As String = dv1(Me.BindingContext(bs1).Position)("nobukti_trans").ToString
        Dim nopol As String = dv1(Me.BindingContext(bs1).Position)("nopol").ToString
        Dim tanggal As String = dv1(Me.BindingContext(bs1).Position)("tanggal").ToString

        Dim sql As String = String.Format("select * from tradm_gud2 where nobukti='{0}'", nobukti)

        Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = comd.ExecuteReader

        If drd.HasRows Then
            While drd.Read

                Dim qtyinkecil As Integer = Integer.Parse(drd("qtyinkecil").ToString)
                Dim qtyoutkecil As Integer = Integer.Parse(drd("qtyoutkecil").ToString)

                Dim qtyinkecil_bad As Integer = Integer.Parse(drd("qtyinkecil_bad").ToString)

                Dim kdbar As String = drd("kd_barang").ToString

                If IsNumeric(drd("noid").ToString) Then


                    If qtyinkecil > 0 Then
                        Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyinkecil, kdbar, "G000", False, False, False)
                        If Not hasilplusmin.Equals("ok") Then
                            MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                            hasil = False
                            Exit While
                        End If

                        ''3. insert to hist stok
                        ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, nobukti, nobukti2, nopol, tanggal, "G000", kdbar, 0, qtyinkecil, "Delete", "")

                    End If

                    If qtyoutkecil > 0 Then
                        Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyoutkecil, kdbar, "G000", True, False, False)
                        If Not hasilplusmin.Equals("ok") Then
                            MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                            hasil = False
                            Exit While
                        End If

                        ''3. insert to hist stok
                        ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, nobukti, nobukti2, nopol, tanggal, "G000", kdbar, qtyoutkecil, 0, "Delete", "")

                    End If


                End If

            End While
        End If

        drd.Close()

        Return hasil

    End Function

    Private Sub fuser_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        tcbofind.SelectedIndex = 0
        opendata()

    End Sub

    Private Sub tsfind_Click(sender As System.Object, e As System.EventArgs) Handles tsfind.Click
        cari()
    End Sub

    Private Sub tfind_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyDown
        If e.KeyCode = 13 Then
            cari()
        End If
    End Sub

    Private Sub tsref_Click(sender As System.Object, e As System.EventArgs) Handles tsref.Click
        tfind.Text = ""
        opendata()
    End Sub

    Private Sub tsdel_Click(sender As System.Object, e As System.EventArgs) Handles tsdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        'If cek_sambil() = True Then
        '    MsgBox("Data telah digunakan transaksi lainnya...", vbOKOnly + vbInformation, "Informasi")
        '    Return
        'End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If MsgBox("Yakin akan dibatalkan ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click

        statmanipulate = True
        Using fkar2 As New finput2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fkar2.ShowDialog()
            statmanipulate = False
        End Using
    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If


        'If cek_sambil() = True Then
        '    MsgBox("Data telah digunakan transaksi lainnya...", vbOKOnly + vbInformation, "Informasi")
        '    Return
        'End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        statmanipulate = True
        Using fkar2 As New finput2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.ShowDialog()
            statmanipulate = False
        End Using

    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        statmanipulate = True
        Using fkar2 As New finput2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.btsimpan.Enabled = False
            fkar2.btadd.Enabled = False
            fkar2.btdel.Enabled = False
            fkar2.tbukti_tr.Enabled = False
            fkar2.ShowDialog()
            statmanipulate = False
        End Using

    End Sub

    Private Sub GridView1_Click(sender As Object, e As System.EventArgs) Handles GridView1.Click

        If statmanipulate Then
            Return
        End If

        opendata_br()

    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged

        If statmanipulate Then
            Return
        End If

        opendata_br()
    End Sub

    Private Sub GridView1_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView1.RowCellClick

        If statmanipulate Then
            Return
        End If

        opendata_br()
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick

        If statmanipulate Then
            Return
        End If

        opendata_br()
    End Sub


End Class