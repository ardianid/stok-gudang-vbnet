Imports System.Data
Imports System.Data.OleDb

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class fpr_inoutbarang2

    Dim crReportDocument As r_inout

    Public sql As String
    Public periode As String

    Private Sub loadfaktur()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = ClassMy.open_conn

            Dim ds As New DataSet
            ds = ClassMy.GetDataSet(sql, cn)

            Dim ds1 As New ds_inout
            ds1.Clear()
            ds1.Tables(0).Merge(ds.Tables(0))

            crReportDocument = New r_inout
            crReportDocument.SetDataSource(ds1)

            crReportDocument.SetParameterValue("periode", periode)
            crReportDocument.SetParameterValue("userprint", String.Format("User : {0} | Tgl : {1}", userprog, Date.Now))

            crv1.ReportSource = crReportDocument
            crv1.Refresh()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            Cursor = Cursors.Default

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub fpr_inoutbarang2_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadfaktur()
    End Sub

End Class