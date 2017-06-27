Public Class Form3
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim tlPanel As TableLayoutPanel = New TableLayoutPanel()
        tlPanel.BorderStyle = BorderStyle.FixedSingle
        tlPanel.ColumnCount = 10
        tlPanel.RowCount = 10
        Dim B As Button = New Button()
        B.Margin = New Padding(0)
        B.BackColor = Color.Red
        B.Height = tlPanel.Height
        B.Width = tlPanel.Width / 2
        B.Dock = DockStyle.Right
        '        tlPanel.Controls.Add(B, 1, 0)

        tlPanel.Controls.Add(B, 0, 0)
        Me.Controls.Add(tlPanel)
    End Sub
End Class