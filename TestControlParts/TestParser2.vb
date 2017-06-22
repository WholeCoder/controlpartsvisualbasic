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
        Dim template As String = "<!DOCTYPE html><html><head><title>Test Template</title></head><body>|table:rubenstable|<tr><td colspan=""3"">Test 3 Header</td></tr>*<tr><td>col</td><td>col 2</td><td>col3</td></tr>*<tr><td>%column:rubenstale%</td><td>%column:ruthstale%</td><td></td></tr>|</body></html>"
        Dim templateHash As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(template, "|", "*")


        Dim tableOptions As List(Of TableRow) = templateHash.Item("table:rubenstable")

        Assert.True(tableOptions.Count = 3)

        Assert.True(tableOptions(0).TemplateText.Equals("<tr><td colspan=""3"">Test 3 Header</td></tr>"))
        Assert.True(tableOptions(0).TemplateFields.Count = 0)

        Assert.True(tableOptions(1).TemplateText.Equals("<tr><td>col</td><td>col 2</td><td>col3</td></tr>"))
        Assert.True(tableOptions(1).TemplateFields.Count = 0)

        Assert.True(tableOptions(2).TemplateText.Equals("<tr><td>%column:rubenstale%</td><td>%column:ruthstale%</td><td></td></tr>"))
        Assert.True(tableOptions(2).TemplateFields.Count = 2)
        Assert.True(tableOptions(2).TemplateFields.Contains("column:rubenstale"))
        Assert.True(tableOptions(2).TemplateFields.Contains("column:ruthstale"))
    End Sub

    <Test()>
    Public Sub TestThatATablesFieldsAreParsedproperly()
        Dim preBreakUpTableRow As String = "<tr><td>%column:partnumber%</td><td>%column:cost%</td><td>%column:voltage%</td></tr>"

        Dim parsedFieldsIntoHashTable As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(preBreakUpTableRow, "%", "NotUsed")
        Assert.IsTrue(parsedFieldsIntoHashTable.Count = 3)
    End Sub

End Class
