Imports System.Text
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Data
Imports System.Data.OleDb
Imports System.Security.Cryptography

Public Class ClassMy



    Private ReadOnly FileConf As String = Application.StartupPath & "\autocon.dll"

    Public Shared Function open_conn() As OleDbConnection

        Dim mloc As String = ""
        Dim mdbase As String = ""
        Dim muser As String = ""
        Dim mpwd As String = ""

        Dim cn = New OleDbConnection

        Try

            'Dim fileReader As StreamReader = New StreamReader(Application.StartupPath & "\autocon.dll")

            'For i As Integer = 0 To 3

            '    Select Case i
            '        Case 0
            '            mloc = fileReader.ReadLine
            '        Case 1
            '            mdbase = fileReader.ReadLine
            '        Case 2
            '            muser = fileReader.ReadLine
            '        Case 3
            '            mpwd = fileReader.ReadLine
            '    End Select

            'Next

            'If mloc.Trim.Length > 0 Then
            '    mloc = Decrypt(mloc)
            'End If

            'If mdbase.Trim.Length > 0 Then
            '    mdbase = Decrypt(mdbase)
            'End If

            'If muser.Trim.Length > 0 Then
            '    muser = Decrypt(muser)
            'End If

            'If mpwd.Trim.Length > 0 Then
            '    mpwd = Decrypt(mpwd)
            'End If

            'fileReader.Close()

            'Dim myconnectionstring As String = String.Format("Provider=SQLOLEDB;Server={0};Database={1};Uid={2};Pwd={3};", mloc, mdbase, muser, mpwd)
            Dim myconnectionstring As String = String.Format(String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}\dagudang.dat;Jet OLEDB:Database Password='';", Application.StartupPath))

            cn.ConnectionString = myconnectionstring
            cn.Open()

        Catch ex As OleDb.OleDbException
            Throw New Exception(ex.ToString)
        End Try

        Return cn

    End Function

    Public Shared Function GetDataSet(ByVal SQL As String, ByVal cn As OleDbConnection) As DataSet

        Dim adapter As New OleDbDataAdapter(SQL, cn)
        Dim myData As New DataSet
        adapter.Fill(myData)

        adapter.Dispose()

        Return myData
    End Function

    Public Shared Sub Insert_HistBarang_Fsk(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti As String, ByVal nobukti2 As String, ByVal nopol As String, _
                                 ByVal tanggal As String, ByVal kd_gudang As String, ByVal kd_barang As String, ByVal jmlin As Integer, ByVal jmlout As Integer, ByVal jenis As String, ByVal kdsupirsales As String)

        Dim sql As String = String.Format("insert into hbarang_kendaraan (nobukti,nobukti2,tanggal,nopol,kd_gudang,kd_barang,jmlin,jmlout,jenis,supirsales) values('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},'{8}','{9}')", _
                                            nobukti, nobukti2, convert_date_to_ind(tanggal), nopol, kd_gudang, kd_barang, jmlin, jmlout, jenis, kdsupirsales)

        Using comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()
        End Using


    End Sub


End Class
