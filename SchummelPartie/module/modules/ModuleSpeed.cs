using System.Linq;
using System.Reflection;
using SchummelPartie.setting.settings;

namespace SchummelPartie.module.modules;

public class ModuleSpeed : Module
{
    public SettingSlider MaxSpeed;

    public ModuleSpeed() : base("Speed", "Allows you to change your speed.")
    {
        MaxSpeed = new SettingSlider(Name, "Max Speed", 5, 15, 15);
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
                if (characterMover != null) characterMover.maxSpeed = (float)MaxSpeed.GetValue();
            }
    }
}