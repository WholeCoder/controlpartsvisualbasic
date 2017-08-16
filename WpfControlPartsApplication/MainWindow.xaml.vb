Class MainWindow

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Dim input As String
        input = My.Computer.FileSystem.ReadAllText("../../a185.htm")

        Me.TextBox1.Text = input
        '        Me.TextBox1.ScrollBars = ScrollBars.Vertical

        Dim lbl As New TextBox
        lbl.Text = "New TextBox@@@"
        Me.grd.Children.Add(lbl)
    End Sub
End Class
