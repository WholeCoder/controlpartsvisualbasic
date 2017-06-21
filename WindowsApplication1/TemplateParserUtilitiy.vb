Public Class TemplateParserUtilitiy

    Public Shared Function ParseHashTableOfElements(input As String, characterToSplitFields As String, tableCharToSplitFields As String)

        Dim configs As Hashtable = New Hashtable()

        Dim parseInTableOptions = False

        Dim temporaryHolderForTableId = ""
        Dim str = ""
        Dim foundReplacementIdentifier = False
        For Each c As Char In input
            If c = characterToSplitFields And Not foundReplacementIdentifier And Not parseInTableOptions Then
                foundReplacementIdentifier = True
            ElseIf (parseInTableOptions Or foundReplacementIdentifier) And c <> characterToSplitFields Then
                str = str & c
            ElseIf c = characterToSplitFields Then
                foundReplacementIdentifier = False
                If str.StartsWith("field") Or str.StartsWith("column") Then
                    configs.Add(str, str)
                ElseIf str.StartsWith("table") Then
                    parseInTableOptions = True
                    temporaryHolderForTableId = str
                Else
                    parseInTableOptions = False
                    Dim listOfTableOptions() As String = TemplateParserUtilitiy.ConvertTableLanguageToHtmlRows(str, tableCharToSplitFields)
                    '                    Console.WriteLine("table value = " & str)
                    configs.Add(temporaryHolderForTableId, listOfTableOptions)
                    temporaryHolderForTableId = ""
                End If
                str = ""
            End If

        Next

        Return configs
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
