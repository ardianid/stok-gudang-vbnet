Public Class fpr_inoutbarang 

    Private Sub btload_Click(sender As Object, e As EventArgs) Handles btload.Click

        Dim sql1 As String = String.Format("SELECT ms_barang.kelompok,ms_barang.kd_barang, ms_barang.nama_barang,0 as qtys, sum(tradm_gud2.qtyinkecil) as jmin, sum(tradm_gud2.qtyoutkecil) as jout,0 as jopname,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3 " & _
        "FROM (tradm_gud INNER JOIN tradm_gud2 ON tradm_gud.nobukti = tradm_gud2.nobukti) INNER JOIN ms_barang ON tradm_gud2.kd_barang = ms_barang.kd_barang " & _
        "where tradm_gud.sbatal=0 and tradm_gud.tanggal>=#{0}# and tradm_gud.tanggal<=#{1}#", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        sql1 = String.Format("{0} group by ms_barang.kelompok,ms_barang.kd_barang, ms_barang.nama_barang,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3 " & _
        "having sum(tradm_gud2.qtyinkecil)<>0 or sum(tradm_gud2.qtyoutkecil)<>0", sql1)

        Dim sql2 As String = String.Format("SELECT ms_barang.kelompok,ms_barang.kd_barang, ms_barang.nama_barang,0,0,0, sum(tr_opname2.qty_k) - sum(tr_opname2.qty_r),ms_barang.qty1,ms_barang.qty2,ms_barang.qty3 " & _
        "FROM (tr_opname INNER JOIN tr_opname2 ON tr_opname.nobukti = tr_opname2.nobukti) INNER JOIN ms_barang ON tr_opname2.kd_barang = ms_barang.kd_barang " & _
        "where tr_opname.sbatal=0 and tr_opname.tanggal>=#{0}# and tr_opname.tanggal<=#{1}#", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        sql2 = String.Format("{0} group by ms_barang.kelompok,ms_barang.kd_barang, ms_barang.nama_barang,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3 " & _
        "having sum(tr_opname2.qty_k) - sum(tr_opname2.qty_r)<>0", sql2)

        Dim sql3 As String = String.Format("select ms_barang.kelompok,ms_barang.kd_barang,ms_barang.nama_barang,sum(jmlin)- sum(jmlout) as sawal,0,0,0,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3 from hbarang_kendaraan inner join ms_barang on hbarang_kendaraan.kd_barang=ms_barang.kd_barang " & _
        "where tanggal<#{0}# " & _
        "group by ms_barang.kelompok,ms_barang.kd_barang,ms_barang.nama_barang,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3", convert_date_to_eng(ttgl.EditValue))

        Dim sqlgabung As String = String.Format("{0} union all {1} union all {2} ", sql1, sql2, sql3)

        Dim periode As String = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Using fkar2 As New fpr_inoutbarang2 With {.WindowState = FormWindowState.Maximized, .sql = sqlgabung, .periode = periode}
            fkar2.ShowDialog(Me)
        End Using

    End Sub

    Private Sub btkeluar_Click(sender As Object, e As EventArgs) Handles btkeluar.Click
        Me.Close()
    End Sub

    Private Sub fpr_inoutbarang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_inoutbarang_Load(sender As Object, e As EventArgs) Handles Me.Load
        ttgl.EditValue = DateValue(Date.Now)
        ttgl2.EditValue = DateValue(Date.Now)
    End Sub

End Class