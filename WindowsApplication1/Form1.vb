Imports System.Diagnostics.Eventing.Reader

Public Class Form1
    Private Function AddButton(name As String, x As Int64, y As Int64)
        Dim newTB2 As New TextBox
        newTB2.Name = name
        'Set location, size and so on if you like
        newTB2.Location = New Point(x, y)
        Me.Controls.Add(newTB2)
        newTB2.Text = name
        Return newTB2
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim input As String
        input = My.Computer.FileSystem.ReadAllText("../../a185.htm")

        Dim fieldSeparatorText = Me.fieldSeparatorTextBox.Text
        Dim tableSeparatorText = Me.tableSeparatorTextBox.Text

        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(input, fieldSeparatorText, tableSeparatorText)
        Dim documentStructure As List(Of String) = getListOfKeywordskeywordList.Item("documentstructure")

        Dim x = 50
        Dim y As Integer = 200
        For Each dEl As String In documentStructure
            If dEl.StartsWith("table") Then
                MsgBox("Found table:  " & dEl)
                Dim str = ""
                Dim textBoxHeight = 0
                For Each de As DictionaryEntry In getListOfKeywordskeywordList
                    If de.Key.ToString().Equals(dEl) Then
                        Dim tableRowList As List(Of TableRow) = de.Value
                        Dim x2 = 50

                        For Each ent As TableRow In tableRowList
                            For Each e2 As String In ent.TemplateFields
                                Dim but As TextBox = Me.AddButton(e2, x2, y)
                                textBoxHeight = but.Height
                                x2 = x2 + but.Width
                            Next
                        Next
                    End If
                Next de
                y = y + textBoxHeight
            Else
                Dim newTL As TextBox = New TextBox()
                newTL.Text = dEl
                newTL.Width = 300
                newTL.Location = New Point(10, y)
                y = y + newTL.Height
                Me.Controls.Add(newTL)
            End If

        Next



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
