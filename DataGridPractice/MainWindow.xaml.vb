Class MainWindow

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Dim dGrid As New DataGrid
        dGrid.AutoGenerateColumns = False
        '        dGrid.Width = 400

        '        dGrid.Columns.Add(
        '            New DataGridTextColumn())
        '
        '        dGrid.Items.Add("John")
        '        dGrid.Items.Add("Jill")

        Dim col1 As DataGridTextColumn =
                New DataGridTextColumn()
        col1.Width = 200
        col1.Binding = New Binding("Name")
        col1.Header = "Name"

        Dim col2 As DataGridTextColumn =
                New DataGridTextColumn()
        col2.Width = 200
        col2.Binding = New Binding("Age")
        col2.Header = "Age"

        dGrid.Columns.Add(col1)
        dGrid.Columns.Add(col2)

        Dim dta1 As New MyData() With {.Name = "John", .Age = "25"}
        Dim dta2 As New MyData() With {.Name = "Jill", .Age = "29"}

        Dim dataList As List(Of MyData) = New List(Of MyData)
        dataList.Add(dta1)
        dataList.Add(dta2)

        dGrid.ItemsSource = dataList

        '        dGrid.Items.Add(
        '            dta1)
        '        dGrid.Items.Add(
        '            dta2)


        '        Dim dataGridColumn As DataGridTextColumn = dGrid.Columns(0)
        '        dataGridColumn.Binding = New Binding(".")
        '
        cnvs.Children.Add(dGrid)
        Canvas.SetTop(dGrid, 100.0)
        Canvas.SetLeft(dGrid, 100.0)

    End Sub
End Class
