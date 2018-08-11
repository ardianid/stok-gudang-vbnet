Imports System.Data
Imports System.Data.OleDb

Public Class fbarang2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private kd_old As String
    Private qty1old, qty2old, qty3old As Integer

    Private Sub kosongkan()
        tkode.Text = ""
        tnama.Text = ""
        tnama2.Text = ""

        tqty1.EditValue = 0
        tqty2.EditValue = 0
        tqty3.EditValue = 0

        tsat1.EditValue = ""
        tsat2.EditValue = ""
        tsat3.EditValue = ""

        tberat1.EditValue = 0.0
        tberat2.EditValue = 0.0
        tberat3.EditValue = 0.0

    End Sub

    Private Sub isi()
        tkode.EditValue = dv(position)("kd_barang").ToString
        tnama.EditValue = dv(position)("nama_barang").ToString
        tnama2.EditValue = dv(position)("nama_lap").ToString


        tjenis.EditValue = dv(position)("jenis").ToString
        tkelompok.EditValue = dv(position)("kelompok").ToString

        tqty1.EditValue = dv(position)("qty1").ToString
        tqty2.EditValue = dv(position)("qty2").ToString
        tqty3.EditValue = dv(position)("qty3").ToString

        qty1old = tqty1.EditValue
        qty2old = tqty2.EditValue
        qty3old = tqty3.EditValue

        tsat1.EditValue = dv(position)("satuan1").ToString
        tsat2.EditValue = dv(position)("satuan2").ToString
        tsat3.EditValue = dv(position)("satuan3").ToString

        If Decimal.Parse(dv(position)("berat1").ToString) = 0 Then
            tberat1.EditValue = 0.0
        Else
            tberat1.EditValue = dv(position)("berat1").ToString
        End If

        If Decimal.Parse(dv(position)("berat2").ToString) = 0 Then
            tberat2.EditValue = 0.0
        Else
            tberat2.EditValue = dv(position)("berat2").ToString
        End If

        If Decimal.Parse(dv(position)("berat3").ToString) = 0 Then
            tberat3.EditValue = 0.0
        Else
            tberat3.EditValue = dv(position)("berat3").ToString
        End If

    End Sub

    Private Sub simpan()

        If addstat = False Then
            If cek_inout() = True Then

                If qty1old <> tqty1.EditValue Then
                    MsgBox("Qty1 tidak dapat dirubah karna sudah ada transaksi sebelumnya...", vbOKOnly + vbInformation, "Informasi")
                    tqty1.Focus()
                    Return
                End If

                If qty2old <> tqty2.EditValue Then
                    MsgBox("Qty2 tidak dapat dirubah karna sudah ada transaksi sebelumnya...", vbOKOnly + vbInformation, "Informasi")
                    tqty2.Focus()
                    Return
                End If

                If qty3old <> tqty3.EditValue Then
                    MsgBox("Qty3 tidak dapat dirubah karna sudah ada transaksi sebelumnya...", vbOKOnly + vbInformation, "Informasi")
                    tqty3.Focus()
                    Return
                End If

            End If
        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim sqlc As String = String.Format("select kd_barang from ms_barang where kd_barang='{0}'", tkode.Text.Trim, tnama.EditValue)
            Dim sql_insert As String = String.Format("insert into ms_barang (kd_barang,nama_barang,qty1,qty2,qty3,satuan1,satuan2,satuan3,jenis,kelompok,berat1,berat2,berat3) values('{0}','{1}',{2},{3},{4},'{5}','{6}','{7}','{8}','{9}',{10},{11},{12})", tkode.Text.Trim, tnama.EditValue, _
                                                     Replace(tqty1.EditValue, ",", "."), Replace(tqty2.EditValue, ",", "."), Replace(tqty3.EditValue, ",", "."), tsat1.EditValue, tsat2.EditValue, tsat3.EditValue, tjenis.EditValue, tkelompok.EditValue, Replace(tberat1.EditValue, ",", "."), Replace(tberat2.EditValue, ",", "."), Replace(tberat3.EditValue, ",", "."))
            Dim sql_update As String = String.Format("update ms_barang set nama_barang='{0}',qty1={1},qty2={2},qty3={3},satuan1='{4}',satuan2='{5}',satuan3='{6}',jenis='{7}',kelompok='{8}',berat1={9},berat2={10},berat3={11} where kd_barang='{12}'", tnama.EditValue, _
                                                     Replace(tqty1.EditValue, ",", "."), Replace(tqty2.EditValue, ",", "."), Replace(tqty3.EditValue, ",", "."), tsat1.EditValue, tsat2.EditValue, tsat3.EditValue, tjenis.EditValue, tkelompok.EditValue, Replace(tberat1.EditValue, ",", "."), Replace(tberat2.EditValue, ",", "."), Replace(tberat3.EditValue, ",", "."), tkode.Text.Trim)

            Dim sqliins2 As String = String.Format("insert into ms_barang2 (kd_gudang,kd_barang) values('G000','{0}')", tkode.Text.Trim)


            sqltrans = cn.BeginTransaction

            Dim comd As OleDbCommand

            If addstat = True Then

                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim dread As OleDbDataReader = cmdc.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        Dim kdka As String = dread(0).ToString

                        If kdka.Trim.Length = 0 Then
                            comd = New OleDbCommand(sql_insert, cn, sqltrans)
                            comd.ExecuteNonQuery()

                            Using cmdin2 As OleDbCommand = New OleDbCommand(sqliins2, cn, sqltrans)
                                cmdin2.ExecuteNonQuery()
                            End Using

                            ' Clsmy.InsertToLog(cn, "btbarang", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                            insertview()
                        Else

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox("Kode/Nama sudah ada ...", vbOKOnly + vbExclamation, "Informasi")
                            tkode.Focus()
                            Return
                        End If
                    Else
                        comd = New OleDbCommand(sql_insert, cn, sqltrans)
                        comd.ExecuteNonQuery()

                        Using cmdin2 As OleDbCommand = New OleDbCommand(sqliins2, cn, sqltrans)
                            cmdin2.ExecuteNonQuery()
                        End Using

                        'Clsmy.InsertToLog(cn, "btbarang", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                        insertview()
                    End If
                Else
                    comd = New OleDbCommand(sql_insert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Using cmdin2 As OleDbCommand = New OleDbCommand(sqliins2, cn, sqltrans)
                        cmdin2.ExecuteNonQuery()
                    End Using

                    '  Clsmy.InsertToLog(cn, "btbarang", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                    insertview()
                End If

                dread.Close()


            Else

                ' jika rubah

                comd = New OleDbCommand(sql_update, cn, sqltrans)
                comd.ExecuteNonQuery()

                ' Clsmy.InsertToLog(cn, "btbarang", 0, 1, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                updateview()

            End If

            '-------------------------

            sqltrans.Commit()
            MsgBox("Data telah disimpan...", vbOKOnly + vbInformation, "Informasi")

            If addstat = True Then
                kosongkan()
                tkode.Focus()
            Else
                Me.Close()
            End If


        Catch ex As Exception

            If Not IsNothing(sqltrans) Then
                sqltrans.Rollback()
            End If

            MsgBox(ex.ToString)
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try


    End Sub

    Private Sub updateview()

        dv(position)("kd_barang") = tkode.Text.Trim
        dv(position)("nama_barang") = tnama.EditValue
        dv(position)("nama_lap") = tnama2.EditValue

        dv(position)("qty1") = tqty1.EditValue
        dv(position)("qty2") = tqty2.EditValue
        dv(position)("qty3") = tqty3.EditValue

        dv(position)("satuan1") = tsat1.Text.Trim
        dv(position)("satuan2") = tsat2.Text.Trim
        dv(position)("satuan3") = tsat3.Text.Trim

        dv(position)("berat1") = tberat1.EditValue
        dv(position)("berat2") = tberat2.EditValue
        dv(position)("berat3") = tberat3.EditValue

        dv(position)("jenis") = tjenis.EditValue
        dv(position)("kelompok") = tkelompok.EditValue


    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("kd_barang") = tkode.Text.Trim
        orow("nama_barang") = tnama.EditValue
        orow("nama_lap") = tnama2.EditValue

        orow("qty1") = tqty1.EditValue
        orow("qty2") = tqty2.EditValue
        orow("qty3") = tqty3.EditValue

        orow("satuan1") = tsat1.Text.Trim
        orow("satuan2") = tsat2.Text.Trim
        orow("satuan3") = tsat3.Text.Trim

        orow("berat1") = tberat1.EditValue
        orow("berat2") = tberat2.EditValue
        orow("berat3") = tberat3.EditValue

        orow("jmlstok_f1") = 0
        orow("jmlstok_f2") = 0
        orow("jmlstok_f3") = 0

        orow("jenis") = tjenis.EditValue
        orow("kelompok") = tkelompok.EditValue

        dv.EndInit()

    End Sub

    Private Sub hitung_berat23(ByVal set2 As Boolean)

        Dim berat1 As Decimal = tberat1.EditValue

        If berat1 = 0 Then
            tberat2.EditValue = 0.0
            tberat3.EditValue = 0.0
            Return
        End If

        Dim qty2 As Double = tqty2.EditValue
        Dim qty3 As Double = tqty3.EditValue

        Dim berat2 As Decimal = 0.0

        If set2 Then
            berat2 = tberat2.EditValue
        Else

            If tqty2.EditValue = 0 Then
                berat2 = 0
            Else
                berat2 = berat1 / qty2
                tberat2.EditValue = berat2
            End If

            
        End If

        Dim berat3 As Decimal = 0
        If tqty3.EditValue = 0 Then
            berat3 = 0
        Else
            berat3 = berat2 / qty3
        End If

        tberat3.EditValue = berat3

    End Sub

    Private Sub isi_kelompok()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim sql As String = "select nama_kelompok from ms_kelompok"
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            tkelompok.Properties.Items.Clear()

            If drd.HasRows Then
                While drd.Read
                    tkelompok.Properties.Items.Add(drd(0).ToString)
                End While
            End If
            drd.Close()

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

    Private Function cek_inout() As Boolean

        Dim hasil As Boolean = False

        Dim sql As String = String.Format("select count(nobukti) as jml from hbarang_kendaraan where kd_barang='{0}'", tkode.Text.Trim)

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    If Integer.Parse(drd(0).ToString) > 0 Then
                        hasil = True
                    End If
                End If
            End If
            drd.Close()

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try


        Return hasil
    End Function

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click


        If tkode.Text.Trim.Length = 0 Then
            MsgBox("Kode tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tkode.Focus()
            Return
        End If

        If tnama.Text.Trim.Length = 0 Then
            MsgBox("Nama barang tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tnama.Focus()
            Return
        End If

        If addstat Then
            tqty3.EditValue = 1
            tsat3.EditValue = "-"
        End If

        If tqty1.EditValue = 0 Or tqty2.EditValue = 0 Or tqty3.EditValue = 0 Then
            MsgBox("Qty min 1 !!!", vbOKOnly + vbInformation, "Informasi")
            tqty1.Focus()
        End If

        simpan()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fkab2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            tkode.Focus()
        Else
            tnama.Focus()
        End If
    End Sub

    Private Sub fkab2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        isi_kelompok()

        If addstat = True Then

            tkode.Enabled = True
            kosongkan()

        Else
            tkode.Enabled = False
            isi()

        End If

    End Sub

    Private Sub tberat1_Validated(sender As System.Object, e As System.EventArgs) Handles tberat1.Validated
        hitung_berat23(False)
    End Sub

    Private Sub tberat2_Validated(sender As System.Object, e As System.EventArgs) Handles tberat2.Validated
        hitung_berat23(True)
    End Sub

    Private Sub tqty2_Validated(sender As System.Object, e As System.EventArgs) Handles tqty2.Validated, tqty1.Validated, tqty3.Validated
        hitung_berat23(False)
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            sqltrans = cn.BeginTransaction

            Dim sql As String = "SELECT tradm_gud.nobukti, tradm_gud.tanggal, tradm_gud.nopol, tradm_gud2.kd_barang, sum(tradm_gud2.qtyinkecil) as qtyinkecil, sum(tradm_gud2.qtyoutkecil) as qtyoutkecil " & _
            "FROM tradm_gud INNER JOIN tradm_gud2 ON tradm_gud.nobukti = tradm_gud2.nobukti group by  tradm_gud.nobukti, tradm_gud.tanggal, tradm_gud.nopol, tradm_gud2.kd_barang"
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            While drd.Read

                Dim nobukti As String = drd("nobukti").ToString
                Dim tanggal As String = drd("tanggal").ToString
                Dim nopol As String = drd("nopol").ToString
                Dim kd_barang As String = drd("kd_barang").ToString
                Dim qtyin As Integer = drd("qtyinkecil").ToString
                Dim qtyout As Integer = drd("qtyoutkecil").ToString

                Dim sqlin As String = String.Format("insert into hbarang_kendaraan (nobukti,nobukti2,tanggal,nopol,kd_barang,kd_gudang,jmlin,jmlout,jenis) values('{0}','-','{1}','{2}','{3}','G000',{4},{5},'Insert')", _
                                                    nobukti, convert_date_to_ind(tanggal), nopol, kd_barang, qtyin, qtyout)
                Using cmdin As OleDbCommand = New OleDbCommand(sqlin, cn, sqltrans)
                    cmdin.ExecuteNonQuery()
                End Using

            End While
            drd.Close()

            sqltrans.Commit()
            MsgBox("selesai")

        Catch ex As Exception
            If Not IsNothing(sqltrans) Then
                sqltrans.Rollback()
            End If

            MsgBox(ex.ToString)
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try


    End Sub

End Class