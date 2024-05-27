using System.Linq;
using SchummelPartie.render;
using UnityEngine;

namespace SchummelPartie.module.modules;

public class ModuleESP : Module
{
    public ModuleESP() : base("Extra Sensory Perception", "Allows you to see through walls.")
    {
    }

    public override void OnGUI()
    {
        base.OnGUI();
        if (Enabled)
            if (GameManager.Minigame != null && GameManager.Minigame.Playable && GameManager.Minigame is not FinderController && GameManager.Minigame.players != null &&
                GameManager.Minigame.players.Count > 0 &&
                GameManager.Minigame.players.FirstOrDefault(p => p.IsMe()) is { } me)
            {
                if (Camera.current != null)
                {
                    var mePos = Camera.current.WorldToScreenPoint(me.transform.position);
                    foreach (var player in GameManager.Minigame.players)
                        if (!player.IsDead && (!player.IsOwner || player.GamePlayer.IsAI))
                        {
                            var playerPos =
                                Camera.current.WorldToScreenPoint(player.transform.position);
                            Render.DrawESP(playerPos, 50f, 100f, Color.red, me: mePos,
                                name: $"[{player.GamePlayer.Name}]");
                        }
                }
            }
    }
}