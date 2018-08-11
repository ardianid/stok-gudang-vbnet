<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class finput2
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(finput2))
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl = New DevExpress.XtraEditors.DateEdit()
        Me.tbukti = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.tbukti_tr = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.tket = New DevExpress.XtraEditors.MemoEdit()
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cbarang = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cjmlin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cjmlout = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.csatuan = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ckd_barang = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.btadd = New DevExpress.XtraEditors.SimpleButton()
        Me.btdel = New DevExpress.XtraEditors.SimpleButton()
        Me.tnopol = New DevExpress.XtraEditors.TextEdit()
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbukti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbukti_tr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnopol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(22, 37)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl4.TabIndex = 159
        Me.LabelControl4.Text = "Tanggal :"
        '
        'ttgl
        '
        Me.ttgl.EditValue = Nothing
        Me.ttgl.Location = New System.Drawing.Point(73, 34)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl.Properties.Mask.EditMask = ""
        Me.ttgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl.Size = New System.Drawing.Size(114, 20)
        Me.ttgl.TabIndex = 1
        '
        'tbukti
        '
        Me.tbukti.Location = New System.Drawing.Point(73, 8)
        Me.tbukti.Name = "tbukti"
        Me.tbukti.Properties.ReadOnly = True
        Me.tbukti.Size = New System.Drawing.Size(114, 20)
        Me.tbukti.TabIndex = 0
        '
        'LabelControl15
        '
        Me.LabelControl15.Location = New System.Drawing.Point(21, 11)
        Me.LabelControl15.Name = "LabelControl15"
        Me.LabelControl15.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl15.TabIndex = 158
        Me.LabelControl15.Text = "No Bukti :"
        '
        'tbukti_tr
        '
        Me.tbukti_tr.Location = New System.Drawing.Point(73, 60)
        Me.tbukti_tr.Name = "tbukti_tr"
        Me.tbukti_tr.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbukti_tr.Size = New System.Drawing.Size(114, 20)
        Me.tbukti_tr.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(21, 63)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl3.TabIndex = 165
        Me.LabelControl3.Text = "No Nota :"
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(226, 11)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl7.TabIndex = 171
        Me.LabelControl7.Text = "No Polisi :"
        '
        'LabelControl11
        '
        Me.LabelControl11.Location = New System.Drawing.Point(249, 37)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(23, 13)
        Me.LabelControl11.TabIndex = 184
        Me.LabelControl11.Text = "Ket :"
        '
        'tket
        '
        Me.tket.Location = New System.Drawing.Point(278, 34)
        Me.tket.Name = "tket"
        Me.tket.Size = New System.Drawing.Size(192, 46)
        Me.tket.TabIndex = 4
        '
        'grid1
        '
        Me.grid1.Location = New System.Drawing.Point(12, 101)
        Me.grid1.MainView = Me.GridView2
        Me.grid1.Name = "grid1"
        Me.grid1.Size = New System.Drawing.Size(575, 200)
        Me.grid1.TabIndex = 189
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cbarang, Me.cjmlin, Me.cjmlout, Me.csatuan, Me.ckd_barang})
        Me.GridView2.GridControl = Me.grid1
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.GridView2.OptionsView.ShowViewCaption = True
        Me.GridView2.ViewCaption = "B A R A N G"
        '
        'cbarang
        '
        Me.cbarang.Caption = "Barang"
        Me.cbarang.FieldName = "nama_barang"
        Me.cbarang.Name = "cbarang"
        Me.cbarang.OptionsColumn.AllowEdit = False
        Me.cbarang.Visible = True
        Me.cbarang.VisibleIndex = 1
        Me.cbarang.Width = 459
        '
        'cjmlin
        '
        Me.cjmlin.AppearanceCell.Options.UseTextOptions = True
        Me.cjmlin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cjmlin.AppearanceHeader.Options.UseTextOptions = True
        Me.cjmlin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cjmlin.Caption = "Jml In"
        Me.cjmlin.DisplayFormat.FormatString = "n0"
        Me.cjmlin.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cjmlin.FieldName = "qtyin"
        Me.cjmlin.Name = "cjmlin"
        Me.cjmlin.OptionsColumn.AllowEdit = False
        Me.cjmlin.Visible = True
        Me.cjmlin.VisibleIndex = 2
        Me.cjmlin.Width = 73
        '
        'cjmlout
        '
        Me.cjmlout.AppearanceCell.Options.UseTextOptions = True
        Me.cjmlout.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cjmlout.AppearanceHeader.Options.UseTextOptions = True
        Me.cjmlout.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cjmlout.Caption = "Jml Out"
        Me.cjmlout.DisplayFormat.FormatString = "n0"
        Me.cjmlout.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cjmlout.FieldName = "qtyout"
        Me.cjmlout.Name = "cjmlout"
        Me.cjmlout.OptionsColumn.AllowEdit = False
        Me.cjmlout.Visible = True
        Me.cjmlout.VisibleIndex = 3
        Me.cjmlout.Width = 60
        '
        'csatuan
        '
        Me.csatuan.Caption = "Satuan"
        Me.csatuan.FieldName = "satuan"
        Me.csatuan.Name = "csatuan"
        Me.csatuan.OptionsColumn.AllowEdit = False
        Me.csatuan.Visible = True
        Me.csatuan.VisibleIndex = 4
        Me.csatuan.Width = 163
        '
        'ckd_barang
        '
        Me.ckd_barang.Caption = "Kode"
        Me.ckd_barang.FieldName = "kd_barang"
        Me.ckd_barang.Name = "ckd_barang"
        Me.ckd_barang.OptionsColumn.AllowEdit = False
        Me.ckd_barang.Visible = True
        Me.ckd_barang.VisibleIndex = 0
        Me.ckd_barang.Width = 74
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(529, 318)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(58, 32)
        Me.btclose.TabIndex = 8
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(465, 318)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(58, 32)
        Me.btsimpan.TabIndex = 7
        Me.btsimpan.Text = "&Simpan"
        '
        'btadd
        '
        Me.btadd.Image = CType(resources.GetObject("btadd.Image"), System.Drawing.Image)
        Me.btadd.Location = New System.Drawing.Point(12, 307)
        Me.btadd.Name = "btadd"
        Me.btadd.Size = New System.Drawing.Size(75, 23)
        Me.btadd.TabIndex = 5
        Me.btadd.Text = "&Tambah"
        Me.btadd.ToolTip = "Tambah Barang"
        '
        'btdel
        '
        Me.btdel.Image = CType(resources.GetObject("btdel.Image"), System.Drawing.Image)
        Me.btdel.Location = New System.Drawing.Point(93, 307)
        Me.btdel.Name = "btdel"
        Me.btdel.Size = New System.Drawing.Size(75, 23)
        Me.btdel.TabIndex = 6
        Me.btdel.Text = "&Hapus"
        Me.btdel.ToolTip = "Hapus Barang"
        '
        'tnopol
        '
        Me.tnopol.Location = New System.Drawing.Point(278, 8)
        Me.tnopol.Name = "tnopol"
        Me.tnopol.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tnopol.Size = New System.Drawing.Size(192, 20)
        Me.tnopol.TabIndex = 3
        '
        'finput2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(601, 356)
        Me.Controls.Add(Me.tnopol)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.btadd)
        Me.Controls.Add(Me.btdel)
        Me.Controls.Add(Me.LabelControl11)
        Me.Controls.Add(Me.tket)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.tbukti_tr)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.ttgl)
        Me.Controls.Add(Me.tbukti)
        Me.Controls.Add(Me.LabelControl15)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "finput2"
        Me.Text = "Input Data"
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbukti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbukti_tr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnopol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents tbukti As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbukti_tr As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tket As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cbarang As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cjmlin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cjmlout As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents csatuan As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ckd_barang As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btdel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnopol As DevExpress.XtraEditors.TextEdit
End Class
