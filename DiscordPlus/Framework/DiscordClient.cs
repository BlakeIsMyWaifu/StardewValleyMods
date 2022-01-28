using DiscordRPC;
using StardewModdingAPI;
using StardewValley;
using StardewModdingAPI.Utilities;
using Netcode;

namespace BlakeIsMyWaifu.Stardew.DiscordPlus
{
    public class DiscordClient
    {
        private static readonly string _clientId = "936297008615010384";
        private static Timestamps _timestamps;

        public static DiscordRpcClient StartDiscordClient(IMonitor monitor)
        {
            DiscordRpcClient client = new DiscordRpcClient(_clientId, autoEvents: false, logger: new Logger(monitor));

            client.OnReady += (sender, e) =>
            {
                monitor.Log("Connected: " + e.User.ToString(), LogLevel.Info);
            };

            client.RegisterUriScheme(executable: "explorer steam://rungameid/413150");

            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                Details = "In Main Menu",               // {farm} Farm, ({money}g)
                State = "",                             // Year: {year} | {season} | {day} | {time}
                Assets = new Assets()
                {
                    LargeImageKey = "large",            // Logo
                    LargeImageText = ""       // {location}
                                              // Plus
                                              // {X of X} Invite code
                }
            });

            _timestamps = Timestamps.Now;

            return client;
        }

        public static RichPresence UpdatePresence()
        {
            NetString farmName = Game1.player.farmName;
            int money = Game1.player.Money;

            int year = Game1.year;
            string season = Utility.getSeasonNameFromNumber(Utility.getSeasonNumber(SDate.Now().Season));
            int day = SDate.Now().Day;
            string dayEnding = Utility.getNumberEnding(day);
            string time = Game1.getTimeOfDayString(Game1.timeOfDay);

            string location = Game1.currentLocation.Name;

            RichPresence presence = new RichPresence
            {
                Details = $"{farmName} Farm, {money}g",
                State = $"Year: {year}, {season} {day}{dayEnding}, {time}"
            };

            Assets assets = new Assets()
            {
                LargeImageKey = "large",
                LargeImageText = $"Location: {location}"
            };

            presence.Assets = assets;
            presence.Timestamps = _timestamps;

            return presence;
        }
    }
}
