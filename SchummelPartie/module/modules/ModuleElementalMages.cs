using System;
using System.Reflection;
using MelonLoader;
using SchummelPartie.setting.settings;

namespace SchummelPartie.module.modules;

public class ModuleElementalMages : ModuleMinigame<ElementalMagesController>
{
    public SettingSwitch InstantPickupCrystal;
    public SettingSwitch NoCameraShake;

    public ModuleElementalMages() : base("Elemental Mages", "Instantly pick up crystals and disable camera shake.")
    {
        InstantPickupCrystal = new SettingSwitch(Name, "Instant Pickup Crystal");
        NoCameraShake = new SettingSwitch(Name, "No Camera Shake");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Enabled && ((bool)InstantPickupCrystal.GetValue() || (bool)NoCameraShake.GetValue()))
            if (GameManager.Minigame is ElementalMagesController elementalMagesController)
                foreach (var player in elementalMagesController.players)
                    if (player is ElementalMagesPlayer elementalMagesPlayer)
                        if (player.IsMe())
                        {
                            if ((bool)InstantPickupCrystal.GetValue())
                                try
                                {
                                    foreach (var crystal in elementalMagesController.crystals)
                                        elementalMagesController.RPCDespawnCrystal(null, crystal.id,
                                            (byte)player.OwnerSlot);
                                }
                                catch (Exception)
                                {
                                    // ignored
                                }

                            if ((bool)NoCameraShake.GetValue())
                            {
                                var cameraShakeField = elementalMagesPlayer.GetType().GetField("cameraShake",
                                    BindingFlags.NonPublic | BindingFlags.Instance);
                                if (cameraShakeField != null)
                                {
                                    var cameraShake = (CameraShake)cameraShakeField.GetValue(elementalMagesPlayer);
                                    if (cameraShake != null) cameraShake.enabled = false;
                                }
                                else
                                {
                                    MelonLogger.Error(
                                        $"[{Name}] Could not find field cameraShake in BarnBrawlPlayer.");
                                }
                            }
                        }
    }
}