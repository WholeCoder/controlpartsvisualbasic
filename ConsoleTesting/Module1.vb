Imports System.Text.RegularExpressions
Imports WindowsApplication1

Module Module1
     Sub Main()
        Dim preBreakUpTableRow As String = "<tr><td>%column:partnumber%</td><td>%column:cost%</td><td>%column:voltage%</td></tr>"

        Dim parsedFieldsIntoHashTable As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(preBreakUpTableRow, "%", "")

        For Each entry In parsedFieldsIntoHashTable
            Console.WriteLine("{0}", entry.Key)

        Next

        Console.ReadKey()
    End Sub
End Module
