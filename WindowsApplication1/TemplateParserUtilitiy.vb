Public Class TemplateParserUtilitiy

    Public Shared Function ParseHashTableOfElements(input As String, characterToSplitFields As String, tableCharToSplitFields As String)
        Console.WriteLine("Executing")

        Dim configs As Hashtable = New Hashtable()

        Dim parseInTableOptions = False

        Dim temporaryHolderForTableId = ""
        Dim str = ""
        Dim foundReplacementIdentifier = False
        For Each c As Char In input
            If foundStartOfField(c, characterToSplitFields) And Not foundReplacementIdentifier And Not parseInTableOptions Then
                foundReplacementIdentifier = True
            ElseIf ShouldReadInFieldOptions(foundReplacementIdentifier, parseInTableOptions) And DidntFindEndOfOptionsField(c, characterToSplitFields) Then
                str = str & c
            ElseIf c = characterToSplitFields Then
                foundReplacementIdentifier = False
                If str.StartsWith("field") Or str.StartsWith("column") Then
                    configs.Add(str, str)
                    Console.WriteLine("Found field :  " & str)
                ElseIf str.StartsWith("table") Then
                    Console.WriteLine("   Found Table")
                    parseInTableOptions = True
                    temporaryHolderForTableId = str
                Else
                    parseInTableOptions = False
                    Dim listOfTableOptionsI() As String = TemplateParserUtilitiy.ConvertTableLanguageToHtmlRows(str, tableCharToSplitFields)

                    Console.WriteLine("Filling up table options hashes")
                    Dim listOfTableColumns As List(Of TableRow) = New List(Of TableRow)

                    For Each tOp As String In listOfTableOptionsI
                        Console.WriteLine("   parsing tableOption " & tOp)

                        Dim tOptionRow As TableRow = New TableRow()
                        Dim toPFields As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(tOp, "%", "notused")

                        tOptionRow.TemplateFields() = toPFields
                        tOptionRow.TemplateText = tOp


                        listOfTableColumns.Add(tOptionRow)
                    Next

                    configs.Add(temporaryHolderForTableId, listOfTableColumns)

                    temporaryHolderForTableId = ""
                End If
                str = ""
            End If

        Next

        Return configs
    End Function

    Private Shared Function foundStartOfField(c As Char, characterToSplitFields As String) As Boolean

        Return c = characterToSplitFields
    End Function

    Private Shared Function DidntFindEndOfOptionsField(c As Char, characterToSplitFields As String) As Boolean

        Return c <> characterToSplitFields
    End Function

    Private Shared Function ShouldReadInFieldOptions(foundReplacementIdentifier As Boolean, parseInTableOptions As Boolean) As Boolean

        Return (parseInTableOptions Or foundReplacementIdentifier)
    End Function

    Public Shared Function ConvertTableLanguageToHtmlTable(tableConfiguration As String, tableCharToSplitFields As String)
        Dim tableOptionList() As String = TemplateParserUtilitiy.ConvertTableLanguageToHtmlRows(tableConfiguration, tableCharToSplitFields)
        Dim tableString As String = ""
        For Each toString As String In tableOptionList
            tableString = tableString & toString
        Next
        Return "<table>" & tableConfiguration & "</table>"
    End Function

    Public Shared Function ConvertTableLanguageToHtmlRows(tableConfiguration As String, tableCharToSplitFields As String)
        Dim substrings() As String = tableConfiguration.Split(tableCharToSplitFields)
        Return substrings
    End Function
End Class
