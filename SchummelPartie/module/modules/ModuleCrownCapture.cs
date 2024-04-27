using System.Linq;
using System.Reflection;
using SchummelPartie.setting.settings;

namespace SchummelPartie.module.modules;

public class ModuleCrownCapture : ModuleMinigame<BombKingController>
{

    public SettingSwitch NoStun;
    public SettingSwitch AlwaysCrown;

    public ModuleCrownCapture() : base("Crown Capture", "No Punch Interval, No Stun, Always Crown")
    {
        NoStun = new(Name, "No Stun", false);
        AlwaysCrown = new(Name, "Always Crown", false);
    }

    public override void OnUpdate()
    {
        if (Enabled)
        {
            if (GameManager.Minigame is BombKingController bombKingController)
            {
                foreach (var player in bombKingController.players)
                {
                    if (player is BombKingPlayer bombKingPlayer)
                    {
                        if (player.IsMe())
                        {
                            if ((bool) NoStun.GetValue())
                            {
                                bombKingPlayer.Stunned = false;
                            }
                            if ((bool) AlwaysCrown.GetValue())
                            {
                                if (!bombKingPlayer.HoldingCrown)
                                {
                                    foreach (var p in bombKingController.players)
                                    {
                                        if (p is BombKingPlayer enemy && enemy.HoldingCrown)
                                        {
                                            enemy.HoldingCrown = false;
                                        }
                                    }
                                    bombKingPlayer.HoldingCrown = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
