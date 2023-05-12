<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RichTextBox3 = New System.Windows.Forms.RichTextBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnFTPSetting = New System.Windows.Forms.Button()
        Me.btnDownload = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFTPURL = New System.Windows.Forms.TextBox()
        Me.txtFolder = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox()
        Me.btnBrowseFTP = New System.Windows.Forms.Button()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(1107, 127)
        Me.RichTextBox1.TabIndex = 1
        Me.RichTextBox1.Text = ""
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RichTextBox1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1107, 542)
        Me.SplitContainer1.SplitterDistance = 411
        Me.SplitContainer1.TabIndex = 2
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnBrowseFTP)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTotal)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnStart)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnFTPSetting)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnDownload)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFTPURL)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFolder)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnBrowse)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer2.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RichTextBox2)
        Me.SplitContainer2.Size = New System.Drawing.Size(1107, 411)
        Me.SplitContainer2.SplitterDistance = 220
        Me.SplitContainer2.TabIndex = 11
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.Color.Blue
        Me.lblTotal.Location = New System.Drawing.Point(120, 81)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(100, 25)
        Me.lblTotal.TabIndex = 13
        Me.lblTotal.Text = "Total JPG"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RichTextBox3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 110)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1107, 110)
        Me.Panel1.TabIndex = 12
        '
        'RichTextBox3
        '
        Me.RichTextBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox3.Font = New System.Drawing.Font("Courier", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBox3.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBox3.Name = "RichTextBox3"
        Me.RichTextBox3.Size = New System.Drawing.Size(1107, 110)
        Me.RichTextBox3.TabIndex = 0
        Me.RichTextBox3.Text = ""
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(864, 39)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(105, 33)
        Me.btnStart.TabIndex = 11
        Me.btnStart.Text = "Read Local"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnFTPSetting
        '
        Me.btnFTPSetting.Location = New System.Drawing.Point(496, 3)
        Me.btnFTPSetting.Name = "btnFTPSetting"
        Me.btnFTPSetting.Size = New System.Drawing.Size(116, 33)
        Me.btnFTPSetting.TabIndex = 10
        Me.btnFTPSetting.Text = "FTP Setting"
        Me.btnFTPSetting.UseVisualStyleBackColor = True
        '
        'btnDownload
        '
        Me.btnDownload.Location = New System.Drawing.Point(864, 0)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(105, 33)
        Me.btnDownload.TabIndex = 9
        Me.btnDownload.Text = "Download"
        Me.btnDownload.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 17)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "FTP URL:"
        '
        'txtFTPURL
        '
        Me.txtFTPURL.Location = New System.Drawing.Point(118, 6)
        Me.txtFTPURL.Name = "txtFTPURL"
        Me.txtFTPURL.ReadOnly = True
        Me.txtFTPURL.Size = New System.Drawing.Size(358, 22)
        Me.txtFTPURL.TabIndex = 7
        '
        'txtFolder
        '
        Me.txtFolder.Location = New System.Drawing.Point(118, 48)
        Me.txtFolder.Name = "txtFolder"
        Me.txtFolder.Size = New System.Drawing.Size(570, 22)
        Me.txtFolder.TabIndex = 4
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(719, 38)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(105, 35)
        Me.btnBrowse.TabIndex = 6
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Local Folder:"
        '
        'RichTextBox2
        '
        Me.RichTextBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox2.Font = New System.Drawing.Font("Courier", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBox2.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.Size = New System.Drawing.Size(1107, 187)
        Me.RichTextBox2.TabIndex = 0
        Me.RichTextBox2.Text = ""
        '
        'btnBrowseFTP
        '
        Me.btnBrowseFTP.Location = New System.Drawing.Point(680, 1)
        Me.btnBrowseFTP.Name = "btnBrowseFTP"
        Me.btnBrowseFTP.Size = New System.Drawing.Size(144, 35)
        Me.btnBrowseFTP.TabIndex = 14
        Me.btnBrowseFTP.Text = "Browse File"
        Me.btnBrowseFTP.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1107, 542)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "FTP File Reader"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Label1 As Label
    Friend WithEvents txtFolder As TextBox
    Friend WithEvents btnBrowse As Button
    Friend WithEvents btnDownload As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtFTPURL As TextBox
    Friend WithEvents btnFTPSetting As Button
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents RichTextBox2 As RichTextBox
    Friend WithEvents btnStart As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents RichTextBox3 As RichTextBox
    Friend WithEvents lblTotal As Label
    Friend WithEvents btnBrowseFTP As Button
End Class
