Imports System.Text.RegularExpressions
Imports WindowsApplication1

Module Module1
     Sub Main()
        Dim template As String = "<!DOCTYPE html><html><head><title>Test Template</title></head><body>|table:rubenstable|<tr><td colspan=""3"">Test 3 Header</td></tr>*<tr><td>col</td><td>col 2</td><td>col3</td></tr>*<tr><td>%column:rubenstale%</td><td>%column:ruthstale%</td><td></td></tr>|</body></html>"
        Dim templateHash As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(template, "|", "*")


        Dim tableOptions As List(Of TableRow) = templateHash.Item("table:rubenstable")

        Console.WriteLine("Count:  " & tableOptions.Count)


        For Each tColumn As TableRow In tableOptions
            Console.WriteLine(tColumn.TemplateText & " - count of TableColumns - " & tColumn.TemplateFields().Count)
            For Each Item In tColumn.TemplateFields
                Console.WriteLine("    " & Item.Key)
            Next
        Next

        Console.ReadKey()
    End Sub
End Module
