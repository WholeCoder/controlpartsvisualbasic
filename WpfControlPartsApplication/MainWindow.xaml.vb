Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.Reflection
Imports System.Reflection.Emit
Imports Microsoft.VisualBasic

Imports WpfControlPartsApplication.WpfControlPartsApplication


Class MainWindow

    Private TableName As String = ""

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Dim input As String
        input = My.Computer.FileSystem.ReadAllText("../../a185.htm")

        Me.TextBox1.Text = input

        ' Create a window from the page you need to show
        Dim window As New Form1()

        ' Open your page
        window.Show()

        Dim tablePrefixName As String = Me.tableTextBox.Text

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
                Dim dGrid As DataGrid = New DataGrid()
                dGrid.Height = 700
                dGrid.AutoGenerateColumns = False

                dGrid.Width = 600

                Dim tSaver As TableSaverAndLoader = New TableSaverAndLoader()
                window.myHTMLObjectSavers.Add(tSaver)
                tSaver.dGrid = dGrid


                window.grd.Children.Add(dGrid)

                Canvas.SetTop(dGrid, y)
                Canvas.SetLeft(dGrid, x)

                For Each de As DictionaryEntry In getListOfKeywordskeywordList
                    Dim columnsForTableCreation As List(Of String) = New List(Of String)()
                    If de.Key.ToString().Equals(dEl) Then
                        Dim tableRowList As List(Of TableRw) = de.Value




                        Dim properties As Dictionary(Of String, Type) = New Dictionary(Of String, Type)

                        For i As Integer = 0 To tableRowList(0).TemplateFields.Count - 1
                            Dim nameOfColumn As String = tableRowList(0).TemplateFields(i).Split(":")(1)

                            Dim col1 As DataGridTextColumn =
                                    New DataGridTextColumn()
                            col1.Width = 200
                            col1.Binding = New Binding(nameOfColumn)
                            col1.Header = nameOfColumn

                            dGrid.Columns.Add(col1)

                            properties.Add(nameOfColumn, GetType(String))
                        Next

                        Dim myClazz As Type = CreateClass("MyClazz", properties)
                        tSaver.myClazz = myClazz

                        ' This loop is for the headers

                        For Each ent As TableRw In tableRowList

                            Dim templateFields = ent.TemplateFields
                            Dim tableTemplateText = ent.TemplateText
                            Dim stringsForHeaders As String = ""

                            Dim hashTableOfParsedDocumentElements As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(tableTemplateText, tableColumnSeparatorText, "NotUsed", "Notused")
                            Dim docStructure = hashTableOfParsedDocumentElements.Item("documentstructure")

                            Dim howManyRowsToCreate = DatabaseInteractionApi.GetNumberOfRowsForTable(tablePrefixName & "_" & dEl.Split(":")(1))


                            Dim dataList = {}.ToList()

                            For currentTableRow As Integer = 0 To howManyRowsToCreate - 1
                                Dim colCounter As Integer = 0




                                For Each elementInDocumentStructure As String In docStructure
                                    If currentTableRow = 0 Then
                                        Dim newTB2 As New Controls.TextBox

                                        newTB2.Text = elementInDocumentStructure

                                        If elementInDocumentStructure.Contains(":") Then
                                            tSaver.AddTableFormatString(elementInDocumentStructure)
                                            columnsForTableCreation.Add(elementInDocumentStructure)
                                        End If

                                        colCounter = colCounter + 1
                                    Else
                                        If Not elementInDocumentStructure.Contains(":") Then

                                            colCounter = colCounter + 1

                                        Else
                                            Dim newTB2 As New Controls.TextBox
                                            newTB2.Text = ""

                                            Grid.SetRow(newTB2, currentTableRow)
                                            Grid.SetColumn(newTB2, colCounter)

                                            colCounter = colCounter + 1
                                        End If
                                    End If
                                Next
                            Next
                            If shouldCreateTableMetadata Then
                                CreateNewTableAndcolumsForNewTemplateAndReturnTableId(template_id, tablePrefixName, dEl, columnsForTableCreation)
                            End If

                            Dim tempRay() As String = dEl.Split(":")
                            Dim table_id As Integer = DatabaseInteractionApi.ReturnTableIdIfTableExists(tablePrefixName & "_" & tempRay(1), template_id)

                            tSaver.table_id = table_id

                            columnsForTableCreation.Clear()
                            Me.TableName = ""
                        Next
                    End If
                Next de

                Dim plusButton As New Button
                window.grd.Children.Add(plusButton)
                plusButton.Content = "+"
                plusButton.Width = 50
                Canvas.SetLeft(plusButton, dGrid.Width + x)
                Canvas.SetTop(plusButton, y)

                Dim minusButton As New Button
                window.grd.Children.Add(minusButton)
                minusButton.Width = plusButton.Width
                minusButton.Content = "-"
                Canvas.SetLeft(minusButton, dGrid.Width + x + plusButton.Width)
                Canvas.SetTop(minusButton, y)

                y = y + dGrid.Height
                tSaver.LoadFromDatabase()

            ElseIf dEl.StartsWith("field") Then
                Dim newTL As Controls.TextBox = New Controls.TextBox()

                newTL.Text = dEl
                newTL.Width = 600
                newTL.Height = 20

                window.grd.Children.Add(newTL)
                Canvas.SetTop(newTL, y)
                Canvas.SetLeft(newTL, x)

                y = y + newTL.Height
            Else
                Dim newTL As Controls.TextBox = New Controls.TextBox()

                newTL.Text = dEl
                newTL.Width = 600
                newTL.Height = 150

                window.grd.Children.Add(newTL)
                Canvas.SetTop(newTL, y)
                Canvas.SetLeft(newTL, x)
                y = y + newTL.Height
            End If

        Next


        Me.TextBox1.Text = input
        window.Load_Tables_From_Datase()
    End Sub

    Public Shared Function CreateClass(ByVal className As String, ByVal properties As Dictionary(Of String, Type)) As Type

        Dim myDomain As AppDomain = AppDomain.CurrentDomain
        Dim myAsmName As New AssemblyName("MyAssembly")
        Dim myAssembly As AssemblyBuilder = myDomain.DefineDynamicAssembly(myAsmName, AssemblyBuilderAccess.Run)

        Dim myModule As ModuleBuilder = myAssembly.DefineDynamicModule("MyModule")

        Dim myType As TypeBuilder = myModule.DefineType(className, TypeAttributes.Public)

        myType.DefineDefaultConstructor(MethodAttributes.Public)

        For Each o In properties

            Dim prop As PropertyBuilder = myType.DefineProperty(o.Key, Data.PropertyAttributes.Read Or Data.PropertyAttributes.Write, o.Value, Nothing)

            Dim field As FieldBuilder = myType.DefineField("_" + o.Key, o.Value, FieldAttributes.[Private])

            Dim getter As MethodBuilder = myType.DefineMethod("get_" + o.Key, MethodAttributes.[Public] Or MethodAttributes.SpecialName Or MethodAttributes.HideBySig, o.Value, Type.EmptyTypes)
            Dim getterIL As ILGenerator = getter.GetILGenerator()
            getterIL.Emit(OpCodes.Ldarg_0)
            getterIL.Emit(OpCodes.Ldfld, field)
            getterIL.Emit(OpCodes.Ret)

            Dim setter As MethodBuilder = myType.DefineMethod("set_" + o.Key, MethodAttributes.[Public] Or MethodAttributes.SpecialName Or MethodAttributes.HideBySig, Nothing, New Type() {o.Value})
            Dim setterIL As ILGenerator = setter.GetILGenerator()
            setterIL.Emit(OpCodes.Ldarg_0)
            setterIL.Emit(OpCodes.Ldarg_1)
            setterIL.Emit(OpCodes.Stfld, field)
            setterIL.Emit(OpCodes.Ret)

            prop.SetGetMethod(getter)
            prop.SetSetMethod(setter)

        Next

        Return myType.CreateType()

    End Function

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

