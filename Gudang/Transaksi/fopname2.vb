Imports System.Data
Imports System.Data.OleDb
Public Class fopname2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private tgl_old As String

    Private Sub kosongkan()

        tbukti.Text = "<< New >>"
        tket.Text = ""

        opengrid()

    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT ms_barang.kd_barang, ms_barang.nama_barang, tr_opname2.qty_k1, tr_opname2.qty_k2, tr_opname2.qty_r1, tr_opname2.qty_r2, tr_opname2.qty_sel1,tr_opname2.qty_sel2,tr_opname2.noid,tr_opname2.qty_k,tr_opname2.qty_r,tr_opname2.qty_sel,tr_opname2.noid " & _
        "FROM tr_opname2 INNER JOIN ms_barang ON tr_opname2.kd_barang = ms_barang.kd_barang " & _
        "WHERE tr_opname2.nobukti='{0}' ", tbukti.Text.Trim)

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

        tbukti.Text = dv(position)("nobukti").ToString
        ttgl.EditValue = DateValue(dv(position)("tanggal").ToString)

        tket.Text = dv(position)("ket").ToString

        tgl_old = ttgl.EditValue

        opengrid()

    End Sub

    Private Function cekbukti(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim sql As String = ""

        Dim nobuktiawal As String = "OPN."

        sql = String.Format("select max(nobukti) from tr_opname where  nobukti like '{0}%'", nobuktiawal)


        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim nilai As Integer = 0

        If drd.HasRows Then
            If drd.Read Then

                If Not drd(0).ToString.Equals("") Then
                    nilai = Microsoft.VisualBasic.Right(drd(0).ToString, 4)
                End If

            End If
        End If

        nilai = nilai + 1
        Dim kbukti As String = nilai

        Select Case kbukti.Length
            Case 1
                kbukti = "000" & nilai
            Case 2
                kbukti = "00" & nilai
            Case 3
                kbukti = "0" & nilai
            Case Else
                kbukti = nilai
        End Select

        Dim tahun As String = Year(ttgl.EditValue)
        tahun = Microsoft.VisualBasic.Right(tahun, 2)
        Dim bulan As String = Month(ttgl.EditValue)

        If bulan.Length = 1 Then
            bulan = "0" & bulan
        End If

        Return String.Format("{0}{1}{2}{3}", nobuktiawal, tahun, bulan, kbukti)

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

                '1 . update faktur
                Dim sqlin_faktur As String = String.Format("insert into tr_opname (nobukti,tanggal,ket) values('{0}','{1}','{2}')", _
                                                           tbukti.Text.Trim, convert_date_to_ind(ttgl.EditValue), tket.Text.Trim)


                cmd = New OleDbCommand(sqlin_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                'Clsmy.InsertToLog(cn, "btopname", 1, 0, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)

            Else

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update tr_opname set tanggal='{0}',ket='{1}' where nobukti='{2}'", _
                                                          convert_date_to_ind(ttgl.EditValue), tket.Text.Trim, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                ' Clsmy.InsertToLog(cn, "btopname", 0, 1, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)

            End If


            If simpan2(cn, sqltrans) = True Then

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

            End If

lanjut_aja:

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

    Private Function simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As Boolean

        Dim hasil As Boolean = True

        For i As Integer = 0 To dv1.Count - 1

            Dim qty_sel As Integer = Integer.Parse(dv1(i)("qty_sel").ToString)

            If dv1(i)("noid") <> 0 Then

                ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, "-", "-", ttgl.EditValue, "G000", dv1(i)("kd_barang").ToString, qty_sel, 0, "Opname", "")

                ''3. insert to hist stok
                ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, "-", "-", ttgl.EditValue, "G000", dv1(i)("kd_barang").ToString, 0, qty_sel, "Opname", "")

            End If

            If dv1(i)("noid") = 0 Then

                Dim qty_k1 As Integer = Integer.Parse(dv1(i)("qty_k1").ToString)
                Dim qty_k2 As Integer = Integer.Parse(dv1(i)("qty_k2").ToString)
                Dim qty_k As Integer = Integer.Parse(dv1(i)("qty_k").ToString)

                Dim qty_r1 As Integer = Integer.Parse(dv1(i)("qty_r1").ToString)
                Dim qty_r2 As Integer = Integer.Parse(dv1(i)("qty_r2").ToString)
                Dim qty_r As Integer = Integer.Parse(dv1(i)("qty_r").ToString)

                Dim qty_sel1 As Integer = Integer.Parse(dv1(i)("qty_sel1").ToString)
                Dim qty_sel2 As Integer = Integer.Parse(dv1(i)("qty_sel2").ToString)
                '   Dim qty_sel As Integer = Integer.Parse(dv1(i)("qty_sel").ToString)

                Dim sqlins As String = String.Format("insert into tr_opname2 (nobukti,kd_barang,qty_k1,qty_k2,qty_k,qty_r1,qty_r2,qty_r,qty_sel1,qty_sel2,qty_sel) values('{0}','{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10})", _
                                                     tbukti.Text.Trim, dv1(i)("kd_barang").ToString, qty_k1, qty_k2, qty_k, qty_r1, qty_r2, qty_r, qty_sel1, qty_sel2, qty_sel)

                Using cmdins As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmdins.ExecuteNonQuery()
                End Using

                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang_Adj(cn, sqltrans, qty_r, qty_sel, dv1(i)("kd_barang").ToString, False)
                If Not hasilplusmin.Equals("ok") Then
                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    hasil = False
                    Exit For

                End If

                ''3. insert to hist stok
                ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, "-", "-", ttgl.EditValue, "G000", dv1(i)("kd_barang").ToString, 0, qty_sel, "Opname", "")

            End If

        Next

        Return hasil

    End Function

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.Text.Trim
        orow("ket") = tket.Text.Trim

        orow("sbatal") = 0

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.Text.Trim
        dv(position)("ket") = tket.Text.Trim

    End Sub

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click
        Using fkar2 As New fopname3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fkar2.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If addstat = True Then

            Dim kdbar As String = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString


            dv1.Delete(Me.BindingContext(dv1).Position)

        Else

            Dim cn As OleDbConnection = Nothing

            Try

                cn = New OleDbConnection
                cn = ClassMy.open_conn

                Dim sqltrans As OleDbTransaction = cn.BeginTransaction

                Dim kdbar As String = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString

                Dim qtykecil As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty_sel").ToString)
                Dim qtykecil_akh As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty_k").ToString)

                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang_Adj(cn, sqltrans, qtykecil_akh, qtykecil, kdbar, True)
                If Not hasilplusmin.Equals("ok") Then
                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    sqltrans.Rollback()
                    GoTo langsung
                End If


                If addstat = False Then

                    If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Then
                        'ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, "-", "-", tgl_old, "G000", kdbar, qtykecil, 0, "Opname (Edit)", "")
                        'ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, "-", "-", ttgl.EditValue, "G000", kdbar, 0, qtykecil, "Opname (Edit)", "")
                    End If

                End If

                '3. insert to hist stok
                ClassMy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, "-", "-", ttgl.EditValue, "G000", kdbar, qtykecil, 0, "Opname (Edit)", "")


                Dim sql As String = String.Format("delete from tr_opname2 where noid={0}", dv1(Me.BindingContext(dv1).Position)("noid").ToString)

                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()

                dv1.Delete(Me.BindingContext(dv1).Position)

                sqltrans.Commit()

                MsgBox("Data dihapus...", vbOKOnly + vbInformation, "Informasi")

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

        End If

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub ffaktur_to2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated

        ttgl.Focus()

    End Sub

    Private Sub ffaktur_to2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If addstat = True Then


            ttgl.EditValue = Date.Now

            kosongkan()

        Else

            isi()

        End If

    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If IsNothing(dv1) Then
            MsgBox("Tidak ada barang yang akan dibeli", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If dv1.Count <= 0 Then
            MsgBox("Tidak ada barang yang akan dibeli", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If MsgBox("Yakin sudah benar.. ???", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
            Return
        Else
            simpan()
        End If

    End Sub


End Class