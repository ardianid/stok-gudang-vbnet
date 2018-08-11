Imports System.Data
Imports System.Data.OleDb

Public Class finput2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private tgl_old As String
    Private dttrans As DataTable

    Dim nopol_old As String

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tbukti_tr.Text = ""
        tket.Text = ""

        opengrid()

    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT tradm_gud2.noid,tradm_gud2.kd_barang, ms_barang.nama_barang, tradm_gud2.qtyin,tradm_gud2.qtyin_bad, tradm_gud2.qtyout, tradm_gud2.satuan, tradm_gud2.qtyinkecil,tradm_gud2.qtyinkecil_bad, tradm_gud2.qtyoutkecil,tradm_gud2.berat1,tradm_gud2.total_berat " & _
            "FROM  tradm_gud2 INNER JOIN ms_barang ON tradm_gud2.kd_barang = ms_barang.kd_barang where tradm_gud2.nobukti='{0}'", tbukti.Text.Trim)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid1.DataSource = Nothing

        Try

            dv1 = Nothing

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            ds = New DataSet()
            ds = ClassMy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            grid1.DataSource = dv1

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

    Private Sub isi()

        Dim nobukti As String = dv(position)("nobukti").ToString
        Dim sql As String = String.Format("select nobukti,tanggal,nobukti_trans,nopol,snote,sbatal from tradm_gud where nobukti='{0}'", nobukti)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim dread As OleDbDataReader = comd.ExecuteReader
            Dim hasil As Boolean

            If dread.HasRows Then
                If dread.Read Then

                    If Not dread("nobukti").ToString.Equals("") Then

                        hasil = True

                        tbukti.EditValue = dread("nobukti").ToString
                        tnopol.EditValue = dread("nopol").ToString

                        nopol_old = tnopol.EditValue

                        ttgl.EditValue = DateValue(dread("tanggal").ToString)

                        tgl_old = ttgl.EditValue

                        
                        tbukti_tr.EditValue = dread("nobukti_trans").ToString
                        
                        tket.EditValue = dread("snote").ToString

                        opengrid()

                    Else
                        hasil = False
                    End If


                Else
                    hasil = False
                End If
            Else
                hasil = False
            End If

            If hasil = False Then

                kosongkan()

            End If

            dread.Close()

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

    'Private Sub isi_nopol()

    '    Const sql As String = "select * from wtl.dbo.ms_kendaraan where aktif=1"

    '    Dim cn As OleDbConnection = Nothing
    '    Dim ds As DataSet
    '    Dim dvg As DataView

    '    Try

    '        cn = ClassMy.open_conn
    '        ds = New DataSet
    '        ds = ClassMy.GetDataSet(sql, cn)

    '        Dim dvm As DataViewManager = New DataViewManager(ds)
    '        dvg = dvm.CreateDataView(ds.Tables(0))

    '        tnopol.Properties.DataSource = dvg

    '    Catch ex As Exception
    '        MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
    '    Finally

    '        If Not cn Is Nothing Then
    '            If cn.State = ConnectionState.Open Then
    '                cn.Close()
    '            End If
    '        End If

    '    End Try

    'End Sub

    Private Function cekbukti(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String


        Dim tahun As String = Year(ttgl.EditValue)
        tahun = Microsoft.VisualBasic.Right(tahun, 2)
        Dim bulan As String = Month(ttgl.EditValue)

        If bulan.Length = 1 Then
            bulan = "0" & bulan
        End If

        Dim bulantahun As String = String.Format("{0}{1}", tahun, bulan)

        Dim sql As String = String.Format("select max(nobukti) from tradm_gud where len(nobukti)=13 and nobukti like 'ING.{0}%'", bulantahun)

        '   sql = String.Format(" {0} and tanggal<'2014/11/07'", sql)

        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim nilai As Integer = 0

        If drd.HasRows Then
            If drd.Read Then

                If Not drd(0).ToString.Equals("") Then
                    nilai = Microsoft.VisualBasic.Right(drd(0).ToString, 5)
                End If

            End If
        End If

        nilai = nilai + 1
        Dim kbukti As String = nilai

        Select Case kbukti.Length
            Case 1
                kbukti = "0000" & nilai
            Case 2
                kbukti = "000" & nilai
            Case 3
                kbukti = "00" & nilai
            Case 4
                kbukti = "0" & nilai
            Case Else
                kbukti = nilai
        End Select

        Return String.Format("ING.{0}{1}{2}", tahun, bulan, kbukti)

    End Function

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim cmd As OleDbCommand

            If addstat Then
                Dim bukti As String = cekbukti(cn, sqltrans)
                tbukti.EditValue = bukti

                '0. insert transaksi
                Dim sqlin As String = String.Format("insert into tradm_gud (nobukti,tanggal,nobukti_trans,nopol,snote) values('{0}','{1}','{2}','{3}','{4}')", _
                                                    tbukti.Text.Trim, convert_date_to_ind(ttgl.EditValue), tbukti_tr.Text.Trim, tnopol.Text.Trim, tket.Text.Trim)

                cmd = New OleDbCommand(sqlin, cn, sqltrans)
                cmd.ExecuteNonQuery()



            Else

             
                '1. update rekap
                Dim sqlup As String = String.Format("update tradm_gud set tanggal='{0}',nobukti_trans='{1}',nopol='{2}',snote='{3}' where nobukti='{4}'", _
                                                    convert_date_to_ind(ttgl.EditValue), tbukti_tr.Text.Trim, tnopol.Text.Trim, tket.Text.Trim, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup, cn, sqltrans)
                cmd.ExecuteNonQuery()

            End If

            If simpan2(cn, sqltrans) = "ok" Then
                '------------------------------

                If addstat = True Then
                    insertview()
                Else
                    updateview()
                End If

                sqltrans.Commit()

                MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")

                If addstat = True Then
                    kosongkan()
                    ttgl.Focus()
                Else
                    Me.Close()
                End If

                '----------------------------------
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

    Private Function simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim hasil As String = ""

        Dim kdbar, satuan As String
        Dim qtyin, qtyout As Integer
        Dim qtyink, qtyoutk As Integer

        Dim qtyin_bad As Integer
        Dim qtyink_bad As Integer

        Dim berat1 As Decimal
        Dim totberat As Decimal

        For i As Integer = 0 To dv1.Count - 1

            kdbar = dv1(i)("kd_barang").ToString
            satuan = dv1(i)("satuan").ToString
            qtyin = Integer.Parse(dv1(i)("qtyin").ToString)
            qtyout = Integer.Parse(dv1(i)("qtyout").ToString)
            qtyink = Integer.Parse(dv1(i)("qtyinkecil").ToString)
            qtyoutk = Integer.Parse(dv1(i)("qtyoutkecil").ToString)

            qtyin_bad = Integer.Parse(dv1(i)("qtyin_bad").ToString)
            qtyink_bad = Integer.Parse(dv1(i)("qtyinkecil_bad").ToString)

            berat1 = Decimal.Parse(dv1(i)("berat1").ToString)
            totberat = Decimal.Parse(dv1(i)("total_berat").ToString)

            If dv1(i)("noid").Equals(0) Then
                Dim sqlins As String = String.Format("insert into tradm_gud2 (nobukti,kd_barang,satuan,qtyin,qtyout,qtyinkecil,qtyoutkecil,qtyin_bad,qtyinkecil_bad,berat1,total_berat) values('{0}','{1}','{2}',{3},{4},{5},{6},{7},{8},{9},{10})", tbukti.Text.Trim, _
                                                     kdbar, satuan, qtyin, qtyout, qtyink, qtyoutk, qtyin_bad, qtyink_bad, Replace(berat1, ",", "."), Replace(totberat, ",", "."))

                Using cmd As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using


                If qtyin > 0 Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyink, kdbar, "G000", True, False, False)
                    If Not hasilplusmin.Equals("ok") Then
                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For
                    End If

                    '3. insert to hist stok
                    ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, tnopol.Text.Trim, ttgl.EditValue, "G000", kdbar, qtyink, 0, "Insert", "")

                ElseIf qtyout > 0 Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyoutk, kdbar, "G000", False, False, False)
                    If Not hasilplusmin.Equals("ok") Then
                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For
                    End If

                    '3. insert to hist stok
                    ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, tnopol.Text.Trim, ttgl.EditValue, "G000", kdbar, 0, qtyoutk, "Insert", "")

                End If

            Else

                If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Or nopol_old <> tnopol.EditValue Then

                    If qtyin > 0 Then

                        '3. insert to hist stok
                        ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, nopol_old, tgl_old, "G000", kdbar, 0, qtyink, "Edit", "")

                        ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, nopol_old, ttgl.EditValue, "G000", kdbar, qtyink, 0, "Edit", "")

                    ElseIf qtyout > 0 Then

                        '3. insert to hist stok
                        ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, nopol_old, tgl_old, "G000", kdbar, qtyoutk, 0, "Edit", "")

                        ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, nopol_old, ttgl.EditValue, "G000", kdbar, 0, qtyoutk, "Edit", "")

                    End If

                End If




            End If

        Next

        If Not hasil.Equals("error") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("sbatal") = 0
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.EditValue
        orow("nobukti_trans") = tbukti_tr.Text.Trim
        orow("nopol") = tnopol.EditValue
        orow("snote") = tket.Text.Trim
        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.EditValue
        dv(position)("nobukti_trans") = tbukti_tr.Text.Trim
        dv(position)("nopol") = tnopol.EditValue
        dv(position)("snote") = tket.Text.Trim

    End Sub

    Private Sub hapus()

        Dim cn As OleDbConnection = Nothing

        Dim kdbar, satuan As String
        Dim qtyin, qtyout As Integer
        Dim qtyink, qtyoutk As Integer

        Dim qtyin_bad As Integer
        Dim qtyink_bad As Integer

        Dim noid As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("noid"))

        Try

            If noid = 0 Then
                dv1.Delete(Me.BindingContext(dv1).Position)
            Else

                cn = New OleDbConnection
                cn = ClassMy.open_conn

                Dim sqltrans As OleDbTransaction = cn.BeginTransaction

                Dim sqldel As String = String.Format("delete from tradm_gud2 where noid={0}", noid)
                Using cmd As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                kdbar = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString
                satuan = dv1(Me.BindingContext(dv1).Position)("satuan").ToString
                qtyin = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtyin").ToString)
                qtyout = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtyout").ToString)
                qtyink = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtyinkecil").ToString)
                qtyoutk = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtyoutkecil").ToString)

                qtyin_bad = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtyin_bad").ToString)
                qtyink_bad = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtyinkecil_bad").ToString)

                '                If qtyin_bad > 0 Then

                '2:                  .update(barang)
                '                    Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyink_bad, kdbar, "G000", False, False, False)
                '                    If Not hasilplusmin.Equals("ok") Then

                '                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                '                        GoTo langsung
                '                    End If

                '                    3. insert to hist stok
                '                    ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, tnopol.Text.Trim, ttgl_muat.EditValue, tgudang2.EditValue, kdbar, 0, qtyink_bad, tjenis.Text.Trim)

                '                End If

                If qtyin > 0 Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyink, kdbar, "G000", False, False, False)
                    If Not hasilplusmin.Equals("ok") Then
                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        GoTo langsung
                    End If

                    '3. insert to hist stok
                    ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, tnopol.Text.Trim, ttgl.EditValue, "G000", kdbar, 0, qtyink, "Delete", "")

                ElseIf qtyout > 0 Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyoutk, kdbar, "G000", True, False, False)
                    If Not hasilplusmin.Equals("ok") Then

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        GoTo langsung
                    End If

                    '3. insert to hist stok
                    ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, tnopol.Text.Trim, ttgl.EditValue, "G000", kdbar, qtyoutk, 0, "Delete", "")

                End If

                sqltrans.Commit()

                dv1.Delete(Me.BindingContext(dv1).Position)



            End If

