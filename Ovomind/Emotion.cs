using UnityEngine;

namespace Ovomind
{
    public enum Emotion : byte
    {
        Joy,
        Excited,
        Alarmed,
        Annoyed,
        Anxious,
        Bored,
        Serious,
        Relaxed,
        Neutral,

        Undefined = byte.MaxValue
    }

    public static class EmotionsExtensions
    {
        public static Vector2 GetValenceArousalPosition(this Emotion emotion)
        {
            switch (emotion)
            {
                case Emotion.Joy: return new Vector2(0.97f, 0.56f);
                case Emotion.Excited: return new Vector2(0.845f, 0.86f);
                case Emotion.Alarmed: return new Vector2(0.46f, 0.945f);
                case Emotion.Annoyed: return new Vector2(0.28f, 0.83f);
                case Emotion.Anxious: return new Vector2(0.14f, 0.105f);
                case Emotion.Bored: return new Vector2(0.33f, 0.105f);
                case Emotion.Serious: return new Vector2(0.61f, 0.17f);
                case Emotion.Relaxed: return new Vector2(0.855f, 0.17f);
                case Emotion.Neutral: return new Vector2(0.5f, 0.5f);
                default: return Vector2.negativeInfinityVector;
            }
        }

        public static Color GetColor(this Emotion emotion)
        {
            switch (emotion)
            {
                case Emotion.Joy: return new Color(1f, 0.972f, 0.302f, 1f);
                case Emotion.Excited: return new Color(0.949f, 0.533f, 0.149f, 1f);
                case Emotion.Alarmed: return new Color(0.824f, 0.145f, 0.098f, 1f);
                case Emotion.Annoyed: return new Color(0.714f, 0.071f, 0.263f, 1f);
                case Emotion.Anxious: return new Color(0.937f, 0.361f, 0.706f, 1f);
                case Emotion.Bored: return new Color(0.388f, 0.102f, 0.753f, 1f);
                case Emotion.Serious: return new Color(0.169f, 0.188f, 0.667f, 1f);
                case Emotion.Relaxed: return new Color(0.102f, 0.702f, 0.753f, 1f);
                case Emotion.Neutral: return new Color(0.5f, 0.5f, 0.5f, 1f);
                default: return Color.black;
            }
        }
    }
}
