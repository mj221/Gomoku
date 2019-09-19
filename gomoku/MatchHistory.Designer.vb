<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MatchHistory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MatchHistory))
        Me.lbxMatch = New System.Windows.Forms.ListBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lbxMatch
        '
        Me.lbxMatch.FormattingEnabled = True
        Me.lbxMatch.ItemHeight = 20
        Me.lbxMatch.Location = New System.Drawing.Point(64, 18)
        Me.lbxMatch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lbxMatch.Name = "lbxMatch"
        Me.lbxMatch.Size = New System.Drawing.Size(396, 304)
        Me.lbxMatch.TabIndex = 0
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(201, 332)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(112, 35)
        Me.btnClear.TabIndex = 1
        Me.btnClear.Text = "ClearHistory"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'MatchHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(511, 402)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.lbxMatch)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "MatchHistory"
        Me.Text = "MatchHistory"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lbxMatch As ListBox
    Friend WithEvents btnClear As Button
End Class
