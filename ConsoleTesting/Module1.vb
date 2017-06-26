Imports System.Text.RegularExpressions
Imports WindowsApplication1

Module Module1
     Sub Main()
        Dim preBreakUpTableRow As String = "<tr><td>%column:partnumber%</td><td>%column:cost%</td><td>%column:voltage%</td></tr>"

        Dim parsedFieldsIntoHashTable As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(preBreakUpTableRow, "%", "NotUsed")
        Console.WriteLine(parsedFieldsIntoHashTable.Count)

        For Each Item In parsedFieldsIntoHashTable
            If Item.Key.Equals("documentstructure") Then
                Console.WriteLine("got document structure")
            Else
                Console.WriteLine("Type:  " & Item.Value)
            End If
        Next
        Console.ReadKey()
    End Sub
End Module
