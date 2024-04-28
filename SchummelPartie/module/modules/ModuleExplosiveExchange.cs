using System.Linq;
using SchummelPartie.setting.settings;

namespace SchummelPartie.module.modules;

public class ModuleExplosiveExchange : ModuleMinigame<PassTheBombController>
{
    public SettingSwitch NoBomb;

    public SettingSwitch NoStun;

    public ModuleExplosiveExchange() : base("Explosive Exchange", "No Punch Interval, No Stun, Always Crown")
    {
        NoStun = new SettingSwitch(Name, "No Stun");
        NoBomb = new SettingSwitch(Name, "No Bomb");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Enabled)
            if (GameManager.Minigame is PassTheBombController passTheBombController)
                foreach (var player in passTheBombController.players)
                    if (player is PassTheBombPlayer passTheBombPlayer)
                        if (player.IsMe())
                        {
                            if ((bool)NoStun.GetValue()) passTheBombPlayer.Stunned = false;
                            if ((bool)NoBomb.GetValue())
                            {
                                var enemy = (PassTheBombPlayer)passTheBombController.players.FirstOrDefault(p =>
                                    !p.IsMe() && !p.IsDead);
                                if (enemy != null) enemy.HoldingBomb = true;
                                passTheBombPlayer.HoldingBomb = false;
                            }
                        }
    }
}