langsung:

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

    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        hapus()

    End Sub

    Private Function cek_notrans() As Boolean

        Dim cn As OleDbConnection = Nothing
        Dim hasil As Boolean = False

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim sql As String = String.Format("select COUNT(*) from tradm_gud where sbatal=0 and nobukti_trans='{0}'", tbukti_tr.Text.Trim)

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    If Integer.Parse(drd(0).ToString) > 2 Then
                        hasil = True
                    End If
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

        Return hasil

    End Function


    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click

        If tbukti_tr.Text.Trim.Length <= 0 Then
            MsgBox("Isi dulu nobukti transaksi...", vbOKOnly + vbExclamation, "Konfirmasi")
            tbukti_tr.Focus()
            Return
        End If

        Using fkar2 As New finput3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fkar2.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If IsNothing(dv1) Then
            MsgBox("Tidak ada barang yang akan diproses...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1.Count <= 0 Then
            MsgBox("Tidak ada barang yang akan diproses...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If


        If tnopol.Text.Trim.Trim.Length <= 0 Then
            MsgBox("No polisi harus diisi..", vbOKOnly + vbExclamation, "Konfirmasi")
            tnopol.Focus()
            Return
        End If

        If tbukti_tr.Text.Trim.Length = 0 Then
            MsgBox("No Bukti transaksi harus diisi..", vbOKOnly + vbExclamation, "Konfirmasi")
            tbukti_tr.Focus()
            Return
        End If

        If addstat Then
            If Not tbukti_tr.Text.Trim.Equals("-") Then
                If cek_notrans() = True Then
                    MsgBox(String.Format("No Bukti Transaksi {0} sudah diinput", tbukti_tr.Text.Trim), vbOKOnly + vbInformation, "Informasi")
                    tbukti_tr.Focus()
                    Return
                End If
            End If
        End If

        If MsgBox("Yakin semua data sudah benar?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            simpan()
        End If

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fadm_gud2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat Then
            ttgl.Focus()
        Else
            ttgl.Focus()
        End If
    End Sub

    Private Sub fadm_gud2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttgl.EditValue = Date.Now

        If addstat Then
            kosongkan()

            tnopol.Enabled = True
            tbukti_tr.Enabled = True


        Else
            isi()

            tnopol.Enabled = False

            tbukti_tr.Enabled = False

        End If

    End Sub



End Class