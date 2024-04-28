using System.Linq;
using SchummelPartie.render;
using UnityEngine;

namespace SchummelPartie.module.modules;

public class ModuleFinder : ModuleMinigame<FinderController>
{
    public ModuleFinder() : base("Finder", "Show the position of the other players.")
    {
    }

    public override void OnGUI()
    {
        base.OnGUI();
        if (Enabled)
            if (GameManager.Minigame is FinderController finderController)
            {
                var me = finderController.players.First(player => player.IsMe());
                if (Camera.current != null)
                {
                    var mePos = Camera.current.WorldToScreenPoint(me.transform.position);
                    foreach (var player in finderController.players)
                        if (player is FinderPlayer finderPlayer)
                            if (player != null && !finderPlayer.escaped && !player.IsDead &&
                                (!player.IsOwner || player.GamePlayer.IsAI))
                            {
                                var playerPos = Camera.current.WorldToScreenPoint(player.transform.position);
                                Render.DrawESP(playerPos, 20f, 20f, Color.red, me: mePos,
                                    name: $"[{player.GamePlayer.Name}]");
                            }

                    if (finderController.beacons != null && finderController.beacons.Length > 0)
                        foreach (var beacon in finderController.beacons)
                            if (beacon != null && !beacon.PickedUp)
                            {
                                var beaconPos = Camera.current.WorldToScreenPoint(beacon.transform.position);
                                Render.DrawESP(beaconPos, 20f, 20f, Color.green, me: mePos,
                                    name: "[Beacon]");
                            }
                }
            }
    }
}