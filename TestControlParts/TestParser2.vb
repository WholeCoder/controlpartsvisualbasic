﻿Imports System.CodeDom.Compiler
Imports NUnit.Framework
Imports WpfControlPartsApplication.WpfControlPartsApplication

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
        Dim template As String = "<!DOCTYPE html><html><head><title>Test Template</title></head><body><table>|table:rubenstable|<tr><td colspan=""3"">Test 3 Header</td></tr>*<tr><td>col</td><td>col 2</td><td>col3</td></tr>*<tr><td>%column:rubenstale%</td><td>%column:ruthstale%</td><td></td></tr>|</table></body></html>"
        Dim templateHash As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(template, "|", "*", "%")


        Dim tableOptions As List(Of TableRw) = templateHash.Item("table:rubenstable")

        Assert.True(tableOptions.Count = 3)

        Assert.True(tableOptions(0).TemplateText.Equals("<tr><td colspan=""3"">Test 3 Header</td></tr>"))
        Assert.True(tableOptions(0).TemplateFields.Count = 0)

        Assert.True(tableOptions(1).TemplateText.Equals("<tr><td>col</td><td>col 2</td><td>col3</td></tr>"))
        Assert.True(tableOptions(1).TemplateFields.Count = 0)

        Assert.True(tableOptions(2).TemplateText.Equals("<tr><td>%column:rubenstale%</td><td>%column:ruthstale%</td><td></td></tr>"))
        Assert.True(tableOptions(2).TemplateFields.Count = 2)
        Assert.True(tableOptions(2).TemplateFields.Contains("column:rubenstale:string"))
        '        Assert.True(tableOptions(2).TemplateFields.Item("column:rubenstale").Equals(1))
        Assert.True(tableOptions(2).TemplateFields.Contains("column:ruthstale:string"))
        '        Assert.True(tableOptions(2).TemplateFields.Item("column:ruthstale").Equals(2))
    End Sub

    <Test()>
    Public Sub TestThatATablesFieldsAreParsedproperly()
        Dim preBreakUpTableRow As String = "<tr><td>%column:partnumber%</td><td>%column:cost%</td><td>%column:voltage%</td></tr>"

        Dim parsedFieldsIntoHashTable As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(preBreakUpTableRow, "%", "NotUsed", "NotUsed")
        Assert.IsTrue(parsedFieldsIntoHashTable.Count = 4)
    End Sub

    <Test()>
    Public Sub TestThatWeCanSortTableFields()
        Dim preBreakUpTableRow As String = "|table:rubenstable|<tr><td>%column:partnumber:number%</td><td>%column:cost:number%</td><td>%column:voltage:string%</td></tr>|"

        Dim hashOfTemplete As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(preBreakUpTableRow, "|", "*", "%")

        Dim tableParameterRowList As List(Of TableRw) = hashOfTemplete.Item("table:rubenstable")
        Assert.True(tableParameterRowList.Item(0).TemplateFields.Count = 3)

        Assert.True(tableParameterRowList.Item(0).TemplateFields.Item(0).Equals("column:partnumber:number"))
        Assert.True(tableParameterRowList.Item(0).TemplateFields.Item(1).Equals("column:cost:number"))
        Assert.True(tableParameterRowList.Item(0).TemplateFields.Item(2).Equals("column:voltage:string"))
    End Sub

End Class
