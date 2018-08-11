Imports System.Globalization
Imports System.Data.OleDb

Module Module1

    Public userprog, pwd As String

    Public Function convert_date_to_eng(ByVal valdate As String) As String

        If valdate = "" Then
            Return ""
        End If

        valdate = CType(valdate, DateTime).ToString("MM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US"))

        Return valdate

    End Function


    Public Function convert_date_to_ind(ByVal valdate As String) As String

        If valdate = "" Then
            Return ""
        End If

        valdate = CType(valdate, Date).ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("id-ID"))

        Return valdate

    End Function


    Public Function PlusMin_Barang_Fsk(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal qtykecil As Integer, _
                               ByVal kdbar As String, ByVal kdgud As String, ByVal additem As Boolean, _
                               ByVal perhitungansaja As Boolean, ByVal hanyabarang2 As Boolean) As String

        Dim cmdcek_brang As OleDbCommand
        Dim drd_barang As OleDbDataReader

        Dim sqlcekbarang As String = String.Format("select a.kd_posisi,a.jmlstok_f as jmlstok,b.qty1,b.qty2,b.qty3,b.jmlstok_f as jmlstok2 from ms_barang2 a inner join ms_barang b on a.kd_barang=b.kd_barang where b.jenis='FISIK' and a.kd_barang='{0}' and a.kd_gudang='{1}'", kdbar, kdgud)
        cmdcek_brang = New OleDbCommand(sqlcekbarang, cn, sqltrans)
        drd_barang = cmdcek_brang.ExecuteReader

        Dim konferror As String = ""

        If drd_barang.HasRows Then
            If drd_barang.Read Then
                If IsNumeric(drd_barang("kd_posisi").ToString) Then


                    Dim jmlstok As Integer = Integer.Parse(drd_barang("jmlstok").ToString)
                    Dim jmlstok2 As Integer = Integer.Parse(drd_barang("jmlstok2").ToString)

                    Dim qty1 As Integer = Integer.Parse(drd_barang("qty1").ToString)
                    Dim qty2 As Integer = Integer.Parse(drd_barang("qty2").ToString)
                    Dim qty3 As Integer = Integer.Parse(drd_barang("qty3").ToString)

                    Dim jml1, jml2, jml3 As Integer
                    Dim jml11, jml12, jml13 As Integer

                    Dim totqty As Integer = qty1 * qty2 * qty3

                    If additem = True Then

                        jmlstok = jmlstok + qtykecil
                        jmlstok2 = jmlstok2 + qtykecil

                    Else

                        If jmlstok < qtykecil Then
                            konferror = kdbar & " melebihi stok yang ada..."
                        End If

                        jmlstok = jmlstok - qtykecil
                        jmlstok2 = jmlstok2 - qtykecil

                    End If

                    ' by gudang

                    If jmlstok >= totqty Then

                        Dim sisa As Integer = jmlstok Mod totqty

                        jml1 = (jmlstok - sisa) / totqty

                        If sisa > qty2 Then
                            jml2 = sisa
                            sisa = sisa Mod qty2

                            jml2 = (jml2 - sisa) / qty2
                            jml3 = sisa

                        Else
                            jml2 = sisa
                            jml3 = 0
                        End If
                    End If

                    ' end by gudang

                    ' by item

                    If jmlstok2 >= totqty Then

                        Dim sisa As Integer = jmlstok2 Mod totqty

                        jml11 = (jmlstok2 - sisa) / totqty

                        If sisa > qty2 Then
                            jml12 = sisa
                            sisa = sisa Mod qty2

                            jml12 = (jml12 - sisa) / qty2
                            jml13 = sisa

                        Else
                            jml12 = sisa
                            jml13 = 0
                        End If
                    End If

                    ' end by item                   


                    If konferror.Equals("") Then

                        If perhitungansaja = False Then



                            Dim sqlup1 As String = String.Format("update ms_barang2 set jmlstok_f1={0},jmlstok_f2={1},jmlstok_f3={2},jmlstok_f={3} where kd_gudang='{4}' and kd_barang='{5}'", jml1, jml2, jml3, jmlstok, kdgud, kdbar)
                            Dim sqlup As String = String.Format("update ms_barang set jmlstok_f1={0},jmlstok_f2={1},jmlstok_f3={2},jmlstok_f={3} where kd_barang='{4}'", jml11, jml12, jml13, jmlstok2, kdbar)

                            Using cmd1 As OleDbCommand = New OleDbCommand(sqlup1, cn, sqltrans)
                                cmd1.ExecuteNonQuery()
                            End Using

                            If hanyabarang2 = False Then
                                Using cmd2 As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                                    cmd2.ExecuteNonQuery()
                                End Using
                            End If



                        End If

                        konferror = "ok"

                    End If

                Else

                    konferror = kdbar & " tidak ditemukan..."

                End If
            Else
                konferror = kdbar & " tidak ditemukan..."
            End If
        Else
            konferror = kdbar & " tidak ditemukan..."
        End If

        drd_barang.Close()

        Return konferror

    End Function


    Public Function PlusMin_Barang_Adj(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal qtykecil As Integer, ByVal qtyselisih As Integer, _
                           ByVal kdbar As String, ByVal additem As Boolean) As String

        Dim cmdcek_brang As OleDbCommand
        Dim drd_barang As OleDbDataReader

        Dim sqlcekbarang As String = String.Format("select b.kd_barang,b.jmlstok_f as jmlstok,b.qty1,b.qty2,1 as qty3 from ms_barang b where b.kd_barang='{0}'", kdbar)
        cmdcek_brang = New OleDbCommand(sqlcekbarang, cn, sqltrans)
        drd_barang = cmdcek_brang.ExecuteReader

        Dim konferror As String = ""

        If drd_barang.HasRows Then
            If drd_barang.Read Then
                If Not drd_barang("kd_barang").ToString.Equals("") Then


                    Dim jmlstok As Integer = Integer.Parse(drd_barang("jmlstok").ToString)
                    Dim jmlstok1 As Integer = 0

                    Dim qty1 As Integer = Integer.Parse(drd_barang("qty1").ToString)
                    Dim qty2 As Integer = Integer.Parse(drd_barang("qty2").ToString)
                    Dim qty3 As Integer = Integer.Parse(drd_barang("qty3").ToString)

                    Dim jml1, jml2, jml3 As Integer

                    Dim totqty As Integer = qty1 * qty2 * qty3

                    If additem = True Then

                        '   jmlstok1 = jmlstok - qtykecil
                        jmlstok = jmlstok + qtyselisih

                    Else

                        'If jmlstok < qtykecil Then
                        '    konferror = kdbar & " melebihi stok yang ada..."
                        'End If

                        jmlstok = jmlstok - qtyselisih


                    End If

                    ' by item

                    '  If jmlstok >= totqty Then

                    Dim sisa As Integer = jmlstok Mod totqty

                    jml1 = (jmlstok - sisa) / totqty

                    If sisa > qty2 Then
                        jml2 = sisa
                        sisa = sisa Mod qty2

                        jml2 = (jml2 - sisa) / qty2
                        jml3 = sisa

                    Else
                        jml2 = sisa
                        jml3 = 0
                    End If
                    'End If

                    ' end by item                   


                    If konferror.Equals("") Then

                        Dim sqlup1 As String = String.Format("update ms_barang2 set jmlstok_f1={0},jmlstok_f2={1},jmlstok_f={2} where kd_barang='{3}'", jml1, jml2, jmlstok, kdbar)
                        Dim sqlup As String = String.Format("update ms_barang set jmlstok_f1={0},jmlstok_f2={1},jmlstok_f={2} where kd_barang='{3}'", jml1, jml2, jmlstok, kdbar)

                        Using cmd2 As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                            cmd2.ExecuteNonQuery()
                        End Using

                        Using cmd3 As OleDbCommand = New OleDbCommand(sqlup1, cn, sqltrans)
                            cmd3.ExecuteNonQuery()
                        End Using

                        konferror = "ok"

                    End If

                    konferror = "ok"



                Else

                    konferror = kdbar & " tidak ditemukan..."

                End If
            Else
                konferror = kdbar & " tidak ditemukan..."
            End If
        Else
            konferror = kdbar & " tidak ditemukan..."
        End If

        drd_barang.Close()

        Return konferror

    End Function

End Module
