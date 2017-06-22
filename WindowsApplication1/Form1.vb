Public Class Form1
    Private Function AddButton(name As String, x As Int64, y As Int64)
        Dim newTB2 As New TextBox
        newTB2.Name = name
        'Set location, size and so on if you like
        newTB2.Location = New Point(x, y)
        Me.Controls.Add(newTB2)
        newTB2.Text = name
        Return newTB2.Height
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim input As String
        input = My.Computer.FileSystem.ReadAllText("C:\Users\rpierich\Documents\Visual Studio 2013\Projects\controlpartsvisualbasic\TestControlParts\TestTemplates\a185.htm")

        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(input, "|", "*")

        Dim str = ""
        Dim y = 100
        For Each de As DictionaryEntry In getListOfKeywordskeywordList
            Dim height = Me.AddButton(de.Key, 30, y)
            '            Me.TextBox1.Text = de.Value
            y += height
        Next de

        Me.TextBox1.Text = input
        Me.TextBox1.ScrollBars = ScrollBars.Vertical

        '        My.Forms.Form2.Text = Now.ToString
        '        My.Forms.Form2.Show()
    End Sub


    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        MsgBox("Copyright 2017", , "About The Lorax")
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub
End Class
