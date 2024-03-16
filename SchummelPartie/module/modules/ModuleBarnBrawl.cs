using System;
using System.Linq;
using System.Reflection;
using MelonLoader;
using SchummelPartie.render;
using UnityEngine;
using UrGUI.UWindow;

namespace SchummelPartie.module.modules;

public class ModuleBarnBrawl : ModuleMinigame<BarnBrawlController>
{
    private bool _godMode = false;
    private bool _infiniteShotgun = false;
    private bool _burstShotgun = false;
    private bool _noCameraShake = false;
    private bool _esp = true;

    public ModuleBarnBrawl() : base("Barn Brawl", "God Mode, Infinite Shotgun (Press F), Burst Shotgun, ESP.")
    {
    }

    public override void OnUpdate()
    {
        if (Enabled && (_godMode || _infiniteShotgun || _burstShotgun))
        {
            if (GameManager.Minigame is BarnBrawlController barnBrawlController)
            {
                foreach (var player in barnBrawlController.players)
                {
                    if (player is BarnBrawlPlayer barnBrawlPlayer)
                    {
                        if (player.IsMe())
                        {
                            if (_godMode)
                            {
                                barnBrawlPlayer.ApplyDamage(barnBrawlPlayer, -10, Vector3.zero, 0, 0, 0);
                            }

                            if (!barnBrawlPlayer.HoldingShotgun)
                            {
                                if (_infiniteShotgun || Input.GetKeyDown(KeyCode.F))
                                {
                                    barnBrawlPlayer.HoldingShotgun = true;
                                }
                            }

                            if (_burstShotgun)
                            {
                                barnBrawlPlayer.minProjectiles = 50;
                                barnBrawlPlayer.maxProjectiles = 60;
                            }
                            else
                            {
                                barnBrawlPlayer.minProjectiles = 7;
                                barnBrawlPlayer.maxProjectiles = 10;
                            }

                            if (_noCameraShake)
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
        if (Enabled && _esp)
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

    public override void OnSettings(UWindow window)
    {
        base.OnSettings(window);
        window.SameLine();
        window.Label("God Mode");
        window.Toggle("God Mode", godMode => { _godMode = godMode; }, _godMode);
        window.SameLine();
        window.Label("Infinite Shotgun");
        window.Toggle("Infinite Shotgun", infiniteShotgun => { _infiniteShotgun = infiniteShotgun; }, _infiniteShotgun);
        window.SameLine();
        window.Label("Burst Shotgun");
        window.Toggle("Burst Shotgun", burstShotgun => { _burstShotgun = burstShotgun; }, _burstShotgun);
        window.SameLine();
        window.Label("No Camera Shake");
        window.Toggle("No Camera Shake", noScreenShake => { _noCameraShake = noScreenShake; }, _noCameraShake);
        window.SameLine();
        window.Label("Extra Sensory Perception");
        window.Toggle("Extra Sensory Perception", esp => { _esp = esp; }, _esp);
    }
}