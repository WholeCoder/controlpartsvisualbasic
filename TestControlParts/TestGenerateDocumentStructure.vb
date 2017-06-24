Imports System.CodeDom.Compiler
Imports WindowsApplication1
Imports NUnit.Framework

Public Class TestGenerateDocumentStructure
    <Test()>
    Public Sub SeeIfTableArgumentsCanBeParsedOutIntoObjects()
        Dim template As String = "<!DOCTYPE html><html><head><title>Test Template</title></head><body><table>|table:rubenstable|<tr><td colspan=""3"">Test 3 Header</td></tr>*<tr><td>col</td><td>col 2</td><td>col3</td></tr>*<tr><td>%column:rubenstale%</td><td>%column:ruthstale%</td><td></td></tr>|</table></body></html>"
        Dim templateHash As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(template, "|", "*")
        Dim documentStructureList As List(Of String) = templateHash.Item("documentstructure")

        Assert(documentStructureList.Count = 3)
        '        Assert.True(documentStructureList(0).Equals("<!DOCTYPE html><html><head><title>Test Template</title></head><body><table>"))
        '        Assert.True(documentStructureList(1).Equals("table:rubenstable"))
        '        Assert.True(documentStructureList(2).Equals("</table></body></html>"))
    End Sub
End Class
