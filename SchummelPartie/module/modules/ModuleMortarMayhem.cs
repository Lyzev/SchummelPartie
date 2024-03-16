using SchummelPartie.render;
using UnityEngine;

namespace SchummelPartie.module.modules;

public class ModuleMortarMayhem : ModuleMinigame<MortarMayhemController>
{
    public ModuleMortarMayhem() : base("Mortar Mayhem", "Show the answer to the mortar mayhem.")
    {
    }

    public override void OnGUI()
    {
        base.OnGUI();
        if (Enabled)
        {
            if (GameManager.Minigame is MortarMayhemController
                {
                    mortarMayhemState: MortarMayhemController.MortarMayhemState
                        .DoingPattern
                } mortarMayhemController)
            {
                for (var i = 0; i < GameManager.GetPlayerCount(); i++)
                {
                    var player = mortarMayhemController.GetPlayer(i) as MortarMayhemPlayer;
                    if (player != null && !player.IsDead)
                    {
                        if (Camera.current != null)
                        {
                            Render.DrawESP(Camera.current.WorldToScreenPoint(mortarMayhemController.GetGridPos(
                                    player.OwnerSlot,
                                    mortarMayhemController.curX[player.OwnerSlot],
                                    mortarMayhemController.curY[player.OwnerSlot])), 50f, 50f, Color.red,
                                me: Camera.current.WorldToScreenPoint(player.transform.position));
                        }
                    }
                }
            }
        }
    }
}