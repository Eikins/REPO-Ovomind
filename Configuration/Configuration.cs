using BepInEx.Configuration;

namespace OvomindEmotions.Config
{
    internal static class Configuration
    {
        public static ConfigEntry<string> UserAccessToken;
        public static ConfigEntry<bool> LogSocketMessages;

        public static void Init(ConfigFile config)
        {
            UserAccessToken = config.Bind(
                "Authentication",
                "AccessToken",
                "",
                "Ovomind user access token"
            );

            LogSocketMessages = config.Bind(
                "Debug",
                "LogSocketMessages",
                false,
                "Enable to log all the socket messages in the console. Have a moderate impact of performance."
            );
        }
    }
}
