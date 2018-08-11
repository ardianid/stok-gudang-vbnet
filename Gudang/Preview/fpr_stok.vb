Imports System.Data
Imports System.Data.OleDb

Imports DevExpress.XtraReports.UI

Public Class fpr_stok


    Private Sub loadfaktur()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim sql As String = "select kd_barang,nama_barang,satuan1,satuan2,satuan3,jmlstok_f1,jmlstok_f2,jmlstok_f3 from ms_barang"

            Dim ds As DataSet = New dsstok
            ds = ClassMy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings

            Dim rinvoice As New rstok With {.DataSource = ds.Tables(0)}
            '  rinvoice.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
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
        loadfaktur()
    End Sub


End Class
