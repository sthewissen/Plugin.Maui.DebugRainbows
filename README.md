<!-- 
Everything in here is of course optional. If you want to add/remove something, absolutely do so as you see fit.
This example README has some dummy APIs you'll need to replace and only acts as a placeholder for some inspiration that you can fill in with your own functionalities.
-->
![Debug Rainbows](https://raw.githubusercontent.com/sthewissen/Plugin.Maui.DebugRainbows/main/nuget.png)
# Plugin.Maui.DebugRainbows

Have you ever had a piece of XAML code that didn't produce the layout you expected? Did you change the background colors on certain elements to get an idea of where they are positioned? Admit it, you have and pretty much all of us have at some point. Either way, this is the package for you! It adds some nice colorful debug modes to your ContentPages or specific visual elements that let you immediately see where all of your elements are located!

![Debug Rainbows](https://raw.githubusercontent.com/sthewissen/Plugin.Maui.DebugRainbows/main/images/sample.png)

## Install Plugin

[![NuGet](https://img.shields.io/nuget/v/Plugin.Maui.DebugRainbows.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.Maui.DebugRainbows/)

Available on [NuGet](http://www.nuget.org/packages/Plugin.Maui.DebugRainbows).

Install with the dotnet CLI: `dotnet add package Plugin.Maui.DebugRainbows`, or through the NuGet Package Manager in Visual Studio.

### Supported Platforms

| Platform | Minimum Version Supported |
|----------|---------------------------|
| iOS      | 11+                       |
| macOS    | 10.15+                    |
| Android  | 5.0 (API 21)              |

## API Usage

### Registration

You will first need to register **DebugRainbows** with the `MauiAppBuilder` by calling `UseDebugRainbows()`:

```csharp
public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();
    builder
        .UseMauiApp<App>()
        // This will add the rainbow backgrounds by default.
        .UseDebugRainbows();

        // Alternatively provide an Options object:
        .UseDebugRainbows(new DebugRainbowsOptions
        {
           ShowRainbows = true,
           ShowGrid = true,
           HorizontalItemSize = 20,
           VerticalItemSize = 20,
           MajorGridLineInterval = 4,
           MajorGridLines = new GridLineOptions { Color = Color.FromRgb(255, 0, 0), Opacity = 1, Width = 4 },
           MinorGridLines = new GridLineOptions { Color = Color.FromRgb(255, 0, 0), Opacity = 1, Width = 1 },
           GridOrigin = DebugGridOrigin.TopLeft,
        });

    return builder.Build();
}
```

### Features

Once you have set up DebugRainbows it will color all of your UI elements (everything inheriting from `View`) automatically. 
This way you can verify which elements might be taking up unnecessary space or trace elements that might not be behaving as expected.
Additionally you have the option to overlay a visual grid onto your page. This grid is completely configurable and helps you identifying areas that might
not be adhering to the correct padding/margin and other constraints you're adding to your visual elements. Effectively its a digital ruler that you can use to tackle
all of your alignment problems.

#### `DebugRainbowOptions` Properties

##### `ShowRainbows`
Sets a value indicating whether or not the rainbow-colored backgrounds should be applied.

##### `ShowGrid`
Sets a value indicating whether or not the alignment grid should be shown.

##### `HorizontalItemSize`
Sets a value indicating the amount of display units between horizontal grid lines.

##### `VerticalItemSize`
Sets a value indicating the amount of display units between vertical grid lines.

##### `MajorGridLineInterval`
Sets a value indicating the interval at which a major grid line is drawn in relation to minor grid lines. A value of `4` means every 4th line will be a major grid line.

##### `MajorGridLines`
Sets a value representing the styling applied to every major grid line. This is set through the `GridLineOptions` class that exposes the properties `Color`, `Opacity` and `Width` to style a grid line.

##### `MinorGridLines`
Sets a value representing the styling applied to every minor grid line. This is set through the `GridLineOptions` class that exposes the properties `Color`, `Opacity` and `Width` to style a grid line.

##### `GridOrigin`
Sets a value representing the origin point of where the grid is initially drawn from. Valid values are:

- `TopLeft`: The grid starts at the top-left corner of the screen.
- `Center`: There will be a major grid line at the center of the screen and additional grid lines will be drawn relative to this center line.

# Acknowledgements

This project could not have come to be without these projects and people, thank you! <3

- The original [DebugRainbows for Xamarin.Forms](https://github.com/sthewissen/Xamarin.Forms.DebugRainbows), also by me :D
- The ever-inspiring [Gerald Versluis](https://github.com/jfversluis) 
