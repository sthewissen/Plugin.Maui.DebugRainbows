#if IOS || MACCATALYST
using PlatformView = DebugRainbows.Maui.Platforms.MaciOS.MauiDebugGrid;
#elif ANDROID
using PlatformView = DebugRainbows.Maui.Platforms.Android.MauiDebugGrid;
//#elif WINDOWS
//using PlatformView = DebugRainbows.Maui.Platforms.Windows.MauiDebugGrid;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
using PlatformView = System.Object;
#endif

using Plugin.Maui.DebugRainbowsControls;
using Microsoft.Maui.Handlers;

namespace Plugin.Maui.DebugRainbows.Handlers
{
    public partial class DebugGridHandler
    {
        public static IPropertyMapper<DebugGrid, DebugGridHandler> PropertyMapper = new PropertyMapper<DebugGrid, DebugGridHandler>(ViewHandler.ViewMapper)
        {
            [nameof(DebugGrid.MajorGridLineInterval)] = MapMajorGridLineInterval,
            [nameof(DebugGrid.HorizontalItemSize)] = MapHorizontalItemSize,
            [nameof(DebugGrid.VerticalItemSize)] = MapVerticalItemSize,
            [nameof(DebugGrid.MajorGridLines)] = MapMajorGridLines,
            [nameof(DebugGrid.MinorGridLines)] = MapMinorGridLines,
            [nameof(DebugGrid.GridOrigin)] = MapGridOrigin
        };

        public DebugGridHandler() : base(PropertyMapper)
        {

        }
    }
}
