using SchummelPartie.setting.settings;

namespace SchummelPartie.module.modules;

public class ModuleCrownCapture : ModuleMinigame<BombKingController>
{
    public SettingSwitch AlwaysCrown;

    public SettingSwitch NoStun;

    public ModuleCrownCapture() : base("Crown Capture", "No Punch Interval, No Stun, Always Crown")
    {
        NoStun = new SettingSwitch(Name, "No Stun");
        AlwaysCrown = new SettingSwitch(Name, "Always Crown");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Enabled)
            if (GameManager.Minigame is BombKingController bombKingController)
                foreach (var player in bombKingController.players)
                    if (player is BombKingPlayer bombKingPlayer)
                        if (player.IsMe())
                        {
                            if ((bool)NoStun.GetValue()) bombKingPlayer.Stunned = false;
                            if ((bool)AlwaysCrown.GetValue())
                                if (!bombKingPlayer.HoldingCrown)
                                {
                                    foreach (var p in bombKingController.players)
                                        if (p is BombKingPlayer enemy && enemy.HoldingCrown)
                                            enemy.HoldingCrown = false;
                                    bombKingPlayer.HoldingCrown = true;
                                }
                        }
    }
}