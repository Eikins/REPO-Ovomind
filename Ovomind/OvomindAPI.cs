using System.Threading.Tasks;

namespace OvomindEmotions.Ovomind
{
    public static class OvomindAPI
    {
        private const float DefaultKeepAliveInterval = 30f;

        public static int SessionId { get; private set; }
        public static int GameId { get; private set; }

        private static readonly OvomindService s_OvomindService = new OvomindService();

        public static async Task InitializeAsync(string token)
        {
            await s_OvomindService.Connect(token);
        }

        public static OvomindDataPoint GetLatestDataPoint()
        {
            return s_OvomindService.LatestDataPoint;
        }

        public static void Disconnect()
        {
            s_OvomindService.Disconnect();
        }
    }
}
