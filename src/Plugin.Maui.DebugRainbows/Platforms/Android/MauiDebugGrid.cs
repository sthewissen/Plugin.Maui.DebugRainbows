using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Util;
using AndroidX.Core.View;
using Plugin.Maui.DebugRainbows.Controls;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;
using Color = Microsoft.Maui.Graphics.Color;
using Paint = Android.Graphics.Paint;

namespace Plugin.Maui.DebugRainbows.Platforms.Android;

public class MauiDebugGrid : AView
{
    private int _screenWidth;
    private int _screenHeight;

    public float HorizontalItemSize { get; set; }
    public float VerticalItemSize { get; set; }
    public int MajorGridLineInterval { get; set; }
    public GridLineOptions MajorGridLines { get; set; }
    public GridLineOptions MinorGridLines { get; set; }
    public DebugGridOrigin GridOrigin { get; set; }

    public MauiDebugGrid(Context? context, DebugGrid debugGrid) : base(context)
    {
        HorizontalItemSize = (float)debugGrid.HorizontalItemSize;
        VerticalItemSize = (float)debugGrid.VerticalItemSize;
        MajorGridLines = debugGrid.MajorGridLines;
        MinorGridLines = debugGrid.MinorGridLines;
        GridOrigin = debugGrid.GridOrigin;
        MajorGridLineInterval = debugGrid.MajorGridLineInterval;

        Init();
    }

    public static float ConvertDpToPixel(float dp, Context context)
    {
        return dp * ((float)context.Resources?.DisplayMetrics?.DensityDpi / (int)DisplayMetricsDensity.Default);
    }

    public void Init()
    {
        GetScreenDimensions();
    }

    private void GetScreenDimensions()
    {
        if (OperatingSystem.IsAndroidVersionAtLeast(30))
        {
            var windowMetrics = (Context as Activity)?.WindowManager?.CurrentWindowMetrics;
            var insets = windowMetrics.WindowInsets.GetInsetsIgnoringVisibility(WindowInsetsCompat.Type.SystemBars());
            _screenWidth = windowMetrics.Bounds.Width() - insets.Left - insets.Right;
            _screenHeight = windowMetrics.Bounds.Height() - insets.Top - insets.Bottom;
        }
        else if (OperatingSystem.IsAndroidVersionAtLeast(21))
        {
            var displayMetrics = new DisplayMetrics();
            (Context as Activity)?.WindowManager?.DefaultDisplay?.GetMetrics(displayMetrics);
            _screenWidth = displayMetrics.WidthPixels;
            _screenHeight = displayMetrics.HeightPixels;
        }
    }

    protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
    {
        base.OnLayout(changed, left, top, right, bottom);
        GetScreenDimensions();
    }

    protected override void OnDraw(Canvas canvas)
    {
        base.OnDraw(canvas);

        var majorPaint = new Paint();
        var minorPaint = new Paint();

        var colors = new[] {
            Color.FromArgb("#f3855b").ToPlatform(),
            Color.FromArgb("#fbcf93").ToPlatform(),
            Color.FromArgb("#fbe960").ToPlatform(),
            Color.FromArgb("#a0e67a").ToPlatform(),
            Color.FromArgb("#33c6ee").ToPlatform(),
            Color.FromArgb("#c652ba").ToPlatform()
        };

        // Make these into true pixels from DP.
        HorizontalItemSize = ConvertDpToPixel(HorizontalItemSize, Context);
        VerticalItemSize = ConvertDpToPixel(VerticalItemSize, Context);
        MajorGridLines.Width = ConvertDpToPixel((float)MajorGridLines.Width, Context);
        MinorGridLines.Width = ConvertDpToPixel((float)MinorGridLines.Width, Context);

        //if (Inverse)
        //{
        //    DrawInverse(canvas, majorPaint, colors);
        //}
        //else
        //{
            //if (MakeGridRainbows)
            //{
            //    var a = canvas.Width * Math.Pow(Math.Sin(2 * Math.PI * ((90 + 0.75) / 2)), 2);
            //    var b = canvas.Height * Math.Pow(Math.Sin(2 * Math.PI * ((90 + 0.0) / 2)), 2);
            //    var c = canvas.Width * Math.Pow(Math.Sin(2 * Math.PI * ((90 + 0.25) / 2)), 2);
            //    var d = canvas.Height * Math.Pow(Math.Sin(2 * Math.PI * ((90 + 0.5) / 2)), 2);

            //    var locations = new float[] { 0, 0.2f, 0.4f, 0.6f, 0.8f, 1 };
            //    var shader = new LinearGradient(canvas.Width - (float)a, (float)b, canvas.Width - (float)c, (float)d, colors.Select(x => (int)x.ToArgb()).ToArray(), locations, Shader.TileMode.Clamp);

            //    minorPaint.SetShader(shader);
            //    majorPaint.SetShader(shader);
            //}

            DrawNormal(canvas, majorPaint, minorPaint);
        //}
    }

