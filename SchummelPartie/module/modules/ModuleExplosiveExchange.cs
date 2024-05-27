using System.Linq;
using SchummelPartie.setting.settings;
using UnityEngine;

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
                            if ((bool)NoBomb.GetValue() && passTheBombPlayer.HoldingBomb)
                            {
                                passTheBombPlayer.PunchRPC(null, Random.value > 0.5);
                            }
                        }
    }
}