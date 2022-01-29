using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Microsoft.Xna.Framework;

namespace BlakeIsMyWaifu.Stardew.GoodMorningVibes
{
    public class ModEntry : Mod
    {
        private ModConfig _modConfig;

        public override void Entry(IModHelper helper)
        {
            this._modConfig = this.Helper.ReadConfig<ModConfig>();

            helper.Events.GameLoop.DayStarted += SendReminder;
        }

        private void SendReminder(object sender, DayStartedEventArgs e)
        {
            string[] messages = _modConfig.Messages;
            Random random = new Random();
            int index = random.Next(0, messages.Length);
            string message = messages.Length != 0 ? messages[index] : "Good Morning";

            HUDMessage hudMessage = new HUDMessage(message, Color.SeaGreen, 8000f);
            hudMessage.noIcon = true;

            Game1.addHUDMessage(hudMessage);
        }
    }
}
