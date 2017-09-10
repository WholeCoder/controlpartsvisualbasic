Imports System.Windows.Media.Animation
Imports System.Windows.Media.Converters

Class MainWindow
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim da As DoubleAnimation = New DoubleAnimation()
        da.From = 30
        da.To = 100
        da.Duration = New Duration(TimeSpan.FromSeconds(1))
        da.AutoReverse = True
        da.RepeatBehavior = RepeatBehavior.Forever
        sender.BeginAnimation(Button.HeightProperty, da)
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Dim da As DoubleAnimation = New DoubleAnimation()
        da.From = 0
        da.To = 360
        da.Duration = New Duration(TimeSpan.FromSeconds(3))
        da.RepeatBehavior = RepeatBehavior.Forever
        Dim rt As RotateTransform = New RotateTransform()
        sender.RenderTransformOrigin = New Point(0.5, 0.5)
        sender.RenderTransform = rt
        rt.BeginAnimation(RotateTransform.AngleProperty, da)
    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        Dim da As DoubleAnimationUsingPath = New DoubleAnimationUsingPath()
        Dim myPathFigure As New PathFigure()
        myPathFigure.StartPoint = New Point(10, 50)

        Dim myLineSegment As New LineSegment()
        myLineSegment.Point = New Point(200, 70)

        Dim myPathSegmentCollection As New PathSegmentCollection()
        myPathSegmentCollection.Add(myLineSegment)

        myPathFigure.Segments = myPathSegmentCollection

        Dim myPathFigureCollection As New PathFigureCollection()
        myPathFigureCollection.Add(myPathFigure)

        Dim myPathGeometry As New PathGeometry()
        myPathGeometry.Figures = myPathFigureCollection

        da.PathGeometry = myPathGeometry
        da.Duration = New Duration(TimeSpan.FromSeconds(1))
        da.AutoReverse = True
        da.RepeatBehavior = RepeatBehavior.Forever
        sender.BeginAnimation(Button.WidthProperty, da)

    End Sub
End Class
