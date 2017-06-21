Public Class TemplateParserUtilitiy

    Public Shared Function ParseHashTableOfElements(input As String)

        Dim configs As Hashtable = New Hashtable()

        Dim parseInTableOptions = False

        Dim temporaryHolderForTableId = ""
        Dim str = ""
        Dim foundReplacementIdentifier = False
        For Each c As Char In input
            If c = "|" And Not foundReplacementIdentifier And Not parseInTableOptions Then
                foundReplacementIdentifier = True
            ElseIf (parseInTableOptions Or foundReplacementIdentifier) And c <> "|" Then
                str = str & c
            ElseIf c = "|" Then
                foundReplacementIdentifier = False
                If str.StartsWith("field") Then
                    configs.Add(str, str)
                ElseIf str.StartsWith("table") Then
                    parseInTableOptions = True
                    temporaryHolderForTableId = str
                Else
                    parseInTableOptions = False
                    Console.WriteLine("table value = " & str)
                    configs.Add(temporaryHolderForTableId, str)
                    temporaryHolderForTableId = ""
                End If
                str = ""
            End If

        Next

        Return configs
    End Function

    Public Shared Function ConvertTableLanguageToHtmlTable(tableConfiguration As String)
        Dim tableOptionList() As String = TemplateParserUtilitiy.ConvertTableLanguageToHtmlRows(tableConfiguration)
        Dim tableString As String = ""
        For Each toString As String In tableOptionList
            tableString = tableString & toString
        Next
        Return "<table>" & tableConfiguration & "</table>"
    End Function

    Public Shared Function ConvertTableLanguageToHtmlRows(tableConfiguration As String)
        Dim substrings() As String = tableConfiguration.Split("*")
        Return substrings
    End Function
End Class
