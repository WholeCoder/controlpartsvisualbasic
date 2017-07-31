Public Class Form2

    Public myObjectSavers As List(Of SaveOrLoadFromToDatabaseObject)

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub AboutToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        My.Forms.AboutBox1.Show()
        '        MsgBox("Copyright 2017", , "About The Lorax")
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub HelpGuideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpGuideToolStripMenuItem.Click
        My.Forms.Form3.Show()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For Each oSaver As SaveOrLoadFromToDatabaseObject In myObjectSavers
            oSaver.SaveToDatabase()
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For Each oSaver As SaveOrLoadFromToDatabaseObject In myObjectSavers
            oSaver.LoadFromDatabase()
        Next
    End Sub
End Class