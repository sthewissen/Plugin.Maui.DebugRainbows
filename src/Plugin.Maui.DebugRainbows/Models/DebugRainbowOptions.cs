namespace Plugin.Maui.DebugRainbows;

public class DebugRainbowsOptions
{
    public bool ShowRainbows { get; set; }
    public bool ShowGrid { get; set; }
    public double HorizontalItemSize { get; set; } = 20;
    public double VerticalItemSize { get; set; } = 20;
    public int MajorGridLineInterval { get; set; } = 4;
    public GridLineOptions MajorGridLines { get; set; } = new GridLineOptions { Color = Color.FromRgb(255, 0, 0), Opacity = 1, Width = 4 };
    public GridLineOptions MinorGridLines { get; set; } = new GridLineOptions { Color = Color.FromRgb(255, 0, 0), Opacity = 1, Width = 1 };
    public DebugGridOrigin GridOrigin { get; set; }
}