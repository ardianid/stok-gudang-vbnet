Imports DevExpress.Skins
Imports DevExpress.LookAndFeel
Imports DevExpress.UserSkins
Imports DevExpress.XtraEditors


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.splitContainerControl = New DevExpress.XtraEditors.SplitContainerControl()
        Me.navBarControl = New DevExpress.XtraNavBar.NavBarControl()
        Me.mailGroup = New DevExpress.XtraNavBar.NavBarGroup()
        Me.inboxItem = New DevExpress.XtraNavBar.NavBarItem()
        Me.outboxItem = New DevExpress.XtraNavBar.NavBarItem()
        Me.draftsItem = New DevExpress.XtraNavBar.NavBarItem()
        Me.trashItem = New DevExpress.XtraNavBar.NavBarItem()
        Me.organizerGroup = New DevExpress.XtraNavBar.NavBarGroup()
        Me.calendarItem = New DevExpress.XtraNavBar.NavBarItem()
        Me.tasksItem = New DevExpress.XtraNavBar.NavBarItem()
        Me.navbarImageListLarge = New System.Windows.Forms.ImageList(Me.components)
        Me.navbarImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.gridControl = New DevExpress.XtraGrid.GridControl()
        Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.bar2 = New DevExpress.XtraBars.Bar()
        Me.mFile = New DevExpress.XtraBars.BarSubItem()
        Me.iNew = New DevExpress.XtraBars.BarButtonItem()
        Me.iOpen = New DevExpress.XtraBars.BarButtonItem()
        Me.iClose = New DevExpress.XtraBars.BarButtonItem()
        Me.iSave = New DevExpress.XtraBars.BarButtonItem()
        Me.iSaveAs = New DevExpress.XtraBars.BarButtonItem()
        Me.iExit = New DevExpress.XtraBars.BarButtonItem()
        Me.mHelp = New DevExpress.XtraBars.BarSubItem()
        Me.iAbout = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.barButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.splitContainerControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainerControl.SuspendLayout()
        CType(Me.navBarControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'splitContainerControl
        '
        Me.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainerControl.Location = New System.Drawing.Point(0, 22)
        Me.splitContainerControl.Name = "splitContainerControl"
        Me.splitContainerControl.Padding = New System.Windows.Forms.Padding(6)
        Me.splitContainerControl.Panel1.Controls.Add(Me.navBarControl)
        Me.splitContainerControl.Panel1.Text = "Panel1"
        Me.splitContainerControl.Panel2.Controls.Add(Me.gridControl)
        Me.splitContainerControl.Panel2.Text = "Panel2"
        Me.splitContainerControl.Size = New System.Drawing.Size(950, 528)
        Me.splitContainerControl.SplitterPosition = 165
        Me.splitContainerControl.TabIndex = 1
        Me.splitContainerControl.Text = "splitContainerControl1"
        '
        'navBarControl
        '
        Me.navBarControl.ActiveGroup = Me.mailGroup
        Me.navBarControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.navBarControl.Groups.AddRange(New DevExpress.XtraNavBar.NavBarGroup() {Me.mailGroup, Me.organizerGroup})
        Me.navBarControl.Items.AddRange(New DevExpress.XtraNavBar.NavBarItem() {Me.inboxItem, Me.outboxItem, Me.draftsItem, Me.trashItem, Me.calendarItem, Me.tasksItem})
        Me.navBarControl.LargeImages = Me.navbarImageListLarge
        Me.navBarControl.Location = New System.Drawing.Point(0, 0)
        Me.navBarControl.Name = "navBarControl"
        Me.navBarControl.OptionsNavPane.ExpandedWidth = 165
        Me.navBarControl.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.ExplorerBar
        Me.navBarControl.Size = New System.Drawing.Size(165, 516)
        Me.navBarControl.SmallImages = Me.navbarImageList
        Me.navBarControl.StoreDefaultPaintStyleName = True
        Me.navBarControl.TabIndex = 1
        Me.navBarControl.Text = "navBarControl1"
        '
        'mailGroup
        '
        Me.mailGroup.Caption = "Mail"
        Me.mailGroup.Expanded = True
        Me.mailGroup.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.inboxItem), New DevExpress.XtraNavBar.NavBarItemLink(Me.outboxItem), New DevExpress.XtraNavBar.NavBarItemLink(Me.draftsItem), New DevExpress.XtraNavBar.NavBarItemLink(Me.trashItem)})
        Me.mailGroup.LargeImageIndex = 0
        Me.mailGroup.Name = "mailGroup"
        '
        'inboxItem
        '
        Me.inboxItem.Caption = "Inbox"
        Me.inboxItem.Name = "inboxItem"
        Me.inboxItem.SmallImageIndex = 0
        '
        'outboxItem
        '
        Me.outboxItem.Caption = "Outbox"
        Me.outboxItem.Name = "outboxItem"
        Me.outboxItem.SmallImageIndex = 1
        '
        'draftsItem
        '
        Me.draftsItem.Caption = "Drafts"
        Me.draftsItem.Name = "draftsItem"
        Me.draftsItem.SmallImageIndex = 2
        '
        'trashItem
        '
        Me.trashItem.Caption = "Trash"
        Me.trashItem.Name = "trashItem"
        Me.trashItem.SmallImageIndex = 3
        '
        'organizerGroup
        '
        Me.organizerGroup.Caption = "Organizer"
        Me.organizerGroup.Expanded = True
        Me.organizerGroup.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.calendarItem), New DevExpress.XtraNavBar.NavBarItemLink(Me.tasksItem)})
        Me.organizerGroup.LargeImageIndex = 1
        Me.organizerGroup.Name = "organizerGroup"
        '
        'calendarItem
        '
        Me.calendarItem.Caption = "Calendar"
        Me.calendarItem.Name = "calendarItem"
        Me.calendarItem.SmallImageIndex = 4
        '
        'tasksItem
        '
        Me.tasksItem.Caption = "Tasks"
        Me.tasksItem.Name = "tasksItem"
        Me.tasksItem.SmallImageIndex = 5
        '
        'navbarImageListLarge
        '
        Me.navbarImageListLarge.ImageStream = CType(resources.GetObject("navbarImageListLarge.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.navbarImageListLarge.TransparentColor = System.Drawing.Color.Transparent
        Me.navbarImageListLarge.Images.SetKeyName(0, "Mail_16x16.png")
        Me.navbarImageListLarge.Images.SetKeyName(1, "Organizer_16x16.png")
        '
        'navbarImageList
        '
        Me.navbarImageList.ImageStream = CType(resources.GetObject("navbarImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.navbarImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.navbarImageList.Images.SetKeyName(0, "Inbox_16x16.png")
        Me.navbarImageList.Images.SetKeyName(1, "Outbox_16x16.png")
        Me.navbarImageList.Images.SetKeyName(2, "Drafts_16x16.png")
        Me.navbarImageList.Images.SetKeyName(3, "Trash_16x16.png")
        Me.navbarImageList.Images.SetKeyName(4, "Calendar_16x16.png")
        Me.navbarImageList.Images.SetKeyName(5, "Tasks_16x16.png")
        '
        'gridControl
        '
        Me.gridControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridControl.Location = New System.Drawing.Point(0, 0)
        Me.gridControl.MainView = Me.gridView1
        Me.gridControl.Name = "gridControl"
        Me.gridControl.Size = New System.Drawing.Size(768, 516)
        Me.gridControl.TabIndex = 1
        Me.gridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridView1})
        '
        'gridView1
        '
        Me.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.gridView1.GridControl = Me.gridControl
        Me.gridView1.Name = "gridView1"
        '
        'barManager
        '
        Me.barManager.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.bar2})
        Me.barManager.DockControls.Add(Me.barDockControlTop)
        Me.barManager.DockControls.Add(Me.barDockControlBottom)
        Me.barManager.DockControls.Add(Me.barDockControlLeft)
        Me.barManager.DockControls.Add(Me.barDockControlRight)
        Me.barManager.Form = Me
        Me.barManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.mFile, Me.barButtonItem2, Me.iOpen, Me.iClose, Me.iNew, Me.iSave, Me.iSaveAs, Me.iExit, Me.mHelp, Me.iAbout})
        Me.barManager.MainMenu = Me.bar2
        Me.barManager.MaxItemId = 12
        '
        'bar2
        '
        Me.bar2.BarName = "Main menu"
        Me.bar2.DockCol = 0
        Me.bar2.DockRow = 0
        Me.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mFile), New DevExpress.XtraBars.LinkPersistInfo(Me.mHelp)})
        Me.bar2.OptionsBar.MultiLine = True
        Me.bar2.OptionsBar.UseWholeRow = True
        Me.bar2.Text = "Main menu"
        '
        'mFile
        '
        Me.mFile.Caption = "&File"
        Me.mFile.Id = 0
        Me.mFile.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.iNew), New DevExpress.XtraBars.LinkPersistInfo(Me.iOpen), New DevExpress.XtraBars.LinkPersistInfo(Me.iClose), New DevExpress.XtraBars.LinkPersistInfo(Me.iSave), New DevExpress.XtraBars.LinkPersistInfo(Me.iSaveAs), New DevExpress.XtraBars.LinkPersistInfo(Me.iExit)})
        Me.mFile.Name = "mFile"
        '
        'iNew
        '
        Me.iNew.Caption = "&New"
        Me.iNew.Id = 6
        Me.iNew.Name = "iNew"
        '
        'iOpen
        '
        Me.iOpen.Caption = "&Open"
        Me.iOpen.Id = 4
        Me.iOpen.Name = "iOpen"
        '
        'iClose
        '
        Me.iClose.Caption = "&Close"
        Me.iClose.Id = 5
        Me.iClose.Name = "iClose"
        '
        'iSave
        '
        Me.iSave.Caption = "&Save"
        Me.iSave.Id = 7
        Me.iSave.Name = "iSave"
        '
        'iSaveAs
        '
        Me.iSaveAs.Caption = "Save &As"
        Me.iSaveAs.Id = 8
        Me.iSaveAs.Name = "iSaveAs"
        '
        'iExit
        '
        Me.iExit.Caption = "E&xit"
        Me.iExit.Id = 9
        Me.iExit.Name = "iExit"
        '
        'mHelp
        '
        Me.mHelp.Caption = "&Help"
        Me.mHelp.Id = 10
        Me.mHelp.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.iAbout)})
        Me.mHelp.Name = "mHelp"
        '
        'iAbout
        '
        Me.iAbout.Caption = "&About"
        Me.iAbout.Id = 11
        Me.iAbout.Name = "iAbout"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(950, 22)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 550)
        Me.barDockControlBottom.Size = New System.Drawing.Size(950, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 22)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 528)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(950, 22)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 528)
        '
        'barButtonItem2
        '
        Me.barButtonItem2.Caption = "Open"
        Me.barButtonItem2.Id = 2
        Me.barButtonItem2.Name = "barButtonItem2"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(950, 550)
        Me.Controls.Add(Me.splitContainerControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.splitContainerControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainerControl.ResumeLayout(False)
        CType(Me.navBarControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents splitContainerControl As DevExpress.XtraEditors.SplitContainerControl
    Private WithEvents barManager As DevExpress.XtraBars.BarManager
    Private WithEvents bar2 As DevExpress.XtraBars.Bar
    Private WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Private WithEvents mFile As DevExpress.XtraBars.BarSubItem
    Private WithEvents barButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iOpen As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iClose As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iNew As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iSave As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iSaveAs As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iExit As DevExpress.XtraBars.BarButtonItem
    Private WithEvents mHelp As DevExpress.XtraBars.BarSubItem
    Private WithEvents iAbout As DevExpress.XtraBars.BarButtonItem
    Private WithEvents navBarControl As DevExpress.XtraNavBar.NavBarControl
    Private WithEvents mailGroup As DevExpress.XtraNavBar.NavBarGroup
    Private WithEvents organizerGroup As DevExpress.XtraNavBar.NavBarGroup
    Private WithEvents inboxItem As DevExpress.XtraNavBar.NavBarItem
    Private WithEvents outboxItem As DevExpress.XtraNavBar.NavBarItem
    Private WithEvents draftsItem As DevExpress.XtraNavBar.NavBarItem
    Private WithEvents trashItem As DevExpress.XtraNavBar.NavBarItem
    Private WithEvents calendarItem As DevExpress.XtraNavBar.NavBarItem
    Private WithEvents tasksItem As DevExpress.XtraNavBar.NavBarItem
    Private WithEvents navbarImageList As System.Windows.Forms.ImageList
    Private WithEvents navbarImageListLarge As System.Windows.Forms.ImageList
    Private WithEvents gridControl As DevExpress.XtraGrid.GridControl
    Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView

End Class
