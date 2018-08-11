Imports System.Data
Imports System.Data.OleDb
Public Class fopname

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private isload_detail As Boolean = False

    Private Sub opendata()

        Dim tglsebelum As String = DateAdd(DateInterval.Month, -3, Date.Now)

        Dim sql As String = String.Format("select * from tr_opname where tanggal>=#{0}#  order by tanggal,nobukti ", convert_date_to_eng(tglsebelum))

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

    Private Sub opendata2()

        grid2.DataSource = Nothing
        dv2 = Nothing

        If isload_detail = True Then
            Return
        End If

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim sql As String = String.Format("SELECT ms_barang.kd_barang, ms_barang.nama_barang, tr_opname2.qty_k1, tr_opname2.qty_k2, tr_opname2.qty_r1, tr_opname2.qty_r2, tr_opname2.qty_sel1,tr_opname2.qty_sel2 " & _
        "FROM tr_opname2 INNER JOIN ms_barang ON tr_opname2.kd_barang = ms_barang.kd_barang " & _
        "WHERE tr_opname2.nobukti='{0}' ", dv1(bs1.Position)("nobukti").ToString)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn


            Dim ds2 As DataSet
            ds2 = New DataSet()
            ds2 = ClassMy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds2)
            dv2 = dvmanager2.CreateDataView(ds2.Tables(0))

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

    Private Sub cari()

        'bs1.DataSource = Nothing
        grid1.DataSource = Nothing
        Dim cn As OleDbConnection = Nothing

        Dim sql As String = ""
        Dim scbo As Integer = tcbofind.SelectedIndex

        sql = "select * from tr_opname"

        Select Case tcbofind.SelectedIndex
            Case 0 ' kode
                sql = String.Format("{0} where nobukti='{1}'", sql, tfind.Text.Trim)
            Case 1
                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} where tanggal=#{1}#", sql, convert_date_to_eng(tfind.Text.Trim))
            Case 2
                sql = String.Format("{0} where note like '%{1}%'", sql, tfind.Text.Trim)
        End Select

        'If Not IsDate(tfind.Text.Trim) Then

        '    Dim tglsebelum As String = DateAdd(DateInterval.Month, -3, Date.Now)

        '    sql = String.Format("{0} and tanggal>='{1}'", sql, convert_date_to_eng(tglsebelum))

        'End If

        sql = String.Format(" {0} order by tanggal,nobukti", sql)

        Try


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

    Private Sub hapus()

        'Dim sqlcekuser As String = String.Format("select namauser from ms_usersys where namauser='{0}' and jenisuser='Admin'", userprog)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            If DateValue(Date.Now) <> DateValue(dv1(bs1.Position)("tanggal").ToString) Then
                MsgBox("Tanggal sudah tidak sesuai dengan tanggal opname...", vbOKOnly + vbInformation, "Informasi")
                Return
            End If

            Dim alasan_batal As String = InputBox("Alasan Batal : ", "Konfirmasi")
            If alasan_batal.Trim.Equals("") Then
                ' close_wait()
                MsgBox("Alasan Batal harus diisi...", vbOKOnly + vbInformation, "Informasi")
                Return
            End If

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim sql As String = String.Format("update tr_opname set sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan_batal.ToUpper, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString)

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            'ClassMy.InsertToLog(cn, "btopname", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("tanggal").ToString, sqltrans)

            If hapus2(cn, sqltrans) = True Then
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

    Private Function hapus2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As Boolean

        Dim hasil As Boolean = True

        Dim nobukti As String = dv1(bs1.Position)("nobukti").ToString
        Dim tanggal As String = dv1(bs1.Position)("tanggal").ToString

        Dim sql As String = String.Format("select * from tr_opname2 where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.HasRows Then
            While drd.Read

                Dim qtykecil As Integer = Integer.Parse(drd("qty_sel").ToString)
                Dim qtykecil_akh As Integer = Integer.Parse(drd("qty_r").ToString)
                Dim kdbar As String = drd("kd_barang").ToString


                If IsNumeric(drd("noid").ToString) Then

                    Dim sqlcek As String = String.Format("select jmlstok_f from ms_barang where kd_barang='{0}'", kdbar)
                    Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
                    Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader
                    If drdcek.Read Then
                        If IsNumeric(drdcek(0).ToString) Then

                            Dim jmlsek As Integer = Integer.Parse(drdcek(0).ToString)

                            If jmlsek <> qtykecil_akh Then
                                '     close_wait()
                                MsgBox("Stok sudah tidak sesuai dengan stok akhir opname", vbOKOnly + vbInformation, "Informasi")
                                hasil = False
                                Exit While
                            End If
                        End If
                    End If
                    drdcek.Close()

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang_Adj(cn, sqltrans, qtykecil_akh, qtykecil, kdbar, True)
                    If Not hasilplusmin.Equals("ok") Then
                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = False
                        Exit While
                    End If

                End If

                '3. insert to hist stok
                ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, nobukti, "-", "-", tanggal, "G000", kdbar, qtykecil, 0, "Opname (Batal)", "")

            End While
        End If

        Return hasil

    End Function

    Private Sub cek_batal(ByVal nobukti As String)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim sql As String = String.Format("select sbatal from tr_opname where nobukti='{0}'", nobukti)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    dv1(bs1.Position)("sbatal") = drd("sbatal").ToString
                End If
            End If

            drd.Close()

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

        cek_batal(dv1(bs1.Position)("nobukti").ToString)

        If Integer.Parse(dv1(bs1.Position)("sbatal").ToString) = 1 Then
            MsgBox("Data telah dibatalkan..", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If MsgBox("Yakin akan dibatalkan ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub

    Private Sub tsadd_Click(sender As Object, e As System.EventArgs) Handles tsadd.Click
        isload_detail = True
        Using fkar2 As New fopname2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fkar2.ShowDialog()
        End Using
        isload_detail = False
    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        cek_batal(dv1(bs1.Position)("nobukti").ToString)

        If Integer.Parse(dv1(bs1.Position)("sbatal").ToString) = 1 Then
            MsgBox("Data telah dibatalkan..", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If DateValue(Date.Now) <> DateValue(dv1(bs1.Position)("tanggal").ToString) Then
            MsgBox("Tanggal sudah tidak sesuai dengan tanggal opname...", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        isload_detail = True
        Using fkar2 As New fopname2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.ShowDialog()
        End Using
        isload_detail = False

    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        isload_detail = True

        Using fkar2 As New fopname2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.btadd.Enabled = False
            fkar2.btdel.Enabled = False
            fkar2.btsimpan.Enabled = False
            fkar2.ShowDialog()
        End Using

        isload_detail = False

    End Sub

    Private Sub GridView1_Click(sender As Object, e As System.EventArgs) Handles GridView1.Click
        opendata2()
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        opendata2()
    End Sub

    Private Sub GridView1_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles GridView1.SelectionChanged
        opendata2()
    End Sub

    Private Sub tsprint_Click(sender As System.Object, e As System.EventArgs) Handles tsprint.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        cek_batal(dv1(bs1.Position)("nobukti").ToString)

        Dim nobukti As String = dv1(bs1.Position)("nobukti").ToString

        Dim sql1 As String = String.Format("SELECT tr_opname.nobukti, tr_opname.tanggal, tr_opname.ket as note, ms_barang.kd_barang, ms_barang.nama_barang, tr_opname2.qty_k1, tr_opname2.qty_k2, " & _
        "tr_opname2.qty_r1, tr_opname2.qty_r2, tr_opname2.qty_sel1, tr_opname2.qty_sel2, ms_barang.satuan1, ms_barang.satuan2 " & _
        "FROM tr_opname INNER JOIN tr_opname2 ON tr_opname.nobukti = tr_opname2.nobukti INNER JOIN " & _
        "ms_barang ON tr_opname2.kd_barang = ms_barang.kd_barang " & _
        "WHERE tr_opname.sbatal=0 and tr_opname.nobukti='{0}'", nobukti)

        Using fkar2 As New fpr_buktiopname With {.WindowState = FormWindowState.Maximized, .sql1 = sql1}
            fkar2.ShowDialog(Me)
        End Using

    End Sub

End Class