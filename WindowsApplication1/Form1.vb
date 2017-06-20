Public Class Form1
    Private Sub AddButton(name As String)
        Dim newTB2 As New TextBox
        newTB2.Name = name
        'Set location, size and so on if you like
        newTB2.Location = New Point(30, 30)
        Me.Controls.Add(newTB2)
        newTB2.Text = name & " Text"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.TextBox1.Text = "Ruben is awesome!"
        Me.TextBox1.ScrollBars = ScrollBars.Vertical
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText("C:\Users\rpierich\Documents\Visual Studio 2013\Projects\controlpartsvisualbasic\WindowsApplication1\a185.htm.template")

        Dim inputTemplate = "<!DOCTYPE html><html><head> <title>|table:title|b|</title></head><body> <h1>|table2:header|b|</h1> |table:rubenstable|</body></html>"
        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.GetListOfKeywords(inputTemplate)

        Dim str = ""
        For Each de As DictionaryEntry In getListOfKeywordskeywordList
            str = str & de.Key
        Next de

        Me.AddButton("Ruben's String")
        Me.TextBox1.Text = str


    End Sub


End Class
