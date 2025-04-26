using Ovomind;
using OvomindEmotions.Ovomind.Protocol;
using SocketIO.Core;
using SocketIO.Serializer.NewtonsoftJson;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace OvomindEmotions.Ovomind
{
    public class OvomindService
    {
        public int SessionId { get; private set; } = -1;
        public int GameId { get; private set; } = -1;
        public OvomindDataPoint LatestDataPoint { get; private set; } = default;

        private SocketIOClient.SocketIO m_Socket;

        public Action onSessionStarted;

        public OvomindService() 
        {
        }

        public async Task Connect(string accessToken)
        {
            var uri = new Uri("https://ws.dev.ovomind.com/");
            m_Socket = new (uri, new SocketIOOptions
            {
                EIO = EngineIO.V4,
                Transport = SocketIOClient.Transport.TransportProtocol.WebSocket,
                Auth = new Dictionary<string, string>
                {
                    { "token", accessToken }
                }
            });

            m_Socket.Serializer = new NewtonsoftJsonSerializer();

            m_Socket.OnConnected += OnConnected;
            m_Socket.OnDisconnected += OnDisconnected;
            m_Socket.OnError += OnError;

            m_Socket.On("session:started", OnSessionStarted);

            m_Socket.On("session:metrics", OnSessionMetrics);
            m_Socket.On("session:sdk-receive", OnSessionMetrics);

            // Not implemented yet
            m_Socket.On("user:joined", (response) => { });
            m_Socket.On("user:join", (response) => { });

            await m_Socket.ConnectAsync();
        }

        public async void Disconnect()
        {
            SessionId = -1;
            GameId = -1;
            await m_Socket?.DisconnectAsync();
            m_Socket.Dispose();
        }

        private void OnError(object sender, string e)
        {
        }

        private void OnConnected(object sender, EventArgs e)
        {
            m_Socket.EmitAsync("user:join", "");
        }

        private void OnDisconnected(object sender, string e)
        {
        }

        private void OnSessionMetrics(SocketIOResponse response)
        {
            var message = response.GetValue<SessionMetricsMessage>();

            var (closestEmotion, secondaryClosestEmotion) = GetClosestEmotions(message.Data.Valence, message.Data.Arousal);

            var dataPoint = new OvomindDataPoint(
                (int) message.Data.ExponentialDecayBpm,
                message.Data.Activation,
                message.Data.BreathingRate,
                message.Data.Valence,
                message.Data.Arousal,
                closestEmotion,
                secondaryClosestEmotion,
                0,
                message.Data.Latency,
                message.Data.RoundtripLatency
            );

            LatestDataPoint = dataPoint;
        }

        private void OnSessionStarted(SocketIOResponse response)
        {
            var message = response.GetValue<SessionStartedMessage>();
            SessionId = message.SessionId;
            GameId = message.Game.Id;

            onSessionStarted?.Invoke();
        }

        private static (Emotion, Emotion) GetClosestEmotions(float valence, float arousal)
        {
            var valenceArousal = new Vector2(valence, arousal);
            var closestEmotion = Emotion.Undefined;
            var secondaryClosestEmotion = Emotion.Undefined;
            float closestDistanceSq = float.PositiveInfinity;

            for (int i = 0; i < s_EmotionValenceArousalLookup.Length; i++)
            {
                var entry = s_EmotionValenceArousalLookup[i];
                var distanceSq = (entry.valenceArousal - valenceArousal).sqrMagnitude;
                if (distanceSq <= closestDistanceSq)
                {
                    secondaryClosestEmotion = closestEmotion;
                    closestEmotion = entry.emotion;
                    closestDistanceSq = distanceSq;
                }
            }

            return (closestEmotion, secondaryClosestEmotion);
        }

        private static readonly (Emotion emotion, Vector2 valenceArousal)[] s_EmotionValenceArousalLookup =
        {
            (Emotion.Joy, new Vector2(0.97f, 0.56f)),
            (Emotion.Excited, new Vector2(0.845f, 0.86f)),
            (Emotion.Alarmed, new Vector2(0.46f, 0.945f)),
            (Emotion.Annoyed, new Vector2(0.28f, 0.83f)),
            (Emotion.Anxious, new Vector2(0.14f, 0.105f)),
            (Emotion.Bored, new Vector2(0.33f, 0.105f)),
            (Emotion.Serious, new Vector2(0.61f, 0.17f)),
            (Emotion.Relaxed, new Vector2(0.855f, 0.17f)),
            (Emotion.Neutral, new Vector2(0.5f, 0.5f))
        };
    }
}
