using System.Reflection;
using SchummelPartie.setting.settings;

namespace SchummelPartie.module.modules;

public class ModuleTanks : ModuleMinigame<TanksController>
{
    public SettingSwitch RapidFire;

    public ModuleTanks() : base("Tanks", "Rapid Fire.")
    {
        RapidFire = new SettingSwitch(Name, "Rapid Fire");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Enabled)
            if (GameManager.Minigame is TanksController tanksController)
                foreach (var player in tanksController.players)
                    if (player is TanksPlayer tanksPlayer)
                        if (player.IsMe())
                        {
                            if ((bool)RapidFire.GetValue())
                                typeof(TanksPlayer)
                                    .GetField("m_fireRate", BindingFlags.NonPublic | BindingFlags.Instance)
                                    ?.SetValue(tanksPlayer, 0.001f);
                            else
                                typeof(TanksPlayer)
                                    .GetField("m_fireRate", BindingFlags.NonPublic | BindingFlags.Instance)
                                    ?.SetValue(tanksPlayer, 2f);
                        }
    }
}