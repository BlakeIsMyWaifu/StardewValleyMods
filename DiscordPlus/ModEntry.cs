using StardewModdingAPI;
using StardewModdingAPI.Events;
using DiscordRPC;

namespace BlakeIsMyWaifu.Stardew.DiscordPlus
{
    public class ModEntry : Mod
    {
        private DiscordRpcClient _client;

        public override void Entry(IModHelper helper)
        {
            _client = DiscordClient.StartDiscordClient(Monitor);

            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
        }

        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady)
            {
                return;
            }

            _client.Invoke();
            if (e.IsMultipleOf(30))
            {
                _client.SetPresence(DiscordClient.UpdatePresence());
            }
        }
    }
}
