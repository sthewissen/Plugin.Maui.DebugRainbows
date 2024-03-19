

namespace Plugin.Maui.DebugRainbows.Controls;

public class DebugGrid : ContentView
{
    public double HorizontalItemSize { get; init; } = 20;
    public double VerticalItemSize { get; init; } = 20;
    public int MajorGridLineInterval { get; init; } = 4;
    public GridLineOptions MajorGridLines { get; init; } = new() { Color = Color.FromRgb(255, 0, 0), Opacity = 1, Width = 4 };
    public GridLineOptions MinorGridLines { get; init; } = new() { Color = Color.FromRgb(255, 0, 0), Opacity = 1, Width = 1 };
    public DebugGridOrigin GridOrigin { get; init; }

    //public bool Inverse { get; set; }
    
    //public bool MakeGridRainbows { get; set; }

    public DebugGrid()
    {
        // The debug grid should always just be a visual helper.
        InputTransparent = true;
    }
}
