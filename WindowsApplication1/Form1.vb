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

        Dim tableOptions = ""
        Dim str = ""
        Dim tableidJustfound = False
        Dim foundReplacementIdentifier = False
        Dim list As New ArrayList
        For Each c As Char In fileReader
            If c = "|" And Not foundReplacementIdentifier Then
                foundReplacementIdentifier = True
            ElseIf foundReplacementIdentifier And c <> "|" Then
                str = str & c
            ElseIf c = "|" Then
                foundReplacementIdentifier = False
                If str.ToString() = "table" Then

                    tableidJustfound = True
                ElseIf tableidJustfound Then
                    tableOptions = tableOptions & str.ToString()
                    tableidJustfound = False
                End If
                list.Add(str.ToString())
                str = ""
                tableOptions = ""
            End If

        Next

        Me.AddButton("Ruben's String")
        Me.TextBox1.Text = tableOptions
        
    End Sub
End Class
