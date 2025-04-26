using Newtonsoft.Json;

namespace OvomindEmotions.Ovomind.Protocol
{
    public class SessionMetricsMessage
    {
        [JsonProperty("session_id")]
        public int SessionId { get; set; }

        [JsonProperty("game_id")]
        public int GameId { get; set; }

        [JsonProperty("data")]
        public SessionMetricsData Data { get; set; }

        public struct SessionMetricsData
        {
            [JsonProperty("bpm")]
            public float ExponentialDecayBpm { get; set; }

            [JsonProperty("breathing")]
            public float BreathingRate { get; set; }

            [JsonProperty("arousal")]
            public float Arousal { get; set; }

            [JsonProperty("activation")]
            public int Activation { get; set; }

            [JsonProperty("valence")]
            public float Valence { get; set; }

            [JsonProperty("latency")]
            public float Latency { get; set; }

            [JsonProperty("roundtripLatency")]
            public RoundtripLatency RoundtripLatency { get; set; }
        }
    }
}