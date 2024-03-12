using Plugin.Maui.DebugRainbows.Controls;
using Plugin.Maui.DebugRainbows.Handlers;
using Microsoft.Maui.Handlers;

namespace Plugin.Maui.DebugRainbows
{
    public static class Extensions
    {
        static readonly Random R = new();

        /// <summary>
        /// Can be used to enable the DebugRainbows background coloring.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>An instance of MauiAppBuilder that can be used to chain further configuration.</returns>
        public static MauiAppBuilder UseDebugRainbows(this MauiAppBuilder builder)
        {
            ViewHandler.ElementMapper.AppendToMapping(nameof(DebugRainbows), (handler, view) =>
            {
                if (view is View l)
                    l.BackgroundColor = GetRandomColor();
            });

            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler(typeof(DebugGrid), typeof(DebugGridHandler));
            });

            return builder;
        }

        /// <summary>
        /// Gets a random color using the RGB color space.
        /// </summary>
        /// <returns>A randomized color.</returns>
        public static Color GetRandomColor() 
            => Color.FromRgb((byte)R.Next(0, 255), (byte)R.Next(0, 255), (byte)R.Next(0, 255));
    }
}
