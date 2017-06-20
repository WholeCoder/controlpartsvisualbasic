Public Class Form1
    Private Function AddButton(name As String, x As Int64, y As Int64)
        Dim newTB2 As New TextBox
        newTB2.Name = name
        'Set location, size and so on if you like
        newTB2.Location = New Point(x, y)
        Me.Controls.Add(newTB2)
        newTB2.Text = name
        Return newTB2.Height
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.TextBox1.Text = "Ruben is awesome!"
        Me.TextBox1.ScrollBars = ScrollBars.Vertical
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText("C:\Users\rpierich\Documents\Visual Studio 2013\Projects\controlpartsvisualbasic\WindowsApplication1\a185.htm.template")

        Dim inputTemplate = "<!DOCTYPE html><html><head> <title>|table:title|b|</title></head><body> <h1>|table:headertable|b|</h1> |table:rubenstable|</body></html>"
        Dim getListOfKeywordskeywordList As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(inputTemplate)

        Dim str = ""
        Dim y = 100
        For Each de As DictionaryEntry In getListOfKeywordskeywordList
            Dim height = Me.AddButton(de.Key, 30, y)
            '            Me.TextBox1.Text = de.Value
            y += height
        Next de



    End Sub


End Class
