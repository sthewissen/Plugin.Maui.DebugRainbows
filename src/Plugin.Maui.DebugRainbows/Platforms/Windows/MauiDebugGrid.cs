using Plugin.Maui.DebugRainbows.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Composition;
using Microsoft.Maui.Platform;
using WinColor = Windows.UI;
using Microsoft.Maui.Graphics;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI.Xaml;
using System.Runtime.InteropServices;

namespace Plugin.Maui.DebugRainbows.Platforms.Windows;

public class MauiDebugGrid : Microsoft.UI.Xaml.Controls.Grid
{
    readonly DebugGrid _debugGrid;
    readonly ContainerVisual _root;
    readonly Canvas _canvas;

    public double HorizontalItemSize { get; set; }
    public double VerticalItemSize { get; set; }
    public int MajorGridLineInterval { get; set; }

    public GridLineOptions MajorGridLines { get; set; }
    public GridLineOptions MinorGridLines { get; set; }
    public DebugGridOrigin GridOrigin { get; set; }

    public MauiDebugGrid(DebugGrid debugGrid)
    {
        _debugGrid = debugGrid;

        HorizontalItemSize = _debugGrid.HorizontalItemSize;
        VerticalItemSize = _debugGrid.VerticalItemSize;
        MajorGridLines = _debugGrid.MajorGridLines;
        MinorGridLines = _debugGrid.MinorGridLines;
        GridOrigin = _debugGrid.GridOrigin;
        MajorGridLineInterval = _debugGrid.MajorGridLineInterval;

        
        _canvas = new Canvas();
        this.Children.Add(_canvas);
        
        // If the size of the window changes, we need to adapt.
        this.SizeChanged += MauiDebugGrid_SizeChanged;
    }

    /// <summary>
    /// Handles changing the size of the window and redrawing the grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MauiDebugGrid_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        DrawGrid();
    }

    /// <summary>
    /// Draws the grid on the screen.
    /// </summary>
    void DrawGrid()
    {
        _canvas.Children.Clear();

        var actualWidth = ActualWidth;
        var actualHeight = ActualHeight;

        var horizontalGridLines = (int)(actualWidth / HorizontalItemSize) + 5;
        var verticalGridLines = (int)(actualHeight / VerticalItemSize) + 5;

        if (GridOrigin == DebugGridOrigin.TopLeft)
        {
            for (int i = 0; i < horizontalGridLines; i++)
            {
                if (MajorGridLineInterval == 0 || i % MajorGridLineInterval == 0)
                {
                    DrawLine(i * HorizontalItemSize, 0, i * HorizontalItemSize, actualHeight, MajorGridLines);
                }
                else
                {
                    DrawLine(i * HorizontalItemSize, 0, i * HorizontalItemSize, actualHeight, MinorGridLines);
                }
            }

            for (int i = 0; i < verticalGridLines; i++)
            {
                if (MajorGridLineInterval == 0 || i % MajorGridLineInterval == 0)
                {
                    DrawLine(0, i * VerticalItemSize, actualWidth, i * VerticalItemSize, MajorGridLines);
                }
                else
                {
                    DrawLine(0, i * VerticalItemSize, actualWidth, i * VerticalItemSize, MinorGridLines);
                }
            }
        }
        else if (GridOrigin == DebugGridOrigin.Center)
        {
            var gridLinesHorizontalCenter = actualWidth / 2;
            var gridLinesVerticalCenter = actualHeight / 2;

            for (int i = 0; i < (horizontalGridLines / 2); i++)
            {
                if (MajorGridLineInterval == 0 || i % MajorGridLineInterval == 0)
                {
                    DrawLine(gridLinesHorizontalCenter + (i * HorizontalItemSize), 0, gridLinesHorizontalCenter + (i * HorizontalItemSize), actualHeight, MajorGridLines);
                    DrawLine(gridLinesHorizontalCenter - (i * HorizontalItemSize), 0, gridLinesHorizontalCenter - (i * HorizontalItemSize), actualHeight, MajorGridLines);
                }
                else
                {

                    DrawLine(gridLinesHorizontalCenter + (i * HorizontalItemSize), 0, gridLinesHorizontalCenter + (i * HorizontalItemSize), actualHeight, MinorGridLines);
                    DrawLine(gridLinesHorizontalCenter - (i * HorizontalItemSize), 0, gridLinesHorizontalCenter - (i * HorizontalItemSize), actualHeight, MinorGridLines);
                }
            }

            for (int i = 0; i < (verticalGridLines / 2); i++)
            {
                if (MajorGridLineInterval == 0 || i % MajorGridLineInterval == 0)
                {
                    DrawLine(0, gridLinesVerticalCenter + (i * VerticalItemSize), actualWidth, gridLinesVerticalCenter + (i * VerticalItemSize), MajorGridLines);
                    DrawLine(0, gridLinesVerticalCenter - (i * VerticalItemSize), actualWidth, gridLinesVerticalCenter - (i * VerticalItemSize), MajorGridLines);
                }
                else
                {

                    DrawLine(0, gridLinesVerticalCenter + (i * VerticalItemSize), actualWidth, gridLinesVerticalCenter + (i * VerticalItemSize), MinorGridLines);
                    DrawLine(0, gridLinesVerticalCenter - (i * VerticalItemSize), actualWidth, gridLinesVerticalCenter - (i * VerticalItemSize), MinorGridLines);
                }
            }
        }
    }

    void DrawLine(double x1, double y1, double x2, double y2, GridLineOptions lineOptions)
    {
        var line = new Line()
        {
            X1 = x1,
            Y1 = y1,
            X2 = x2,
            Y2 = y2,
            Stroke = lineOptions.Color.ToPlatform(),
            Opacity = lineOptions.Opacity,
            StrokeThickness = lineOptions.Width
        };

        _canvas.Children.Add(line);
    }

    internal void UpdateMajorGridLineInterval()
    {

    }

    internal void UpdateGridOrigin()
    {

    }

    internal void UpdateMajorGridLines()
    {

    }

    internal void UpdateMinorGridLines()
    {

    }

    internal void UpdateHorizontalItemSize()
    {

    }

    internal void UpdateVerticalItemSize()
    {

    }
}
