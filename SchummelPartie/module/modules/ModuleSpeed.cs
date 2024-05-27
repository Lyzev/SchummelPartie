using System.Linq;
using System.Reflection;
using SchummelPartie.setting.settings;

namespace SchummelPartie.module.modules;

public class ModuleSpeed : Module
{
    public SettingSlider MaxSpeed;
    public SettingSlider Acceleration;
    public SettingSlider Deceleration;
    public SettingSlider AirAcceleration;
    public SettingSlider AirDeceleration;

    public ModuleSpeed() : base("Speed", "Allows you to change your speed.")
    {
        MaxSpeed = new SettingSlider(Name, "Max Speed", 5, 30, 15);
        Acceleration = new SettingSlider(Name, "Acceleration", 1, 30, 15);
        Deceleration = new SettingSlider(Name, "Deceleration", 1, 30, 15);
        AirAcceleration = new SettingSlider(Name, "Air Acceleration", 1, 30, 1);
        AirDeceleration = new SettingSlider(Name, "Air Deceleration", 1, 30, 15);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Enabled)
            if (GameManager.Minigame != null && GameManager.Minigame.Playable && GameManager.Minigame.players != null &&
                GameManager.Minigame.players.Count > 0 &&
                GameManager.Minigame.players.FirstOrDefault(p => p.IsMe()) is { } me)
            {
                var characterMover = me.GetComponent<CharacterMover>();
                if (characterMover != null)
                {
                    characterMover.maxSpeed = (float)MaxSpeed.GetValue();
                    characterMover.acceleration = (float)Acceleration.GetValue();
                    characterMover.deceleration = (float)Deceleration.GetValue();
                    characterMover.airAcceleration = (float)AirAcceleration.GetValue();
                    characterMover.airDeceleration = (float)AirDeceleration.GetValue();
                }
            }
    }
}