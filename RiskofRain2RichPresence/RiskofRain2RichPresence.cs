using System;
using System.Collections.Generic;
using BepInEx;
using RoR2;

namespace RiskofRain2RichPresence {

    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("me.rileyflynn.riskofrain2richpresence", "Risk of Rain 2 Rich Presence", "1.0.0")]
    public class RiskofRain2RichPresence : BaseUnityPlugin {

        private Dictionary<string, string> _stageNames = new Dictionary<string, string> {
            {"foggyswamp", "Wetland Aspect"},
            {"blackbeach", "Distant Roost"},
            {"golemplains", "Titanic Plains"},
            {"goolake", "Abandoned Aqueduct"},
            {"frozenwall", "Rallypoint Delta"},
            {"dampcavesimple", "Abyssal Depths"}
        };

        public void Awake() {
            Logger.LogInfo("Initializing RPC");
            var handlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize("570764972238307329", ref handlers, true, "632360");
            DiscordRpc.UpdatePresence(new DiscordRpc.RichPresence {
                state = "On the main menu",
                largeImageKey = "generic_icon"
            });

            RoR2.Stage.onServerStageBegin += Stage_onServerStageBegin;
        }

        private void Stage_onServerStageBegin(RoR2.Stage obj) {
            var presence = new DiscordRpc.RichPresence {largeImageKey = "generic_icon", startTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds()};
            try {
                presence.state = $"Exploring {_stageNames[obj.sceneDef.sceneName]}";
            }
            catch (KeyNotFoundException) {
                presence.state = $"Exploring unknown stage {obj.sceneDef.sceneName}";
            }
            DiscordRpc.UpdatePresence(presence);
            
        }
    }
}
