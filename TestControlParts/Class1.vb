Imports System.CodeDom.Compiler
Imports WindowsApplication1
Imports NUnit.Framework

<TestFixture()>
Public Class Class1

    Dim fileReader As String

    <SetUp()>
    Protected Sub SetUp()
        fileReader = My.Computer.FileSystem.ReadAllText("C:\Users\rpierich\Documents\Visual Studio 2013\Projects\controlpartsvisualbasic\WindowsApplication1\a185.htm.template")
    End Sub

    <Test()>
    Public Sub EmptyListReturned()
        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.GetListOfKeywords("")

        Assert.IsTrue(getListOfKeywordskeywordList.Count = 0)
    End Sub

    <Test()>
    Public Sub StripOutFields()
        Dim inputTemplate = "<!DOCTYPE html><html><head> <title>|field:title|</title></head><body> <h1>|field:header|</h1> <table> <tr> <th>Ruben Found This</th> <th></th> <th></th> </tr> <tr> <td></td> <td></td> <td></td> </tr> <tr> <td></td> <td></td> <td></td> </tr> </table></body></html>"
        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.GetListOfKeywords(inputTemplate)

        Assert.IsTrue(getListOfKeywordskeywordList.Count = 2)
    End Sub

    <Test()>
    Public Sub EnsureCorrectFields()
        Dim inputTemplate = "<!DOCTYPE html><html><head> <title>|field:title|</title></head><body> <h1>|field:header|</h1> <table> <tr> <th>Ruben Found This</th> <th></th> <th></th> </tr> <tr> <td></td> <td></td> <td></td> </tr> <tr> <td></td> <td></td> <td></td> </tr> </table></body></html>"
        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.GetListOfKeywords(inputTemplate)

        Assert.IsTrue(getListOfKeywordskeywordList.ContainsKey("field:title"))
        Assert.IsTrue(getListOfKeywordskeywordList.ContainsKey("field:header"))
    End Sub

    <Test()>
    Public Sub EnsureCorrectTableIsExtracted()
        Dim inputTemplate = "<!DOCTYPE html><html><head> <title>|field:title|</title></head><body> <h1>|field:header|</h1> |table:rubenstable|b|</body></html>"
        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.GetListOfKeywords(inputTemplate)

        Assert.IsTrue(getListOfKeywordskeywordList.ContainsKey("table:rubenstable"))
    End Sub
End Class
