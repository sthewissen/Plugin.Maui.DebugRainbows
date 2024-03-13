<!-- 
Everything in here is of course optional. If you want to add/remove something, absolutely do so as you see fit.
This example README has some dummy APIs you'll need to replace and only acts as a placeholder for some inspiration that you can fill in with your own functionalities.
-->
![Debug Rainbows](https://raw.githubusercontent.com/sthewissen/Plugin.Maui.DebugRainbows/main/nuget.png)
# Plugin.Maui.DebugRainbows

Have you ever had a piece of XAML code that didn't produce the layout you expected? Did you change the background colors on certain elements to get an idea of where they are positioned? Admit it, you have and pretty much all of us have at some point. Either way, this is the package for you! It adds some nice colorful debug modes to your ContentPages or specific visual elements that let you immediately see where all of your elements are located!

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
        .UseDebugRainbows();
        // Coming Soon: Optionally provide an Options object:
        //.UseDebugRainbows(new DebugRainbowOptions());

    return builder.Build();
}
```

### Features

Once you have set up DebugRainbows it will color all of your UI elements (everything inheriting from `View`) automatically. 
This way you can verify which elements might be taking up unnecessary space or trace elements that might not be behaving as expected.

🔜 **Coming soon:** additional grid overlay features currently already present in the old Xamarin.Forms version. 

# Acknowledgements

This project could not have come to be without these projects and people, thank you! <3

- The original [DebugRainbows for Xamarin.Forms](https://github.com/sthewissen/Xamarin.Forms.DebugRainbows), also by me :D
- The ever-inspiring [Gerald Versluis](https://github.com/jfversluis)
