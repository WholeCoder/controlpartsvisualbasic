Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.TextBox1.Text = "Ruben is awesome!"
        Me.TextBox1.ScrollBars = ScrollBars.Vertical
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText("C:\Users\rpierich\Documents\visual studio 2013\Projects\controlpartsvisualbasic\WindowsApplication1\at11-limit-switches.htm.template")

        Dim str = ""
        Dim foundReplacementIdentifier = False
        Dim list As New ArrayList
        For Each c As Char In fileReader
            If c = "|" And Not foundReplacementIdentifier Then
                foundReplacementIdentifier = True
            ElseIf foundReplacementIdentifier And c <> "|" Then
                str = str & c
            Else
                foundReplacementIdentifier = False
                list.Add(str)
                str = ""
            End If

        Next

        Dim output = ""
        For Each ch As String In list
            output = output & ch
        Next

        Me.TextBox1.Text = output

    End Sub
End Class
