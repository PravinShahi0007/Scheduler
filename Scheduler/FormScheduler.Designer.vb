<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormScheduler
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormScheduler))
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SettingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.Linfo = New DevExpress.XtraEditors.LabelControl()
        Me.BCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.BSave = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.BDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.BAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.GCSchedule = New DevExpress.XtraGrid.GridControl()
        Me.GVSchedule = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XTPSchedule = New DevExpress.XtraTab.XtraTabPage()
        Me.XTPAttendance = New DevExpress.XtraTab.XtraTabPage()
        Me.BSaveWAR = New DevExpress.XtraEditors.SimpleButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TETime = New DevExpress.XtraEditors.TimeEdit()
        Me.LEDay = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.XTPLeaveRemaining = New DevExpress.XtraTab.XtraTabPage()
        Me.BSaveMonthly = New DevExpress.XtraEditors.SimpleButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TETimeMonthly = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPProduction = New DevExpress.XtraTab.XtraTabPage()
        Me.BProdDuty = New DevExpress.XtraEditors.SimpleButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TETimeDuty = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPEmpAppraisal = New DevExpress.XtraTab.XtraTabPage()
        Me.SBEmpPerApp = New DevExpress.XtraEditors.SimpleButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TEEmpPerApp = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPCashAdvance = New DevExpress.XtraTab.XtraTabPage()
        Me.SBCashAdvance = New DevExpress.XtraEditors.SimpleButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TECashAdvance = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPEvaulationAR = New DevExpress.XtraTab.XtraTabPage()
        Me.BtnEvaluationAR = New DevExpress.XtraEditors.SimpleButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TEEvaluationAR = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPNoticeEmail = New DevExpress.XtraTab.XtraTabPage()
        Me.BtnEmailNoticeAR = New DevExpress.XtraEditors.SimpleButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TEEmailNoticeAR = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPWarningLate = New DevExpress.XtraTab.XtraTabPage()
        Me.SBWarningLate = New DevExpress.XtraEditors.SimpleButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TEWaningLate = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPGetKurs = New DevExpress.XtraTab.XtraTabPage()
        Me.BSaveKurs = New DevExpress.XtraEditors.SimpleButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TETimeKurs = New DevExpress.XtraEditors.TimeEdit()
        Me.LEDayKurs = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.GCSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XTPSchedule.SuspendLayout()
        Me.XTPAttendance.SuspendLayout()
        CType(Me.TETime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LEDay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPLeaveRemaining.SuspendLayout()
        CType(Me.TETimeMonthly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPProduction.SuspendLayout()
        CType(Me.TETimeDuty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPEmpAppraisal.SuspendLayout()
        CType(Me.TEEmpPerApp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPCashAdvance.SuspendLayout()
        CType(Me.TECashAdvance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPEvaulationAR.SuspendLayout()
        CType(Me.TEEvaluationAR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPNoticeEmail.SuspendLayout()
        CType(Me.TEEmailNoticeAR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPWarningLate.SuspendLayout()
        CType(Me.TEWaningLate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPGetKurs.SuspendLayout()
        CType(Me.TETimeKurs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LEDayKurs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer
        '
        Me.Timer.Interval = 1000
        '
        'ContextMenuStrip
        '
        Me.ContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.ContextMenuStrip.Name = "ContextMenuStrip"
        Me.ContextMenuStrip.Size = New System.Drawing.Size(112, 48)
        '
        'SettingToolStripMenuItem
        '
        Me.SettingToolStripMenuItem.Name = "SettingToolStripMenuItem"
        Me.SettingToolStripMenuItem.Size = New System.Drawing.Size(111, 22)
        Me.SettingToolStripMenuItem.Text = "Setting"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(111, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'NotifyIcon
        '
        Me.NotifyIcon.BalloonTipText = "Still running here !"
        Me.NotifyIcon.BalloonTipTitle = "Scheduler PHP"
        Me.NotifyIcon.ContextMenuStrip = Me.ContextMenuStrip
        Me.NotifyIcon.Icon = CType(resources.GetObject("NotifyIcon.Icon"), System.Drawing.Icon)
        Me.NotifyIcon.Text = "Scheduler PHP"
        Me.NotifyIcon.Visible = True
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.Linfo)
        Me.PanelControl1.Controls.Add(Me.BCancel)
        Me.PanelControl1.Controls.Add(Me.BSave)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 189)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(878, 36)
        Me.PanelControl1.TabIndex = 3
        '
        'Linfo
        '
        Me.Linfo.Location = New System.Drawing.Point(12, 11)
        Me.Linfo.Name = "Linfo"
        Me.Linfo.Size = New System.Drawing.Size(86, 13)
        Me.Linfo.TabIndex = 5
        Me.Linfo.Text = "Schedule Stopped"
        '
        'BCancel
        '
        Me.BCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.BCancel.Location = New System.Drawing.Point(733, 2)
        Me.BCancel.Name = "BCancel"
        Me.BCancel.Size = New System.Drawing.Size(71, 32)
        Me.BCancel.TabIndex = 1
        Me.BCancel.Text = "Exit"
        '
        'BSave
        '
        Me.BSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.BSave.Location = New System.Drawing.Point(804, 2)
        Me.BSave.Name = "BSave"
        Me.BSave.Size = New System.Drawing.Size(72, 32)
        Me.BSave.TabIndex = 0
        Me.BSave.Text = "Start"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.BDelete)
        Me.PanelControl2.Controls.Add(Me.BAdd)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(872, 30)
        Me.PanelControl2.TabIndex = 4
        '
        'BDelete
        '
        Me.BDelete.Dock = System.Windows.Forms.DockStyle.Right
        Me.BDelete.Location = New System.Drawing.Point(788, 2)
        Me.BDelete.Name = "BDelete"
        Me.BDelete.Size = New System.Drawing.Size(42, 26)
        Me.BDelete.TabIndex = 1
        Me.BDelete.Text = "-"
        '
        'BAdd
        '
        Me.BAdd.Dock = System.Windows.Forms.DockStyle.Right
        Me.BAdd.Location = New System.Drawing.Point(830, 2)
        Me.BAdd.Name = "BAdd"
        Me.BAdd.Size = New System.Drawing.Size(40, 26)
        Me.BAdd.TabIndex = 0
        Me.BAdd.Text = "+"
        '
        'GCSchedule
        '
        Me.GCSchedule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GCSchedule.Location = New System.Drawing.Point(0, 30)
        Me.GCSchedule.MainView = Me.GVSchedule
        Me.GCSchedule.Name = "GCSchedule"
        Me.GCSchedule.Size = New System.Drawing.Size(872, 131)
        Me.GCSchedule.TabIndex = 5
        Me.GCSchedule.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVSchedule})
        '
        'GVSchedule
        '
        Me.GVSchedule.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn1, Me.GridColumn2})
        Me.GVSchedule.GridControl = Me.GCSchedule
        Me.GVSchedule.Name = "GVSchedule"
        Me.GVSchedule.OptionsBehavior.Editable = False
        Me.GVSchedule.OptionsView.ShowGroupPanel = False
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "id"
        Me.GridColumn3.FieldName = "id_scheduler_attn"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Schedule"
        Me.GridColumn1.FieldName = "schedule"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Time"
        Me.GridColumn2.DisplayFormat.FormatString = "HH:mm:ss"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn2.FieldName = "time_var"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XTPSchedule
        Me.XtraTabControl1.Size = New System.Drawing.Size(878, 189)
        Me.XtraTabControl1.TabIndex = 6
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XTPSchedule, Me.XTPAttendance, Me.XTPLeaveRemaining, Me.XTPProduction, Me.XTPEmpAppraisal, Me.XTPCashAdvance, Me.XTPEvaulationAR, Me.XTPNoticeEmail, Me.XTPWarningLate, Me.XTPGetKurs})
        '
        'XTPSchedule
        '
        Me.XTPSchedule.Controls.Add(Me.GCSchedule)
        Me.XTPSchedule.Controls.Add(Me.PanelControl2)
        Me.XTPSchedule.Name = "XTPSchedule"
        Me.XTPSchedule.Size = New System.Drawing.Size(872, 161)
        Me.XTPSchedule.Text = "Attendance Fingerprint Data"
        '
        'XTPAttendance
        '
        Me.XTPAttendance.Controls.Add(Me.BSaveWAR)
        Me.XTPAttendance.Controls.Add(Me.Label2)
        Me.XTPAttendance.Controls.Add(Me.TETime)
        Me.XTPAttendance.Controls.Add(Me.LEDay)
        Me.XTPAttendance.Controls.Add(Me.Label1)
        Me.XTPAttendance.Name = "XTPAttendance"
        Me.XTPAttendance.Size = New System.Drawing.Size(872, 161)
        Me.XTPAttendance.Text = "Weekly Attendance Report"
        '
        'BSaveWAR
        '
        Me.BSaveWAR.Location = New System.Drawing.Point(190, 41)
        Me.BSaveWAR.Name = "BSaveWAR"
        Me.BSaveWAR.Size = New System.Drawing.Size(66, 23)
        Me.BSaveWAR.TabIndex = 5
        Me.BSaveWAR.Text = "Save"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "At : "
        '
        'TETime
        '
        Me.TETime.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TETime.Location = New System.Drawing.Point(84, 44)
        Me.TETime.Name = "TETime"
        Me.TETime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TETime.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TETime.Size = New System.Drawing.Size(100, 20)
        Me.TETime.TabIndex = 3
        '
        'LEDay
        '
        Me.LEDay.Location = New System.Drawing.Point(84, 11)
        Me.LEDay.Name = "LEDay"
        Me.LEDay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LEDay.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_day", "ID Day", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default], DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("day_name", "Day")})
        Me.LEDay.Size = New System.Drawing.Size(445, 20)
        Me.LEDay.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Run Every : "
        '
        'XTPLeaveRemaining
        '
        Me.XTPLeaveRemaining.Controls.Add(Me.BSaveMonthly)
        Me.XTPLeaveRemaining.Controls.Add(Me.Label3)
        Me.XTPLeaveRemaining.Controls.Add(Me.TETimeMonthly)
        Me.XTPLeaveRemaining.Name = "XTPLeaveRemaining"
        Me.XTPLeaveRemaining.Size = New System.Drawing.Size(872, 161)
        Me.XTPLeaveRemaining.Text = "Leave Remaining"
        '
        'BSaveMonthly
        '
        Me.BSaveMonthly.Location = New System.Drawing.Point(284, 16)
        Me.BSaveMonthly.Name = "BSaveMonthly"
        Me.BSaveMonthly.Size = New System.Drawing.Size(66, 23)
        Me.BSaveMonthly.TabIndex = 10
        Me.BSaveMonthly.Text = "Save"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(154, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Beginning of every month At : "
        '
        'TETimeMonthly
        '
        Me.TETimeMonthly.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TETimeMonthly.Location = New System.Drawing.Point(178, 18)
        Me.TETimeMonthly.Name = "TETimeMonthly"
        Me.TETimeMonthly.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TETimeMonthly.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TETimeMonthly.Size = New System.Drawing.Size(100, 20)
        Me.TETimeMonthly.TabIndex = 8
        '
        'XTPProduction
        '
        Me.XTPProduction.Controls.Add(Me.BProdDuty)
        Me.XTPProduction.Controls.Add(Me.Label4)
        Me.XTPProduction.Controls.Add(Me.TETimeDuty)
        Me.XTPProduction.Name = "XTPProduction"
        Me.XTPProduction.Size = New System.Drawing.Size(872, 161)
        Me.XTPProduction.Text = "Production"
        '
        'BProdDuty
        '
        Me.BProdDuty.Location = New System.Drawing.Point(221, 13)
        Me.BProdDuty.Name = "BProdDuty"
        Me.BProdDuty.Size = New System.Drawing.Size(66, 23)
        Me.BProdDuty.TabIndex = 8
        Me.BProdDuty.Text = "Save"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Duty Reminder at :"
        '
        'TETimeDuty
        '
        Me.TETimeDuty.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TETimeDuty.Location = New System.Drawing.Point(115, 15)
        Me.TETimeDuty.Name = "TETimeDuty"
        Me.TETimeDuty.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TETimeDuty.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TETimeDuty.Size = New System.Drawing.Size(100, 20)
        Me.TETimeDuty.TabIndex = 6
        '
        'XTPEmpAppraisal
        '
        Me.XTPEmpAppraisal.Controls.Add(Me.SBEmpPerApp)
        Me.XTPEmpAppraisal.Controls.Add(Me.Label5)
        Me.XTPEmpAppraisal.Controls.Add(Me.TEEmpPerApp)
        Me.XTPEmpAppraisal.Name = "XTPEmpAppraisal"
        Me.XTPEmpAppraisal.Size = New System.Drawing.Size(872, 161)
        Me.XTPEmpAppraisal.Text = "Employee Performance Appraisal "
        '
        'SBEmpPerApp
        '
        Me.SBEmpPerApp.Location = New System.Drawing.Point(125, 41)
        Me.SBEmpPerApp.Name = "SBEmpPerApp"
        Me.SBEmpPerApp.Size = New System.Drawing.Size(66, 23)
        Me.SBEmpPerApp.TabIndex = 11
        Me.SBEmpPerApp.Text = "Save"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(370, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Monthly Reminder day [emp_per_app_day_1] && [emp_per_app_day_2] at :"
        '
        'TEEmpPerApp
        '
        Me.TEEmpPerApp.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEEmpPerApp.Location = New System.Drawing.Point(19, 43)
        Me.TEEmpPerApp.Name = "TEEmpPerApp"
        Me.TEEmpPerApp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEEmpPerApp.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEEmpPerApp.Size = New System.Drawing.Size(100, 20)
        Me.TEEmpPerApp.TabIndex = 9
        '
        'XTPCashAdvance
        '
        Me.XTPCashAdvance.Controls.Add(Me.SBCashAdvance)
        Me.XTPCashAdvance.Controls.Add(Me.Label6)
        Me.XTPCashAdvance.Controls.Add(Me.TECashAdvance)
        Me.XTPCashAdvance.Name = "XTPCashAdvance"
        Me.XTPCashAdvance.Size = New System.Drawing.Size(872, 161)
        Me.XTPCashAdvance.Text = "Cash Advance"
        '
        'SBCashAdvance
        '
        Me.SBCashAdvance.Location = New System.Drawing.Point(222, 10)
        Me.SBCashAdvance.Name = "SBCashAdvance"
        Me.SBCashAdvance.Size = New System.Drawing.Size(66, 23)
        Me.SBCashAdvance.TabIndex = 14
        Me.SBCashAdvance.Text = "Save"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Daily Reminder at :"
        '
        'TECashAdvance
        '
        Me.TECashAdvance.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TECashAdvance.Location = New System.Drawing.Point(116, 12)
        Me.TECashAdvance.Name = "TECashAdvance"
        Me.TECashAdvance.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TECashAdvance.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TECashAdvance.Size = New System.Drawing.Size(100, 20)
        Me.TECashAdvance.TabIndex = 12
        '
        'XTPEvaulationAR
        '
        Me.XTPEvaulationAR.Controls.Add(Me.BtnEvaluationAR)
        Me.XTPEvaulationAR.Controls.Add(Me.Label7)
        Me.XTPEvaulationAR.Controls.Add(Me.TEEvaluationAR)
        Me.XTPEvaulationAR.Name = "XTPEvaulationAR"
        Me.XTPEvaulationAR.Size = New System.Drawing.Size(872, 161)
        Me.XTPEvaulationAR.Text = "AR - Evaluation"
        '
        'BtnEvaluationAR
        '
        Me.BtnEvaluationAR.Location = New System.Drawing.Point(196, 16)
        Me.BtnEvaluationAR.Name = "BtnEvaluationAR"
        Me.BtnEvaluationAR.Size = New System.Drawing.Size(66, 23)
        Me.BtnEvaluationAR.TabIndex = 17
        Me.BtnEvaluationAR.Text = "Save"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Evaluation at"
        '
        'TEEvaluationAR
        '
        Me.TEEvaluationAR.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEEvaluationAR.Location = New System.Drawing.Point(90, 18)
        Me.TEEvaluationAR.Name = "TEEvaluationAR"
        Me.TEEvaluationAR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEEvaluationAR.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEEvaluationAR.Size = New System.Drawing.Size(100, 20)
        Me.TEEvaluationAR.TabIndex = 15
        '
        'XTPNoticeEmail
        '
        Me.XTPNoticeEmail.Controls.Add(Me.BtnEmailNoticeAR)
        Me.XTPNoticeEmail.Controls.Add(Me.Label8)
        Me.XTPNoticeEmail.Controls.Add(Me.TEEmailNoticeAR)
        Me.XTPNoticeEmail.Name = "XTPNoticeEmail"
        Me.XTPNoticeEmail.Size = New System.Drawing.Size(872, 161)
        Me.XTPNoticeEmail.Text = "AR - Email Pemberitahuan"
        '
        'BtnEmailNoticeAR
        '
        Me.BtnEmailNoticeAR.Location = New System.Drawing.Point(173, 14)
        Me.BtnEmailNoticeAR.Name = "BtnEmailNoticeAR"
        Me.BtnEmailNoticeAR.Size = New System.Drawing.Size(66, 23)
        Me.BtnEmailNoticeAR.TabIndex = 20
        Me.BtnEmailNoticeAR.Text = "Save"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(17, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Email at"
        '
        'TEEmailNoticeAR
        '
        Me.TEEmailNoticeAR.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEEmailNoticeAR.Location = New System.Drawing.Point(67, 16)
        Me.TEEmailNoticeAR.Name = "TEEmailNoticeAR"
        Me.TEEmailNoticeAR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEEmailNoticeAR.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEEmailNoticeAR.Size = New System.Drawing.Size(100, 20)
        Me.TEEmailNoticeAR.TabIndex = 18
        '
        'XTPWarningLate
        '
        Me.XTPWarningLate.Controls.Add(Me.SBWarningLate)
        Me.XTPWarningLate.Controls.Add(Me.Label9)
        Me.XTPWarningLate.Controls.Add(Me.TEWaningLate)
        Me.XTPWarningLate.Name = "XTPWarningLate"
        Me.XTPWarningLate.Size = New System.Drawing.Size(872, 161)
        Me.XTPWarningLate.Text = "Warning Late"
        '
        'SBWarningLate
        '
        Me.SBWarningLate.Location = New System.Drawing.Point(221, 10)
        Me.SBWarningLate.Name = "SBWarningLate"
        Me.SBWarningLate.Size = New System.Drawing.Size(66, 23)
        Me.SBWarningLate.TabIndex = 14
        Me.SBWarningLate.Text = "Save"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(11, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(98, 13)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Daily Reminder at :"
        '
        'TEWaningLate
        '
        Me.TEWaningLate.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEWaningLate.Location = New System.Drawing.Point(115, 12)
        Me.TEWaningLate.Name = "TEWaningLate"
        Me.TEWaningLate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEWaningLate.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEWaningLate.Size = New System.Drawing.Size(100, 20)
        Me.TEWaningLate.TabIndex = 12
        '
        'XTPGetKurs
        '
        Me.XTPGetKurs.Controls.Add(Me.BSaveKurs)
        Me.XTPGetKurs.Controls.Add(Me.Label10)
        Me.XTPGetKurs.Controls.Add(Me.TETimeKurs)
        Me.XTPGetKurs.Controls.Add(Me.LEDayKurs)
        Me.XTPGetKurs.Controls.Add(Me.Label11)
        Me.XTPGetKurs.Name = "XTPGetKurs"
        Me.XTPGetKurs.Size = New System.Drawing.Size(872, 161)
        Me.XTPGetKurs.Text = "Kurs"
        '
        'BSaveKurs
        '
        Me.BSaveKurs.Location = New System.Drawing.Point(190, 41)
        Me.BSaveKurs.Name = "BSaveKurs"
        Me.BSaveKurs.Size = New System.Drawing.Size(66, 23)
        Me.BSaveKurs.TabIndex = 10
        Me.BSaveKurs.Text = "Save"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(11, 47)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(28, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "At : "
        '
        'TETimeKurs
        '
        Me.TETimeKurs.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TETimeKurs.Location = New System.Drawing.Point(84, 44)
        Me.TETimeKurs.Name = "TETimeKurs"
        Me.TETimeKurs.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TETimeKurs.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TETimeKurs.Size = New System.Drawing.Size(100, 20)
        Me.TETimeKurs.TabIndex = 8
        '
        'LEDayKurs
        '
        Me.LEDayKurs.Location = New System.Drawing.Point(84, 11)
        Me.LEDayKurs.Name = "LEDayKurs"
        Me.LEDayKurs.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LEDayKurs.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_day", "ID Day", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default], DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("day_name", "Day")})
        Me.LEDayKurs.Size = New System.Drawing.Size(445, 20)
        Me.LEDayKurs.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(11, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(67, 13)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "Run Every : "
        '
        'FormScheduler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(878, 225)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormScheduler"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Scheduler"
        Me.ContextMenuStrip.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.GCSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XTPSchedule.ResumeLayout(False)
        Me.XTPAttendance.ResumeLayout(False)
        Me.XTPAttendance.PerformLayout()
        CType(Me.TETime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LEDay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPLeaveRemaining.ResumeLayout(False)
        Me.XTPLeaveRemaining.PerformLayout()
        CType(Me.TETimeMonthly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPProduction.ResumeLayout(False)
        Me.XTPProduction.PerformLayout()
        CType(Me.TETimeDuty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPEmpAppraisal.ResumeLayout(False)
        Me.XTPEmpAppraisal.PerformLayout()
        CType(Me.TEEmpPerApp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPCashAdvance.ResumeLayout(False)
        Me.XTPCashAdvance.PerformLayout()
        CType(Me.TECashAdvance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPEvaulationAR.ResumeLayout(False)
        Me.XTPEvaulationAR.PerformLayout()
        CType(Me.TEEvaluationAR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPNoticeEmail.ResumeLayout(False)
        Me.XTPNoticeEmail.PerformLayout()
        CType(Me.TEEmailNoticeAR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPWarningLate.ResumeLayout(False)
        Me.XTPWarningLate.PerformLayout()
        CType(Me.TEWaningLate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPGetKurs.ResumeLayout(False)
        Me.XTPGetKurs.PerformLayout()
        CType(Me.TETimeKurs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LEDayKurs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Timer As Timer
    Friend WithEvents ContextMenuStrip As ContextMenuStrip
    Friend WithEvents SettingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NotifyIcon As NotifyIcon
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Linfo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GCSchedule As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVSchedule As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XTPSchedule As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XTPAttendance As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Label1 As Label
    Friend WithEvents LEDay As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents TETime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents BSaveWAR As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents XTPLeaveRemaining As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BSaveMonthly As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label3 As Label
    Friend WithEvents TETimeMonthly As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPProduction As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BProdDuty As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label4 As Label
    Friend WithEvents TETimeDuty As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPEmpAppraisal As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SBEmpPerApp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label5 As Label
    Friend WithEvents TEEmpPerApp As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPCashAdvance As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SBCashAdvance As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label6 As Label
    Friend WithEvents TECashAdvance As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPEvaulationAR As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BtnEvaluationAR As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label7 As Label
    Friend WithEvents TEEvaluationAR As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPNoticeEmail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BtnEmailNoticeAR As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label8 As Label
    Friend WithEvents TEEmailNoticeAR As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPWarningLate As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SBWarningLate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label9 As Label
    Friend WithEvents TEWaningLate As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPGetKurs As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BSaveKurs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label10 As Label
    Friend WithEvents TETimeKurs As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents LEDayKurs As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label11 As Label
End Class
