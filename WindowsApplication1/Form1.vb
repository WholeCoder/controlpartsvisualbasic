Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Runtime.InteropServices
Imports WindowsApplication1.TestControlParts

Public Class Form1

    Private Sub ReadSingleRow(ByVal record As IDataRecord)
        Console.WriteLine(String.Format("{0}", record(0)))
        Me.TableName = String.Format("{0}", record(0))
    End Sub

    Private TableName As String = ""

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim input As String
        input = My.Computer.FileSystem.ReadAllText("../../a185.htm")

        Dim tablePrefixName As String = Me.tableTextBox.Text

        Dim templateIdForCheckTemplateAlreadyExists = DatabaseInteractionApi.ReturnTemplateIdIfTemplateExists(tablePrefixName)

        '        If templateIdForCheckTemplateAlreadyExists <> -1 Then
        '            MsgBox("This template already exists.  Please use a new name for your template", , "Template already exists")
        '            Return
        '        End If

        My.Forms.Form2.Text = Now.ToString
        My.Forms.Form2.AutoScroll = True
        My.Forms.Form2.Show()

        Dim fieldSeparatorText = Me.fieldSeparatorTextBox.Text
        Dim tableSeparatorText = Me.tableSeparatorTextBox.Text
        Dim tableColumnSeparatorText = Me.tableColumnSeparator.Text

        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(input, fieldSeparatorText, tableSeparatorText, tableColumnSeparatorText)
        Dim documentStructure As List(Of String) = getListOfKeywordskeywordList.Item("documentstructure")

        Dim x = 100
        Dim y As Integer = 40

        My.Forms.Form2.myObjectSavers = New List(Of SaveToDatabaseObject)

        Dim shouldCreateTableMetadata = False

        Dim template_id As Integer
        If DatabaseInteractionApi.ReturnTemplateIdIfTemplateExists(tablePrefixName) = -1 Then
            template_id = DatabaseInteractionApi.InsertTemplateAndReturnTemplateId(fieldSeparatorText, tableSeparatorText, tableColumnSeparatorText, tablePrefixName, input)
            shouldCreateTableMetadata = True
        Else
            template_id = DatabaseInteractionApi.ReturnTemplateIdIfTemplateExists(tablePrefixName)

        End If


        For Each dEl As String In documentStructure
            If dEl.StartsWith("table") Then

                Dim str = ""
                Dim tlPanel As TableLayoutPanel = New TableLayoutPanel()

                Dim tSaver As TableSaver = New TableSaver()

                tlPanel.Location = New Point(x, y)
                tlPanel.BorderStyle = BorderStyle.FixedSingle
                tlPanel.BackColor = Color.Aqua
                tlPanel.Width = 600

                My.Forms.Form2.Controls.Add(tlPanel)

                For Each de As DictionaryEntry In getListOfKeywordskeywordList
                    Dim columnsForTableCreation As List(Of String) = New List(Of String)()
                    If de.Key.ToString().Equals(dEl) Then
                        Dim tableRowList As List(Of TableRow) = de.Value

                        tlPanel.ColumnCount = 10 ' maxNumberOfColument(tableRowList)
                        tlPanel.RowCount = tableRowList.Count
                        tlPanel.AutoScroll = True


                        ' This loop is for the headers

                        For Each ent As TableRow In tableRowList

                            Dim templateFields = ent.TemplateFields
                            Dim tableTemplateText = ent.TemplateText
                            Dim stringsForHeaders As String = ""

                            Dim hashTableOfParsedDocumentElements As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(tableTemplateText, tableColumnSeparatorText, "NotUsed", "Notused")
                            Dim docStructure = hashTableOfParsedDocumentElements.Item("documentstructure")

                            For currentTableRow As Integer = 0 To 5
                                Dim colCounter As Integer = 0
                                Dim listOfTextBoxes As List(Of TextBox) = New List(Of TextBox)

                                For Each elementInDocumentStructure As String In docStructure
                                    If currentTableRow = 0 Then
                                        Dim newTB2 As New TextBox
                                        newTB2.Name = elementInDocumentStructure
                                        newTB2.Text = elementInDocumentStructure

                                        '                                        templateSavers.Add(New TextBoxSaver(newTB2))
                                        If elementInDocumentStructure.Contains(":") Then
                                            tSaver.AddTableFormatString(elementInDocumentStructure)
                                        End If
                                        tlPanel.Controls.Add(newTB2, colCounter, currentTableRow)
                                        colCounter = colCounter + 1
                                    Else
                                        If Not elementInDocumentStructure.Contains(":") Then

                                            '                                            Dim newTB2 As New TextBox
                                            '                                            newTB2.Name = e2
                                            '
                                            '                                            tlPanel.Controls.Add(newTB2, colCounter, currentTableRow)
                                            colCounter = colCounter + 1
                                            '                                            newTB2.BackColor = Color.Aqua

                                        Else
                                            '                                            If elementInDocumentStructure.Contains(":") And currentTableRow = 1 Then
                                            columnsForTableCreation.Add(elementInDocumentStructure)
                                            '                                            End If
                                            Dim newTB2 As New TextBox
                                            newTB2.Name = elementInDocumentStructure

                                            '                                            templateSavers.Add(New TableSaver(dEl, tableId))
                                            newTB2.Text = "Debugging Code"

                                            listOfTextBoxes.Add(newTB2)

                                            tlPanel.Controls.Add(newTB2, colCounter, currentTableRow)
                                            colCounter = colCounter + 1
                                            '                                            newTB2.BackColor = Color.Aqua
                                        End If
                                    End If
                                Next

                                tSaver.Add(listOfTextBoxes)
                            Next
                            If shouldCreateTableMetadata Then
                                CreateNewTableAndcolumsForNewTemplateAndReturnTableId(template_id, tablePrefixName, dEl, columnsForTableCreation)
                            End If

                            Dim tempRay() As String = dEl.Split(":")
                            Dim table_id As Integer = DatabaseInteractionApi.ReturnTableIdIfTableExists(tablePrefixName & "_" & tempRay(1), template_id)

                            tSaver.tBoxs = tSaver.tBoxs.GetRange(1, tSaver.tBoxs.Count - 1)
                            '                            MessageBox.Show(tSaver.tBoxs.Count & " tSaver.tBoxs exist - ", "The Lorax",
                            '                                            MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk)

                            tSaver.tableFormatString = dEl
                            tSaver.table_id = table_id

                            My.Forms.Form2.myObjectSavers.Add(tSaver)

                            columnsForTableCreation.Clear()
                            Me.TableName = ""
                        Next
                    End If
                    '                    For Each cftc As String In columnsForTableCreation
                    '                    Next
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
                '                y = 40
                '                x = 600 + x
            End If
        Next


        Me.TextBox1.Text = input
        Me.TextBox1.ScrollBars = ScrollBars.Vertical
    End Sub

    Public Sub CreateNewTableAndcolumsForNewTemplateAndReturnTableId(template_id As Integer, templateName As String, tableNameWithTablePrefixAndColumns As String, columnsForTableCreation As List(Of String))

        Dim connectionString As String = "Server = localhost" & "\SQLEXPRESS;Database=ControlParts;" &
                                         "User ID=sa;Password=ssGood&Plenty;"
        Dim tableNameSuffix As String = Split(tableNameWithTablePrefixAndColumns, ":")(1)

        Dim table_id As Integer = DatabaseInteractionApi.InsertTableIntoDatabaseAndReturnTableId(templateName & "_" & tableNameSuffix, template_id)

        For Each e As String In columnsForTableCreation
            Dim sStringCol As String() = Split(e, ":")
            If e.Contains(":") And sStringCol(2).Equals("string") Then
                DatabaseInteractionApi.InsertTableColumnsIntoDatabase(sStringCol(1), table_id, "string")
            ElseIf e.Contains(":") And sStringCol(2).Equals("datetime") Then
                DatabaseInteractionApi.InsertTableColumnsIntoDatabase(sStringCol(1), table_id, "datetime")
            End If
        Next

        Dim queryString As String =
                "Select * From sys.tables Where name = '" & templateName & "_" & tableNameSuffix & "' AND type = 'U';"



        Using connection As New SqlConnection(connectionString)
            Dim command As New SqlCommand(queryString, connection)
            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader()
            While reader.Read()
                ReadSingleRow(CType(reader, IDataRecord))
            End While

            If Me.TableName.Equals("") Then
                reader.Close()
                Dim obj As SqlCommand
                Dim strSQL As String
                obj = connection.CreateCommand()
                strSQL = "CREATE TABLE " & templateName & "_" & tableNameSuffix & "  ("

                For Each e As String In columnsForTableCreation
                    Dim sStringCol As String() = Split(e, ":")
                    If e.Contains(":") And sStringCol(2).Equals("string") Then
                        strSQL += sStringCol(1) & " VARCHAR(30), "
                    ElseIf e.Contains(":") And sStringCol(2).Equals("datetime") Then
                        strSQL += sStringCol(1) & " DATETIME, "
                    End If
                Next
                '                             "LastName  VARCHAR(30), " &
                '                             "FirstName VARCHAR(20), " &
                '                             "Address   VARCHAR(50) " &


                strSQL += "Id int NOT NULL PRIMARY KEY, " & ") "

                obj.CommandText = strSQL
                obj.ExecuteNonQuery()


            End If

            '                reader.Close()
            connection.Close()
        End Using

    End Sub


    Private Sub GetTemplateId(templateName As String)

        '        Console.WriteLine("Saving table:  " & templateName & TableName)

        Dim connectionString As String = "Server = localhost" & "\SQLEXPRESS;Database=ControlParts;" &
                                         "User ID=sa;Password=ssGood&Plenty;"
        Dim tableNameSuffix As String = Split(TableName, ":")(1)

        Dim queryString As String =
                "Select * From temples Where name = '" & templateName & ";"

        Using connection As New SqlConnection(connectionString)
            Dim command As New SqlCommand(queryString, connection)
            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader()
            While reader.Read()
                ReadSingleRow(CType(reader, IDataRecord))
            End While

            Dim style = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or
                        MsgBoxStyle.Critical

            reader.Close()

            connection.Close()
        End Using

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
        '        MsgBox("Copyright 2017", , "About The Lorax")
        My.Forms.AboutBox1.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub HelpGuideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpGuideToolStripMenuItem.Click
        My.Forms.Form3.Show()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub
End Class
