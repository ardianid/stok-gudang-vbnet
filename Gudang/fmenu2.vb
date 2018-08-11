Public Class fmenu2 

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        finput.MdiParent = Me
        finput.Show()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        fbarang.MdiParent = Me
        fbarang.Show()
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        fkelompok.MdiParent = Me
        fkelompok.Show()
    End Sub

    Private Sub BarButtonItem5_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        fpr_hbarang_kend.MdiParent = Me
        fpr_hbarang_kend.Show()
    End Sub

    Private Sub BarButtonItem6_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        ' fpr_stok.MdiParent = Me
        fpr_stok.ShowDialog()
    End Sub

    Private Sub fmenu2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        fmaster.Enabled = False
        ftrans.Enabled = False
        flap.Enabled = False

        login.MdiParent = Me
        login.Show()
    End Sub

    Private Sub BarButtonItem7_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem7.ItemClick

        Using fform As New fpr_jmin With {.StartPosition = FormStartPosition.CenterScreen, .jenislap = 0}
            fform.ShowDialog()
        End Using

    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem8.ItemClick

        Using fform As New fpr_jmin With {.StartPosition = FormStartPosition.CenterScreen, .jenislap = 1}
            fform.ShowDialog()
        End Using

    End Sub

    Private Sub btopname_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btopname.ItemClick
        fopname.MdiParent = Me
        fopname.Show()
    End Sub

    Private Sub btinoutlap_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btinoutlap.ItemClick
        Using fform As New fpr_inoutbarang With {.StartPosition = FormStartPosition.CenterScreen}
            fform.ShowDialog()
        End Using
    End Sub
End Class