    private void DrawNormal(Canvas canvas, Paint majorPaint, Paint minorPaint)
    {
        majorPaint.StrokeWidth = (float)MajorGridLines.Width;
        majorPaint.Color = MajorGridLines.Color.ToPlatform();
        majorPaint.Alpha = (int)(255 * MajorGridLines.Opacity);

        minorPaint.StrokeWidth = (float)MinorGridLines.Width;
        minorPaint.Color = MinorGridLines.Color.ToPlatform();
        minorPaint.Alpha = (int)(255 * MinorGridLines.Opacity);

        switch (GridOrigin)
        {
            case DebugGridOrigin.TopLeft:
            {
                var verticalPosition = 0f;
                var i = 0;
            
                while (verticalPosition <= _screenHeight)
                {
                    canvas.DrawLine(0, verticalPosition, _screenWidth, verticalPosition, MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? majorPaint : minorPaint);
                    verticalPosition += VerticalItemSize;
                    i++;
                }

                float horizontalPosition = 0;
                i = 0;
                while (horizontalPosition <= _screenWidth)
                {
                    canvas.DrawLine(horizontalPosition, 0, horizontalPosition, _screenHeight, MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? majorPaint : minorPaint);
                    horizontalPosition += HorizontalItemSize;
                    i++;
                }

                break;
            }
            case DebugGridOrigin.Center:
            {
                var gridLinesHorizontalCenter = _screenWidth / 2;
                var gridLinesVerticalCenter = _screenHeight / 2;
                var amountOfVerticalLines = _screenWidth / HorizontalItemSize;
                var amountOfHorizontalLines = _screenHeight / VerticalItemSize;

                // Draw the horizontal lines.
                for (var i = 0; i < (amountOfHorizontalLines / 2); i++)
                {
                    canvas.DrawLine(
                        startX: 0,
                        startY: gridLinesVerticalCenter + (i * VerticalItemSize),
                        stopX: _screenWidth,
                        stopY: gridLinesVerticalCenter + (i * VerticalItemSize),
                        paint: MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? majorPaint : minorPaint
                    );

                    canvas.DrawLine(
                        startX: 0,
                        startY: gridLinesVerticalCenter - (i * VerticalItemSize),
                        stopX: _screenWidth,
                        stopY: gridLinesVerticalCenter - (i * VerticalItemSize),
                        paint: MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? majorPaint : minorPaint
                    );
                }

                // Draw vertical lines.
                for (var i = 0; i < (amountOfVerticalLines / 2); i++)
                {
                    canvas.DrawLine(
                        startX: gridLinesHorizontalCenter + (i * HorizontalItemSize),
                        startY: 0,
                        stopX: gridLinesHorizontalCenter + (i * HorizontalItemSize),
                        stopY: _screenHeight,
                        paint: MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? majorPaint : minorPaint
                    );

                    canvas.DrawLine(
                        startX: gridLinesHorizontalCenter - (i * HorizontalItemSize),
                        startY: 0,
                        stopX: gridLinesHorizontalCenter - (i * HorizontalItemSize),
                        stopY: _screenHeight,
                        paint: MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? majorPaint : minorPaint
                    );
                }

                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    //private void DrawInverse(Canvas canvas, Paint majorPaint, global::Android.Graphics.Color[] colors)
    //{
    //    majorPaint.StrokeWidth = 0;
    //    majorPaint.Color = GridLineColor.ToAndroid();
    //    majorPaint.Alpha = (int)(255 * GridLineOpacity);

    //    if (GridOrigin == DebugGridOrigin.TopLeft)
    //    {
    //        var horizontalTotal = 0;
    //        for (int i = 1; horizontalTotal < screenWidth; i++)
    //        {
    //            var verticalTotal = 0;
    //            var horizontalSpacerSize = MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? MajorGridLineThickness : GridLineThickness;

    //            for (int j = 1; verticalTotal < screenHeight; j++)
    //            {
    //                var verticalSpacerSize = MajorGridLineInterval > 0 && j % MajorGridLineInterval == 0 ? MajorGridLineThickness : GridLineThickness;

    //                var rectangle = new Rect(
    //                    (int)horizontalTotal,
    //                    (int)verticalTotal,
    //                    (int)(horizontalTotal + HorizontalItemSize),
    //                    (int)(verticalTotal + VerticalItemSize)
    //                );

    //                if (MakeGridRainbows)
    //                {
    //                    var color = colors[(i + j) % colors.Length];
    //                    majorPaint.Color = color;
    //                }

    //                canvas.DrawRect(rectangle, majorPaint);

    //                verticalTotal += (int)(VerticalItemSize + verticalSpacerSize);
    //            }

    //            horizontalTotal += (int)(HorizontalItemSize + horizontalSpacerSize);
    //        }
    //    }
    //    else if (GridOrigin == DebugGridOrigin.Center)
    //    {
    //        var horizontalRightTotal = (screenWidth / 2) + (int)((MajorGridLineInterval > 0 ? MajorGridLineThickness : GridLineThickness) / 2);
    //        var horizontalLeftTotal = (screenWidth / 2) - (int)(HorizontalItemSize + ((MajorGridLineInterval > 0 ? MajorGridLineThickness : GridLineThickness) / 2));

    //        for (int i = 1; horizontalRightTotal < screenWidth; i++)
    //        {
    //            var horizontalSpacerSize = MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? MajorGridLineThickness : GridLineThickness;
    //            var verticalBottomTotal = (screenHeight / 2) + (int)((MajorGridLineInterval > 0 ? MajorGridLineThickness : GridLineThickness) / 2);
    //            var verticalTopTotal = (screenHeight / 2) - (int)(VerticalItemSize + ((MajorGridLineInterval > 0 ? MajorGridLineThickness : GridLineThickness) / 2));

    //            for (int j = 1; verticalBottomTotal < screenHeight; j++)
    //            {
    //                if (MakeGridRainbows)
    //                {
    //                    var color = colors[(i + j) % colors.Length];
    //                    majorPaint.Color = color;
    //                }

    //                var verticalSpacerSize = MajorGridLineInterval > 0 && j % MajorGridLineInterval == 0 ? MajorGridLineThickness : GridLineThickness;

    //                var rectangle = new Rect(horizontalRightTotal, verticalBottomTotal, (int)(horizontalRightTotal + HorizontalItemSize), (int)(verticalBottomTotal + VerticalItemSize));
    //                canvas.DrawRect(rectangle, majorPaint);

    //                var rectangle2 = new Rect(horizontalLeftTotal, verticalTopTotal, (int)(horizontalLeftTotal + HorizontalItemSize), (int)(verticalTopTotal + VerticalItemSize));
    //                canvas.DrawRect(rectangle2, majorPaint);

    //                var rectangle3 = new Rect(horizontalRightTotal, verticalTopTotal, (int)(horizontalRightTotal + HorizontalItemSize), (int)(verticalTopTotal + VerticalItemSize));
    //                canvas.DrawRect(rectangle3, majorPaint);

    //                var rectangle4 = new Rect(horizontalLeftTotal, verticalBottomTotal, (int)(horizontalLeftTotal + HorizontalItemSize), (int)(verticalBottomTotal + VerticalItemSize));
    //                canvas.DrawRect(rectangle4, majorPaint);

    //                verticalTopTotal -= (int)(VerticalItemSize + verticalSpacerSize);
    //                verticalBottomTotal += (int)(VerticalItemSize + verticalSpacerSize);
    //            }

    //            horizontalRightTotal += (int)(HorizontalItemSize + horizontalSpacerSize);
    //            horizontalLeftTotal -= (int)(HorizontalItemSize + horizontalSpacerSize);
    //        }
    //    }
    //}

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
