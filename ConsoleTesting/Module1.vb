Imports System.Text.RegularExpressions
Imports WindowsApplication1

Module Module1
     Sub Main()
        Dim preBreakUpTableRow As String = "|table:rubenstable|<tr><td>%column:partnumber%</td><td>%column:cost%</td><td>%column:voltage%</td></tr>|"

        Dim hashOfTemplete As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(preBreakUpTableRow, "|", "*")

        '        Assert.IsTrue(hashOfTemplete.Count = 1)

        Dim tableParameterRowList As List(Of TableRow) = hashOfTemplete.Item("table:rubenstable")
        '        Assert.True(tableParameterRowList.Count = 3)
        Console.WriteLine("Count:  " & tableParameterRowList.Item(0).TemplateFields.Count)

        Console.ReadKey()
    End Sub
End Module
