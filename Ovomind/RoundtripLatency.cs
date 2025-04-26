using Newtonsoft.Json;

namespace OvomindEmotions.Ovomind
{
    public struct RoundtripLatency
    {
        [JsonProperty("time_event")]
        public long TimeEvent { get; set; }

        [JsonProperty("latency")]
        public float Latency { get; set; }
    }
}
