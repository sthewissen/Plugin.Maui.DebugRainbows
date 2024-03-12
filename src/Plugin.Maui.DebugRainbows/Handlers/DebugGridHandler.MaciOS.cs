using Plugin.Maui.DebugRainbowsControls;
using Microsoft.Maui.Handlers;
using Plugin.Maui.DebugRainbowsPlatforms.MaciOS;

namespace Plugin.Maui.DebugRainbows.Handlers
{
    public partial class DebugGridHandler : ViewHandler<DebugGrid, MauiDebugGrid>
    {
        protected override MauiDebugGrid CreatePlatformView() => new(VirtualView);

        protected override void ConnectHandler(MauiDebugGrid platformView)
        {
            base.ConnectHandler(platformView);
        }

        protected override void DisconnectHandler(MauiDebugGrid platformView)
		{
			platformView.Dispose();
			base.DisconnectHandler(platformView);
        }

        public static void MapMajorGridLineInterval(DebugGridHandler handler, DebugGrid debugGrid)
        {
			handler?.PlatformView.UpdateMajorGridLineInterval();
		}

		public static void MapMajorGridLines(DebugGridHandler handler, DebugGrid debugGrid)
		{
            handler?.PlatformView.UpdateMajorGridLines();
		}

		public static void MapMinorGridLines(DebugGridHandler handler, DebugGrid debugGrid)
		{
			handler?.PlatformView.UpdateMinorGridLines();
		}

		public static void MapGridOrigin(DebugGridHandler handler, DebugGrid debugGrid)
		{
            handler?.PlatformView.UpdateGridOrigin();
		}

		public static void MapHorizontalItemSize(DebugGridHandler handler, DebugGrid debugGrid)
		{
			handler?.PlatformView.UpdateHorizontalItemSize();
		}

		public static void MapVerticalItemSize(DebugGridHandler handler, DebugGrid debugGrid)
		{
            handler?.PlatformView.UpdateVerticalItemSize();
		}
	}
}
