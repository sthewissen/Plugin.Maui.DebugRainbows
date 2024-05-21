using Microsoft.Maui.Handlers;
using Plugin.Maui.DebugRainbows.Controls;

namespace Plugin.Maui.DebugRainbows.Handlers
{
    public partial class DebugGridHandler
    {
        protected override MauiDebugGrid CreatePlatformView() => new(VirtualView);

        protected override void ConnectHandler(MauiDebugGrid platformView)
        {
           throw new NotImplementedException();
        }

        protected override void DisconnectHandler(MauiDebugGrid platformView)
		{
           throw new NotImplementedException();
        }

        public static void MapMajorGridLineInterval(DebugGridHandler handler, DebugGrid debugGrid)
        {
           throw new NotImplementedException();
		}

		public static void MapMajorGridLines(DebugGridHandler handler, DebugGrid debugGrid)
		{
           throw new NotImplementedException();
		}

		public static void MapMinorGridLines(DebugGridHandler handler, DebugGrid debugGrid)
		{
           throw new NotImplementedException();
		}

		public static void MapGridOrigin(DebugGridHandler handler, DebugGrid debugGrid)
		{
           throw new NotImplementedException();
		}

		public static void MapHorizontalItemSize(DebugGridHandler handler, DebugGrid debugGrid)
		{
           throw new NotImplementedException();
		}

		public static void MapVerticalItemSize(DebugGridHandler handler, DebugGrid debugGrid)
		{
           throw new NotImplementedException();
		}
	}
}
