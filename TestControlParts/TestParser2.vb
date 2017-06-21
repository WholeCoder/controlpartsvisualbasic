Imports System.CodeDom.Compiler
Imports WindowsApplication1
Imports NUnit.Framework

Public Class TestParser2

    <Test()>
    Public Sub SeeIfTableIdentifierCanhaveItsOptionsParsed()

        Dim rawTableSTring As String = "<tr><td colspan=""3"">Test 3 Header</td></tr>*<tr><td>col</td><td>col 2</td><td>col3</td></tr>*<tr><td></td><td></td><td></td></tr>"

        Dim parsedTableList() As String = TemplateParserUtilitiy.ConvertTableLanguageToHtmlRows(rawTableSTring, "*")

        Assert.IsTrue(parsedTableList.Count() = 3)

        Assert.IsTrue(parsedTableList.Contains("<tr><td colspan=""3"">Test 3 Header</td></tr>"))
        Assert.IsTrue(parsedTableList.Contains("<tr><td>col</td><td>col 2</td><td>col3</td></tr>"))
        Assert.IsTrue(parsedTableList.Contains("<tr><td></td><td></td><td></td></tr>"))
    End Sub

    <Test()>
    Public Sub SeeIfTableArgumentsCanBeParsedOutIntoObjects()
        Dim template As String = "<!DOCTYPE html><html><head><title>Test Template</title></head><body>|table:rubenstable|<tr><td colspan=""3"">Test 3 Header</td></tr>*<tr><td>col</td><td>col 2</td><td>col3</td></tr>*<tr><td></td><td></td><td></td></tr>|</body></html>"
        Dim templateHash As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(template, "|", "*")

        Assert.IsTrue(templateHash.Contains("table:rubenstable"))

        Dim tableOptions() As String = templateHash.Item("table:rubenstable")

        Assert.IsTrue(tableOptions.Count() = 3)

        Assert.IsTrue(tableOptions.Contains("<tr><td colspan=""3"">Test 3 Header</td></tr>"))
        Assert.IsTrue(tableOptions.Contains("<tr><td>col</td><td>col 2</td><td>col3</td></tr>"))
        Assert.IsTrue(tableOptions.Contains("<tr><td></td><td></td><td></td></tr>"))

    End Sub

    <Test()>
    Public Sub TestRegularExpressionsForTdElements()
        Dim preBreakUpTableRow As String = "<tr><td>%column:partnumber%</td><td>%column:cost%</td><td>%column:voltage%</td></tr>"

        Dim parsedFieldsIntoHashTable As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(preBreakUpTableRow, "%", "NotUsed")
        Assert.IsTrue(parsedFieldsIntoHashTable.Count = 3)
    End Sub

End Class
