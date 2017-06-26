Imports System.CodeDom.Compiler
Imports WindowsApplication1
Imports NUnit.Framework

<TestFixture()>
Public Class TestParser

    Dim fileReader As String

    <SetUp()>
    Protected Sub SetUp()
        fileReader = My.Computer.FileSystem.ReadAllText("C:\Users\rpierich\Documents\Visual Studio 2013\Projects\controlpartsvisualbasic\WindowsApplication1\a185.htm.template")
    End Sub

    <Test()>
    Public Sub EmptyListReturned()
        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements("", "|", "*")

        Assert.IsTrue(getListOfKeywordskeywordList.Count = 1)
    End Sub

    <Test()>
    Public Sub StripOutFields()
        Dim inputTemplate = "<!DOCTYPE html><html><head> <title>|field:title|</title></head><body> <h1>|field:header|</h1> <table> <tr> <th>Ruben Found This</th> <th></th> <th></th> </tr> <tr> <td></td> <td></td> <td></td> </tr> <tr> <td></td> <td></td> <td></td> </tr> </table></body></html>"
        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(inputTemplate, "|", "*")

        Assert.IsTrue(getListOfKeywordskeywordList.Count = 3)
    End Sub

    <Test()>
    Public Sub EnsureCorrectFields()
        Dim inputTemplate = "<!DOCTYPE html><html><head> <title>|field:title|</title></head><body> <h1>|field:header|</h1> <table> <tr> <th>Ruben Found This</th> <th></th> <th></th> </tr> <tr> <td></td> <td></td> <td></td> </tr> <tr> <td></td> <td></td> <td></td> </tr> </table></body></html>"
        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(inputTemplate, "|", "*")

        Assert.IsTrue(getListOfKeywordskeywordList.ContainsKey("field:title:string"))
        Assert.IsTrue(getListOfKeywordskeywordList.ContainsKey("field:header:string"))
    End Sub

    <Test()>
    Public Sub EnsureCorrectTableIsExtracted()
        Dim inputTemplate As String
        inputTemplate = My.Computer.FileSystem.ReadAllText("C:\Users\rpierich\Documents\Visual Studio 2013\Projects\controlpartsvisualbasic\TestControlParts\TestTemplates\EnsureCorrectTableIsExtracted.template.html")

        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(inputTemplate, "|", "*")

        Assert.IsTrue(getListOfKeywordskeywordList.ContainsKey("table:rubenstable"))
    End Sub

    <Test()>
    Public Sub ParseTableWithItsCongigurationOptionsHeaders()
        Dim inputTemplate As String
        inputTemplate = "<!DOCTYPE html><html><head> <title>|field:title|</title></head><body> <h1>|field:header|</h1> |table:rubenstable|<tr><td colspan=""3"">Test 3 Header</td></tr>*<tr> <td>col</td> <td>col 2</td><td>col3</td></tr>|</body></html>"
        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(inputTemplate, "|", "*")
        Dim listOfTableColumns As List(Of TableRow) = getListOfKeywordskeywordList.Item("table:rubenstable")

        Assert.IsTrue(listOfTableColumns.Count = 2)
        Assert.IsTrue(listOfTableColumns(0).TemplateText.Equals("<tr><td colspan=""3"">Test 3 Header</td></tr>"))
        Assert.IsTrue(listOfTableColumns(1).TemplateText.Equals("<tr> <td>col</td> <td>col 2</td><td>col3</td></tr>"))
    End Sub

    <Test()>
    Public Sub TransformEmptyTableArgumentsIntoEmptyTable()
        Dim emptyTranformed As String = TemplateParserUtilitiy.ConvertTableLanguageToHtmlTable("", "*")

        Assert.IsTrue(emptyTranformed.Equals("<table></table>"))
    End Sub

    <Test()>
    Public Sub TransformTableWithHeaderToHtmlyTableArgumentsIntoEmptyTable()

        Dim extractedTableTemplateOptions As String
        extractedTableTemplateOptions = My.Computer.FileSystem.ReadAllText("C:\Users\rpierich\Documents\Visual Studio 2013\Projects\controlpartsvisualbasic\TestControlParts\TestTemplates\ParsTableWithItsCongigurationOptionsHeaders.template.html")


        Dim tableWithheaders As String = TemplateParserUtilitiy.ConvertTableLanguageToHtmlTable(extractedTableTemplateOptions, "*")

        Assert.IsTrue(tableWithheaders.Equals("<table>" & extractedTableTemplateOptions & "</table>"))
    End Sub

 End Class
