using System.Linq;
using System.Reflection;
using SchummelPartie.setting.settings;

namespace SchummelPartie.module.modules;

public class ModuleExplosiveExchange : ModuleMinigame<PassTheBombController>
{

    public SettingSwitch NoStun;
    public SettingSwitch NoBomb;

    public ModuleExplosiveExchange() : base("Explosive Exchange", "No Punch Interval, No Stun, Always Crown")
    {
        NoStun = new(Name, "No Stun", false);
        NoBomb = new(Name, "No Bomb", false);
    }

    public override void OnUpdate()
    {
        if (Enabled)
        {
            if (GameManager.Minigame is PassTheBombController passTheBombController)
            {
                foreach (var player in passTheBombController.players)
                {
                    if (player is PassTheBombPlayer passTheBombPlayer)
                    {
                        if (player.IsMe())
                        {
                            if ((bool) NoStun.GetValue())
                            {
                                passTheBombPlayer.Stunned = false;
                            }
                            if ((bool) NoBomb.GetValue())
                            {
                                PassTheBombPlayer enemy = (PassTheBombPlayer) passTheBombController.players.FirstOrDefault(p => !p.IsMe() && !p.IsDead);
                                if (enemy != null)
                                {
                                    enemy.HoldingBomb = true;
                                }
                                passTheBombPlayer.HoldingBomb = false;
                            }
                        }
                    }
                }
            }
        }
    }
}
