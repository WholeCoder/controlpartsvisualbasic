Imports System.CodeDom.Compiler
Imports WindowsApplication1
Imports NUnit.Framework

Public Class TestParser2

    <Test()>
    Public Sub SeeIfTableIdentifierCanhaveItsOptionsParsed()

        Dim rawTableSTring As String = "<tr><td colspan=""3"">Test 3 Header</td></tr>*<tr><td>col</td><td>col 2</td><td>col3</td></tr>*<tr><td></td><td></td><td></td></tr>"

        Dim parsedTableList() As String = TemplateParserUtilitiy.ConvertTableLanguageToHtmlRows(rawTableSTring)

        Assert.IsTrue(parsedTableList.Count() = 3)

        Assert.IsTrue(parsedTableList.Contains("<tr><td colspan=""3"">Test 3 Header</td></tr>"))
        Assert.IsTrue(parsedTableList.Contains("<tr><td>col</td><td>col 2</td><td>col3</td></tr>"))
        Assert.IsTrue(parsedTableList.Contains("<tr><td></td><td></td><td></td></tr>"))

        '        Dim tableAsHTMLString As String = TemplateParserUtilitiy.ConvertTableLanguageToHtmlTable(tableOptionsString)
    End Sub


End Class
