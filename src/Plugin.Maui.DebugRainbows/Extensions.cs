using Plugin.Maui.DebugRainbows.Controls;
using Plugin.Maui.DebugRainbows.Handlers;
using Microsoft.Maui.Handlers;

namespace Plugin.Maui.DebugRainbows
{
    public static class Extensions
    {
        private static readonly Random R = new();

        /// <summary>
        /// Can be used to enable the DebugRainbows plugin.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>An instance of MauiAppBuilder that can be used to chain further configuration.</returns>
        public static MauiAppBuilder UseDebugRainbows(this MauiAppBuilder builder)
        {
            return builder.UseDebugRainbows(new DebugRainbowsOptions { ShowRainbows = true });
        }

        /// <summary>
        /// Can be used to enable the DebugRainbows plugin.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns>An instance of MauiAppBuilder that can be used to chain further configuration.</returns>
        public static MauiAppBuilder UseDebugRainbows(this MauiAppBuilder builder, DebugRainbowsOptions options)
        {
            if (options.ShowRainbows)
            {
                ViewHandler.ElementMapper.AppendToMapping(nameof(DebugRainbows), (handler, view) =>
                {
                    if (view is not DebugGrid && view is View l)
                        l.BackgroundColor = GetRandomColor();
                });
            }


            if (options.ShowGrid)
            {
                PageHandler.ElementMapper.AppendToMapping(nameof(DebugRainbows), (handler, view) =>
                {
                    if (view is not ContentPage page || page.Content.ClassId == nameof(DebugRainbows)) 
                        return;

                    var pageContent = page.Content;
                    page.Content = null;

                    var gridContent = new DebugGrid
                    {
                        MajorGridLines = options.MajorGridLines,
                        MinorGridLines = options.MinorGridLines,
                        HorizontalItemSize = options.HorizontalItemSize,
                        VerticalItemSize = options.VerticalItemSize,
                        MajorGridLineInterval = options.MajorGridLineInterval,
                        GridOrigin = options.GridOrigin
                    };
                    var newContent = new Grid
                    {
                        ClassId = nameof(DebugRainbows),
                        HeightRequest = pageContent.Height,
                        WidthRequest = pageContent.Width
                    };

                    newContent.Children.Add(pageContent);
                    newContent.Children.Add(gridContent);

                    page.Content = newContent;
                });

                builder.ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler(typeof(DebugGrid), typeof(DebugGridHandler));
                });
            }

            return builder;
        }

        /// <summary>
        /// Gets a random color using the RGB color space.
        /// </summary>
        /// <returns>A randomized color.</returns>
        private static Color GetRandomColor() => Color.FromRgb((byte)R.Next(0, 255), (byte)R.Next(0, 255), (byte)R.Next(0, 255));
    }
}
