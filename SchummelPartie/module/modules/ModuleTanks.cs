using System.Reflection;
using UrGUI.UWindow;

namespace SchummelPartie.module.modules;

public class ModuleTanks : ModuleMinigame<TanksController>
{

    private bool _rapidFire = false;

    public ModuleTanks() : base("Tanks", "Rapid Fire")
    {
    }

    public override void OnUpdate()
    {
        if (Enabled)
        {
            if (GameManager.Minigame is TanksController tanksController)
            {
                foreach (var player in tanksController.players)
                {
                    if (player is TanksPlayer tanksPlayer)
                    {
                        if (player.IsMe())
                        {
                            if (_rapidFire)
                            {
                                typeof(TanksPlayer).GetField("m_fireRate", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(tanksPlayer, 0.001f);
                            }
                            else
                            {
                                typeof(TanksPlayer).GetField("m_fireRate", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(tanksPlayer, 2f);
                            }
                        }
                    }
                }
            }
        }
    }

    public override void OnSettings(UWindow window)
    {
        base.OnSettings(window);
        window.SameLine();
        window.Label("Rapid Fire");
        window.Toggle("Rapid Fire", value => { _rapidFire = value; }, _rapidFire);
    }
}