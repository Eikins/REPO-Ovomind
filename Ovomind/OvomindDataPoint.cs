using Ovomind;

namespace OvomindEmotions.Ovomind
{
    public readonly struct OvomindDataPoint
    {
        public int BPM { get; }
        public int Activation { get; }
        public float BreathingRate { get; }
        public float Valence { get; }
        public float Arousal { get; }

        public Emotion ClosestEmotion { get; }
        public Emotion SecondaryClosestEmotion { get; }

        public float NormalizedAcceleration { get; }

        public float Latency { get; }
        public RoundtripLatency RoundtripLatency { get; }

        public float SimpleBr => BPM / 4;

        public OvomindDataPoint(
            int bpm, 
            int activation, 
            float breathingRate, 
            float valence, 
            float arousal, 
            Emotion closestEmotion, 
            Emotion secondaryClosestEmotion,
            float normalizedAcceleration,
            float latency,
            RoundtripLatency roundtripLatency)
        {
            BPM = bpm;
            Activation = activation;
            BreathingRate = breathingRate;
            Valence = valence;
            Arousal = arousal;
            ClosestEmotion = closestEmotion;
            SecondaryClosestEmotion = secondaryClosestEmotion;
            NormalizedAcceleration = normalizedAcceleration;
            Latency = latency;
            RoundtripLatency = roundtripLatency;
        }
    }
}
