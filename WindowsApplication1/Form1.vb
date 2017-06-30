Imports System.Diagnostics.Eventing.Reader

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim input As String
        input = My.Computer.FileSystem.ReadAllText("../../a185.htm")

        My.Forms.Form2.Text = Now.ToString
        My.Forms.Form2.Show()

        '        My.Forms.Form3.Show()

        Dim fieldSeparatorText = Me.fieldSeparatorTextBox.Text
        Dim tableSeparatorText = Me.tableSeparatorTextBox.Text
        Dim tableColumnSeparatorText = Me.tableColumnSeparator.Text

        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(input, fieldSeparatorText, tableSeparatorText, tableColumnSeparatorText)
        Dim documentStructure As List(Of String) = getListOfKeywordskeywordList.Item("documentstructure")

        Dim x = 100
        Dim y As Integer = 40
        For Each dEl As String In documentStructure
            If dEl.StartsWith("table") Then
                Dim str = ""
                Dim textBoxHeight = 0
                Dim tlPanel As TableLayoutPanel = New TableLayoutPanel()

                tlPanel.Location = New Point(x, y)
                tlPanel.BorderStyle = BorderStyle.FixedSingle
                tlPanel.BackColor = Color.Aqua
                tlPanel.Width = 600

                My.Forms.Form2.Controls.Add(tlPanel)

                For Each de As DictionaryEntry In getListOfKeywordskeywordList
                    If de.Key.ToString().Equals(dEl) Then
                        Dim tableRowList As List(Of TableRow) = de.Value
                        Dim x2 = x

                        tlPanel.ColumnCount = 10 ' maxNumberOfColument(tableRowList)
                        tlPanel.RowCount = tableRowList.Count
                        tlPanel.AutoScroll = True


                        ' This loop is for the headers


                        Dim colCounter As Integer = 0
                        For Each ent As TableRow In tableRowList

                            Dim templateFields = ent.TemplateFields
                            Dim tableTemplateText = ent.TemplateText
                            Dim stringsForHeaders As String = ""

                            Dim ht As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(tableTemplateText, tableColumnSeparatorText, "NotUsed", "Notused")
                            Dim st2 = ht.Item("documentstructure")

                            For Each e2 As String In st2

                                Dim newTB2 As New TextBox
                                newTB2.Name = e2
                                '                                newTB2.Location = New Point(x2, y)
                                '                                My.Forms.Form2.Controls.Add(newTB2)
                                newTB2.Text = e2
                                Dim but As TextBox = newTB2
                                tlPanel.Controls.Add(newTB2, colCounter, 0)
                                textBoxHeight = newTB2.Height
                                colCounter = colCounter + 1
                                x2 = x2 + newTB2.Width
                            Next
                        Next
                        colCounter = colCounter + 1
                    End If
                Next de
                y = y + tlPanel.Height
            ElseIf dEl.StartsWith("field") Then
                Dim newTL As TextBox = New TextBox()
                newTL.Multiline = True
                newTL.ScrollBars = ScrollBars.Both
                newTL.Text = dEl
                newTL.Width = 600
                newTL.Height = 20
                newTL.Location = New Point(x, y)
                My.Forms.Form2.Controls.Add(newTL)
                y = y + newTL.Height
            Else
                Dim newTL As TextBox = New TextBox()
                newTL.BackColor = Color.LightGray
                newTL.Multiline = True
                newTL.ScrollBars = ScrollBars.Both
                newTL.Text = dEl
                newTL.Width = 600
                newTL.Height = 150
                newTL.Location = New Point(x, y)
                My.Forms.Form2.Controls.Add(newTL)
                y = y + newTL.Height
            End If
            If y > My.Forms.Form2.Height Then
                y = 40
                x = 600 + x
            End If
        Next


        Me.TextBox1.Text = input
        Me.TextBox1.ScrollBars = ScrollBars.Vertical
    End Sub

    Private Function maxNumberOfColument(tableRowList As List(Of TableRow)) As Integer
        Dim maxNumberOfCols As Integer = Integer.MinValue
        For Each tr As TableRow In tableRowList
            If tr.TemplateFields.Count > maxNumberOfCols Then
                maxNumberOfCols = tr.TemplateFields.Count
            End If
        Next
        Return maxNumberOfCols
    End Function


    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        MsgBox("Copyright 2017", , "About The Lorax")
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub HelpGuideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpGuideToolStripMenuItem.Click
        My.Forms.Form3.Show()
    End Sub
End Class
