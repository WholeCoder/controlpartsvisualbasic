Imports System.Data
Imports System.Data.SqlClient

Imports WpfControlPartsApplication.WpfControlPartsApplication


Class MainWindow

    Private TableName As String = ""

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Dim input As String
        input = My.Computer.FileSystem.ReadAllText("../../a185.htm")

        Me.TextBox1.Text = input
        '        Me.TextBox1.ScrollBars = ScrollBars.Vertical

        Dim lbl As New Controls.TextBox
        lbl.Text = "New TextBox@@@"
        Me.grd.Children.Add(lbl)
        ' Create a window from the page you need to show
        Dim window As New Form1()

        ' Open your page
        window.Show()

        Dim tablePrefixName As String = Me.tableTextBox.Text

        Dim templateIdForCheckTemplateAlreadyExists = DatabaseInteractionApi.ReturnTemplateIdIfTemplateExists(tablePrefixName)

        '        If templateIdForCheckTemplateAlreadyExists <> -1 Then
        '            MsgBox("This template already exists.  Please use a new name for your template", , "Template already exists")
        '            Return
        '        End If

        '        My.Forms.Form2.Text = Now.ToString
        '        My.Forms.Form2.AutoScroll = True

        Dim fieldSeparatorText = Me.fieldSeparatorTextBox.Text
        Dim tableSeparatorText = Me.tableSeparatorTextBox.Text
        Dim tableColumnSeparatorText = Me.tableColumnSeparator.Text

        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(input, fieldSeparatorText, tableSeparatorText, tableColumnSeparatorText)
        Dim documentStructure As List(Of String) = getListOfKeywordskeywordList.Item("documentstructure")

        Const x As Integer = 100
        Dim y As Integer = 40

        window.myObjectSavers = New List(Of SaveOrLoadFromToDatabaseObject)
        window.myHTMLObjectSavers = New List(Of SaveOrLoadFromToDatabaseObject)

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
                Dim tlPanel As Grid = New Grid()
                tlPanel.Height = 700


                '                Canvas.SetTop(tlPanel, y)
                '                Canvas.SetLeft(tlPanel, x)

                Dim tSaver As TableSaverAndLoader = New TableSaverAndLoader()
                window.myHTMLObjectSavers.Add(tSaver)
                '
                '                tlPanel.Location = New System.Drawing.Point(x, y)
                '                tlPanel.BorderStyle = BorderStyle.FixedSingle
                '                tlPanel.BackColor = Color.Aqua
                tlPanel.Width = 600

                Dim sv As ScrollViewer = New ScrollViewer()
                sv.Height = 300
                sv.Width = tlPanel.Width

                sv.Content = tlPanel
                window.grd.Children.Add(sv)

                Canvas.SetTop(sv, y)
                Canvas.SetLeft(sv, x)

                For Each de As DictionaryEntry In getListOfKeywordskeywordList
                    Dim columnsForTableCreation As List(Of String) = New List(Of String)()
                    If de.Key.ToString().Equals(dEl) Then
                        Dim tableRowList As List(Of TableRw) = de.Value

                        For i As Integer = 1 To 10
                            Dim colDef1 As ColumnDefinition = New ColumnDefinition()
                            tlPanel.ColumnDefinitions.Add(colDef1)
                        Next

                        '                        tlPanel.ColumnCount = 10 ' maxNumberOfColument(tableRowList)
                        '                        tlPanel.RowCount = tableRowList.Count
                        '                        tlPanel.AutoScroll = True


                        ' This loop is for the headers

                        For Each ent As TableRw In tableRowList

                            Dim templateFields = ent.TemplateFields
                            Dim tableTemplateText = ent.TemplateText
                            Dim stringsForHeaders As String = ""

                            Dim hashTableOfParsedDocumentElements As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(tableTemplateText, tableColumnSeparatorText, "NotUsed", "Notused")
                            Dim docStructure = hashTableOfParsedDocumentElements.Item("documentstructure")

                            Dim howManyRowsToCreate = DatabaseInteractionApi.GetNumberOfRowsForTable(tablePrefixName & "_" & dEl.Split(":")(1))

                            For j As Integer = 1 To howManyRowsToCreate
                                Dim rowDef4 As RowDefinition = New RowDefinition()
                                tlPanel.RowDefinitions.Add(rowDef4)
                            Next


                            For currentTableRow As Integer = 0 To howManyRowsToCreate
                                Dim colCounter As Integer = 0
                                Dim listOfTextBoxes As List(Of TextBox) = New List(Of Controls.TextBox)

                                For Each elementInDocumentStructure As String In docStructure
                                    If currentTableRow = 0 Then
                                        Dim newTB2 As New Controls.TextBox
                                        '                                        newTB2.Name = elementInDocumentStructure
                                        newTB2.Text = elementInDocumentStructure

                                        '                                        templateSavers.Add(New TextBoxSaver(newTB2))
                                        If elementInDocumentStructure.Contains(":") Then
                                            tSaver.AddTableFormatString(elementInDocumentStructure)
                                            columnsForTableCreation.Add(elementInDocumentStructure)
                                        End If
                                        '                                        tlPanel.SetRow(newTB2, currentTableRow)
                                        '                                        tlPanel.SetColumn(newTB2, colCounter)
                                        Grid.SetRow(newTB2, currentTableRow)
                                        Grid.SetColumn(newTB2, colCounter)
                                        tlPanel.Children.Add(newTB2)

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

                                            '                                            End If
                                            Dim newTB2 As New Controls.TextBox
                                            '                                            newTB2.Name = elementInDocumentStructure

                                            '                                            templateSavers.Add(New TableSaver(dEl, tableId))
                                            newTB2.Text = ""

                                            listOfTextBoxes.Add(newTB2)
                                            Grid.SetRow(newTB2, currentTableRow)
                                            Grid.SetColumn(newTB2, colCounter)
                                            tlPanel.Children.Add(newTB2)
                                            '                                                tlPanel.Controls.Add(newTB2, colCounter, currentTableRow)
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

                            window.myObjectSavers.Add(tSaver)

                            columnsForTableCreation.Clear()
                            Me.TableName = ""
                        Next
                    End If
                    '                    For Each cftc As String In columnsForTableCreation
                    '                    Next
                Next de

                y = y + sv.Height
            ElseIf dEl.StartsWith("field") Then
                Dim newTL As Controls.TextBox = New Controls.TextBox()
                '                newTL.Multiline = True
                '                newTL.ScrollBars = ScrollBars.Both
                newTL.Text = dEl
                newTL.Width = 600
                newTL.Height = 20
                '                newTL.Location = New System.Windows.Point(x, y)
                window.grd.Children.Add(newTL)
                Canvas.SetTop(newTL, y)
                Canvas.SetLeft(newTL, x)

                y = y + newTL.Height
            Else
                Dim newTL As Controls.TextBox = New Controls.TextBox()
                '                newTL.BackColor = Media.Color.LightGray
                '                newTL.Multiline = True
                '                newTL.ScrollBars = ScrollBars.Both
                newTL.Text = dEl
                newTL.Width = 600
                newTL.Height = 150
                '                newTL.Location = New System.Windows.Point(x, y)
                window.grd.Children.Add(newTL)
                Canvas.SetTop(newTL, y)
                Canvas.SetLeft(newTL, x)
                y = y + newTL.Height
            End If

        Next


        Me.TextBox1.Text = input
        '        Me.TextBox1.VerticalScrollBarVisibility = True
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


                strSQL += "Id int NOT NULL PRIMARY KEY IDENTITY(1,1) " & ") "

                obj.CommandText = strSQL
                obj.ExecuteNonQuery()


            End If

            '                reader.Close()
            connection.Close()
        End Using

    End Sub


    Private Sub ReadSingleRow(ByVal record As IDataRecord)
        Console.WriteLine(String.Format("{0}", record(0)))
        Me.TableName = String.Format("{0}", record(0))
    End Sub

End Class

