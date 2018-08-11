Imports System.Data
Imports System.Data.OleDb

Imports DevExpress.XtraReports.UI

Public Class fpr_jmin2

    Public sql As String
    Public periode As String
    Public jenislap As Integer

    Private Sub load_data()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim ds As DataSet = New ds_jmlin_orout
            ds = ClassMy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings



            Dim rinvoice As New rjmlin With {.DataSource = ds.Tables(0)}
            rinvoice.xperiode.Text = periode

            If jenislap = 0 Then
                rinvoice.xjudul.Text = "LAPORAN BARANG MASUK"
            Else
                rinvoice.xjudul.Text = "LAPORAN BARANG KELUAR"
            End If

            rinvoice.DataMember = rinvoice.DataMember

            rinvoice.PrinterName = ops.PrinterName
            rinvoice.CreateDocument(True)

            PrintControl1.PrintingSystem = rinvoice.PrintingSystem

         

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

    Private Sub fpr_invoice2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If jenislap = 0 Then
            Me.Text = "Laporan Barang Masuk"
        Else
            Me.Text = "Laporan Barang Keluar"
        End If

        load_data()
    End Sub

End Class