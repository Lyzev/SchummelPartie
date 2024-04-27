using System;
using System.Linq;
using SchummelPartie.render;
using UnityEngine;

namespace SchummelPartie.module.modules;

public class ModuleSpeedySpotlights : ModuleMinigame<SpeedySpotlightsController>
{
    public ModuleSpeedySpotlights() : base("Speedy Spotlights", "Show the position of the other players.")
    {
    }

    public override void OnGUI()
    {
        base.OnGUI();
        if (Enabled)
        {
            if (GameManager.Minigame is SpeedySpotlightsController speedySpotlightsController)
            {
                var me = speedySpotlightsController.players.First(player => player.IsMe());
                if (Camera.current != null)
                {
                    Vector3 mePos = Camera.current.WorldToScreenPoint(me.transform.position);
                    foreach (var player in speedySpotlightsController.players)
                    {
                        if (player is SpeedySpotlightsPlayer speedySpotlightsPlayer)
                        {
                            if (player != null && !player.IsDead && (!player.IsOwner || player.GamePlayer.IsAI))
                            {
                                Vector3 playerPos = Camera.current.WorldToScreenPoint(player.transform.position);
                                float[] color = new float[3];
                                color[0] = 1.0f - speedySpotlightsPlayer.Health / 100.0f;
                                color[1] = speedySpotlightsPlayer.Health / 100.0f;
                                color[2] = 0.0f;
                                Render.DrawESP(playerPos, 20f, 20f, new Color(color[0], color[1], color[2]), me: mePos,
                                    name: $"[{player.GamePlayer.Name}] [{speedySpotlightsPlayer.Health}]%");
                            }
                        }
                    }
                }
            }
        }
    }
}