using BepInEx;
using RoR2;

namespace RiskofRain2RichPresence {

    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("me.rileyflynn.riskofrain2richpresence", "Risk of Rain 2 Rich Presence", "1.0.0")]
    public class RiskofRain2RichPresence : BaseUnityPlugin {

        public void Awake() {
            Logger.LogInfo("Initializing RPC");
            var handlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize("570764972238307329", ref handlers, true, "632360");
            DiscordRpc.UpdatePresence(new DiscordRpc.RichPresence {
                state = "On the main menu"
            });
        }
    }
}
