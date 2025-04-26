using Newtonsoft.Json;

namespace OvomindEmotions.Ovomind.Protocol
{
    public class SessionStartedMessage
    {
        [JsonProperty("id")]
        public int SessionId { get; set; }

        [JsonProperty("game")]
        public GameInfo Game { get; set; }

        public struct GameInfo
        {
            [JsonProperty("id")]
            public int Id { get; set; }
        }
    }
}
