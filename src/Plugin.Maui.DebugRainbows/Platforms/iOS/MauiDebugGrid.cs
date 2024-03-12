using CoreAnimation;
using CoreGraphics;
using Plugin.Maui.DebugRainbows.Controls;
using Plugin.Maui.DebugRainbows.Models;
using Microsoft.Maui.Platform;
using UIKit;

namespace Plugin.Maui.DebugRainbows.Platforms.MaciOS;

public class MauiDebugGrid : UIView
{
    readonly DebugGrid _debugGrid;

    readonly CALayer _gridLayer = new CAShapeLayer();
    readonly CALayer _majorGridLayer = new CAShapeLayer();

    readonly CGColor[] rainbowColors = {
        Color.FromArgb("#f3855b").ToCGColor(),
        Color.FromArgb("#fbcf93").ToCGColor(),
        Color.FromArgb("#fbe960").ToCGColor(),
        Color.FromArgb("#a0e67a").ToCGColor(),
        Color.FromArgb("#33c6ee").ToCGColor(),
        Color.FromArgb("#c652ba").ToCGColor(),
        Color.FromArgb("#ef53b2").ToCGColor()
    };

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

        BackgroundColor = UIColor.Clear;
        ContentMode = UIViewContentMode.Redraw;
    }

    void DrawGrid(CGRect rect)
    {
        //if (Inverse)
        //{
        //    DrawInverseGridLayer(rect);
        //}
        //else
        //{
        DrawNormalGridLayer(_gridLayer, false);
        DrawNormalGridLayer(_majorGridLayer, true);
        //}
    }

    private void DrawNormalGridLayer(CALayer layer, bool isMajor)
    {
        if (isMajor && MajorGridLineInterval == 0)
            return;

        using (var path = CreatePath(isMajor ? MajorGridLineInterval : 0))
        {
            layer = new CAShapeLayer
            {
                LineWidth = isMajor ? (nfloat)MajorGridLines.Width : (nfloat)MinorGridLines.Width,
                Path = path.CGPath,
                StrokeColor = isMajor ? MajorGridLines.Color.ToCGColor() : MinorGridLines.Color.ToCGColor(),
                Opacity = isMajor ? (float)MajorGridLines.Opacity : (float)MinorGridLines.Opacity,
                Frame = new CGRect(0, 0, Bounds.Size.Width, Bounds.Size.Height)
            };

            //if (!MakeGridRainbows)
            //{
            this.Layer.AddSublayer(layer);
            //}
            //else
            //{
            //    var gradientLayer = new CAGradientLayer
            //    {
            //        StartPoint = new CGPoint(0.5, 0.0),
            //        EndPoint = new CGPoint(0.5, 1.0),
            //        Frame = new CGRect(0, 0, Bounds.Size.Width, Bounds.Size.Height),
            //        Colors = rainbowColors,
            //        Mask = layer
            //    };

            //    this.Layer.AddSublayer(gradientLayer);
            //}
        }
    }
    private UIBezierPath CreatePath(int interval = 0)
    {
        var path = new UIBezierPath();
        var gridLinesHorizontal = Bounds.Width / HorizontalItemSize;
        var gridLinesVertical = Bounds.Height / VerticalItemSize;

        if (GridOrigin == DebugGridOrigin.TopLeft)
        {
            for (int i = 0; i < gridLinesHorizontal; i++)
            {
                if (interval == 0 || i % interval == 0)
                {
                    var start = new CGPoint(x: (nfloat)i * HorizontalItemSize, y: 0);
                    var end = new CGPoint(x: (nfloat)i * HorizontalItemSize, y: Bounds.Height);
                    path.MoveTo(start);
                    path.AddLineTo(end);
                }
            }

            for (int i = 0; i < gridLinesVertical; i++)
            {
                if (interval == 0 || i % interval == 0)
                {
                    var start = new CGPoint(x: 0, y: (nfloat)i * VerticalItemSize);
                    var end = new CGPoint(x: Bounds.Width, y: (nfloat)i * VerticalItemSize);
                    path.MoveTo(start);
                    path.AddLineTo(end);
                }
            }

            path.ClosePath();
        }
        else if (GridOrigin == DebugGridOrigin.Center)
        {
            var gridLinesHorizontalCenter = Bounds.Width / 2;
            var gridLinesVerticalCenter = Bounds.Height / 2;

            for (int i = 0; i < (gridLinesHorizontal / 2); i++)
            {
                if (interval == 0 || i % interval == 0)
                {
                    var startRight = new CGPoint(x: gridLinesHorizontalCenter + ((nfloat)i * HorizontalItemSize), y: 0);
                    var endRight = new CGPoint(x: gridLinesHorizontalCenter + ((nfloat)i * HorizontalItemSize), y: Bounds.Height);
                    path.MoveTo(startRight);
                    path.AddLineTo(endRight);

                    var startLeft = new CGPoint(x: gridLinesHorizontalCenter - ((nfloat)i * HorizontalItemSize), y: 0);
                    var endLeft = new CGPoint(x: gridLinesHorizontalCenter - ((nfloat)i * HorizontalItemSize), y: Bounds.Height);
                    path.MoveTo(startLeft);
                    path.AddLineTo(endLeft);
                }
            }

            for (int i = 0; i < (gridLinesVertical / 2); i++)
            {
                if (interval == 0 || i % interval == 0)
                {
                    var startBottom = new CGPoint(x: 0, y: gridLinesVerticalCenter + ((nfloat)i * VerticalItemSize));
                    var endBottom = new CGPoint(x: Bounds.Width, y: gridLinesVerticalCenter + ((nfloat)i * VerticalItemSize));
                    path.MoveTo(startBottom);
                    path.AddLineTo(endBottom);

                    var startTop = new CGPoint(x: 0, y: gridLinesVerticalCenter - ((nfloat)i * VerticalItemSize));
                    var endTop = new CGPoint(x: Bounds.Width, y: gridLinesVerticalCenter - ((nfloat)i * VerticalItemSize));
                    path.MoveTo(startTop);
                    path.AddLineTo(endTop);
                }
            }
        }

        return path;
    }

    public override void Draw(CGRect rect)
    {
        DrawGrid(rect);
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
