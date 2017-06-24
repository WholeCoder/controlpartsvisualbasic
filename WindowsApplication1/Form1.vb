Public Class Form1
    Private Function AddButton(name As String, x As Int64, y As Int64)
        Dim newTB2 As New TextBox
        newTB2.Name = name
        'Set location, size and so on if you like
        newTB2.Location = New Point(x, y)
        Me.Controls.Add(newTB2)
        newTB2.Text = name
        Return newTB2.Width
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim input As String
        input = My.Computer.FileSystem.ReadAllText("../../a185.htm")

        Dim fieldSeparatorText = Me.fieldSeparatorTextBox.Text
        Dim tableSeparatorText = Me.tableSeparatorTextBox.Text

        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(input, fieldSeparatorText, tableSeparatorText)

        Dim str = ""
        Dim x = 50
        For Each de As DictionaryEntry In getListOfKeywordskeywordList
            If de.Key.ToString().StartsWith("table") Then
                Dim tableRowList As List(Of TableRow) = de.Value

                For Each ent As TableRow In tableRowList
                    For Each e2 As String In ent.TemplateFields
                        Dim Width = Me.AddButton(e2, x, 230)
                        x += Width
                    Next
                Next
            End If
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
