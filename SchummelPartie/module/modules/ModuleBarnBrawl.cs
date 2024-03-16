using System;
using System.Linq;
using System.Reflection;
using MelonLoader;
using SchummelPartie.render;
using SchummelPartie.setting.settings;
using UnityEngine;

namespace SchummelPartie.module.modules;

public class ModuleBarnBrawl : ModuleMinigame<BarnBrawlController>
{
    public SettingSwitch GodMode;
    public SettingSwitch InfiniteShotgun;
    public SettingSwitch BurstShotgun;
    public SettingSwitch NoCameraShake;
    public SettingSwitch ESP;

    public ModuleBarnBrawl() : base("Barn Brawl", "God Mode, Infinite Shotgun (Press F), Burst Shotgun, ESP.")
    {
        GodMode = new(Name, "God Mode", false);
        InfiniteShotgun = new(Name, "Infinite Shotgun", false);
        BurstShotgun = new(Name, "Burst Shotgun", false);
        NoCameraShake = new(Name, "No Camera Shake", false);
        ESP = new(Name, "Extra Sensory Perception", false);
    }

    public override void OnUpdate()
    {
        if (Enabled && ((bool) GodMode.GetValue() || (bool) InfiniteShotgun.GetValue() || (bool) BurstShotgun.GetValue()))
        {
            if (GameManager.Minigame is BarnBrawlController barnBrawlController)
            {
                foreach (var player in barnBrawlController.players)
                {
                    if (player is BarnBrawlPlayer barnBrawlPlayer)
                    {
                        if (player.IsMe())
                        {
                            if ((bool) GodMode.GetValue())
                            {
                                barnBrawlPlayer.ApplyDamage(barnBrawlPlayer, -10, Vector3.zero, 0, 0, 0);
                            }

                            if (!barnBrawlPlayer.HoldingShotgun)
                            {
                                if ((bool) InfiniteShotgun.GetValue() || Input.GetKeyDown(KeyCode.F))
                                {
                                    barnBrawlPlayer.HoldingShotgun = true;
                                }
                            }

                            if ((bool) BurstShotgun.GetValue())
                            {
                                barnBrawlPlayer.minProjectiles = 50;
                                barnBrawlPlayer.maxProjectiles = 60;
                            }
                            else
                            {
                                barnBrawlPlayer.minProjectiles = 7;
                                barnBrawlPlayer.maxProjectiles = 10;
                            }

                            if ((bool) NoCameraShake.GetValue())
                            {
                                FieldInfo cameraShakeField = barnBrawlPlayer.GetType().GetField("cameraShake",
                                    BindingFlags.NonPublic | BindingFlags.Instance);
                                if (cameraShakeField != null)
                                {
                                    CameraShake cameraShake = (CameraShake)cameraShakeField.GetValue(barnBrawlPlayer);
                                    if (cameraShake != null)
                                    {
                                        cameraShake.enabled = false;
                                    }
                                }
                                else
                                {
                                    MelonLoader.MelonLogger.Error(
                                        $"[{Name}] Could not find field cameraShake in BarnBrawlPlayer.");
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public override void OnGUI()
    {
        base.OnGUI();
        if (Enabled && (bool) ESP.GetValue())
        {
            try
            {
                if (GameManager.Minigame is BarnBrawlController barnBrawlController && GameManager.Minigame.Playable)
                {
                    var me = barnBrawlController.players.First(player => player.IsMe());
                    if (Camera.current != null)
                    {
                        Vector3 mePos = Camera.current.WorldToScreenPoint(me.transform.position);
                        if (barnBrawlController.players is { Count: > 0 })
                        {
                            foreach (var player in barnBrawlController.players)
                            {
                                if (player is BarnBrawlPlayer)
                                {
                                    if (!player.IsDead && (!player.IsOwner || player.GamePlayer.IsAI))
                                    {
                                        Vector3 playerPos =
                                            Camera.current.WorldToScreenPoint(player.transform.position);
                                        Render.DrawESP(playerPos, 50f, 100f, Color.red, me: mePos,
                                            name: $"[{player.GamePlayer.Name}]");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MelonLogger.Error(e.ToString());
            }
        }
    }
}