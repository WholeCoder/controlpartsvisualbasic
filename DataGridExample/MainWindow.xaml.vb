Class MainWindow
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim dGrid As New DataGrid
        dGrid.Width = 400

        dGrid.Columns.Add(
            New DataGridTextColumn())

        dGrid.Items.Add("John")
        dGrid.Items.Add("Jill")

        Dim dataGridColumn As DataGridTextColumn = dGrid.Columns(0)
        dataGridColumn.Binding = New Binding(".")

        cnvs.Children.Add(dGrid)
        Canvas.SetTop(dGrid, 100.0)
        Canvas.SetLeft(dGrid, 100.0)
    End Sub
End Class
