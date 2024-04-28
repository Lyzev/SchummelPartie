using System.Linq;
using System.Reflection;
using SchummelPartie.setting.settings;

namespace SchummelPartie.module.modules;

public class ModuleAirJump : Module
{
    public SettingSlider MaxJumps;

    public ModuleAirJump() : base("AirJump", "Allows you to jump in the air.")
    {
        MaxJumps = new SettingSlider(Name, "Max Jumps", 2, 9999, 9999);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Enabled)
            if (GameManager.Minigame != null && GameManager.Minigame.Playable && GameManager.Minigame.players != null &&
                GameManager.Minigame.players.Count > 0 &&
                GameManager.Minigame.players.FirstOrDefault(p => p.IsMe()) is Movement movement)
            {
                var characterMover = (CharacterMover)typeof(Movement)
                    .GetField("m_characterMover", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(movement);
                if (characterMover != null) characterMover.maxJumps = (int)(float)MaxJumps.GetValue();
            }
    }
}