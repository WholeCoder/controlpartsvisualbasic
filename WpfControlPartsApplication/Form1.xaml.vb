Imports WpfControlPartsApplication.WpfControlPartsApplication


Public Class Form1
    Public myObjectSavers As List(Of SaveOrLoadFromToDatabaseObject)
    Public myHTMLObjectSavers As List(Of SaveOrLoadFromToDatabaseObject)

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        For Each oSaver As SaveOrLoadFromToDatabaseObject In myObjectSavers
            oSaver.SaveToDatabase()
        Next
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        For Each oSaver As SaveOrLoadFromToDatabaseObject In myObjectSavers
            oSaver.LoadFromDatabase()
        Next

        Dim htmlString As String = ""

        For Each oSaver As SaveOrLoadFromToDatabaseObject In myHTMLObjectSavers
            htmlString &= oSaver.ConvertToHTMLDocument() & "                   "
        Next
        My.Windows.ViewHTMLWindow.HTMLTextBox.Text = htmlString
        My.Windows.ViewHTMLWindow.Show()
    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        Me.Load_Tables_From_Datase()
    End Sub

    Public Sub Load_Tables_From_Datase()
        For Each oSaver As SaveOrLoadFromToDatabaseObject In myObjectSavers
            oSaver.LoadFromDatabase()
        Next
    End Sub
End Class


