using System;
using System.Linq;
using System.Reflection;
using MelonLoader;
using SchummelPartie.render;
using SchummelPartie.setting.settings;
using UnityEngine;

namespace SchummelPartie.module.modules;

public class ModuleDaringDogfight : ModuleMinigame<PlanesController>
{
    public SettingSwitch GodMode;
    public SettingSwitch KillAll;
    public SettingSwitch BurstShot;
    public SettingSwitch ESP;

    public ModuleDaringDogfight() : base("Daring Dogfight", "God Mode, Kill All, Burst Shot, ESP.")
    {
        GodMode = new(Name, "God Mode", false);
        KillAll = new(Name, "Kill All", false);
        BurstShot = new(Name, "Burst Shot", false);
        ESP = new(Name, "Extra Sensory Perception", false);
    }

    public override void OnUpdate()
    {
        if (Enabled && ((bool) GodMode.GetValue() || (bool) KillAll.GetValue()))
        {
            if (GameManager.Minigame is PlanesController planesController)
            {
                foreach (var player in planesController.players)
                {
                    if (player is PlanesPlayer planesPlayer)
                    {
                        if (player.IsMe())
                        {
                            if ((bool) GodMode.GetValue())
                            {
                                planesPlayer.Health = 5;
                            }

                            if ((bool) KillAll.GetValue())
                            {
                                foreach (var enemy in planesController.players)
                                {
                                    if (enemy is PlanesPlayer planesEnemy && !enemy.IsDead && !enemy.IsMe())
                                    {
                                        planesController.TryDamagePlayer(planesPlayer, planesEnemy, enemy.GetPlayerPosition());
                                    }
                                }
                            }

                            if ((bool) BurstShot.GetValue())
                            {
                                typeof(PlanesPlayer).GetField("m_fireCooldown", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(planesPlayer, 0f);
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
                if (GameManager.Minigame is PlanesController planesController && GameManager.Minigame.Playable)
                {
                    var me = planesController.players.First(player => player.IsMe());
                    if (Camera.current != null && me.transform != null)
                    {
                        Vector3 mePos = Camera.current.WorldToScreenPoint(me.transform.position);
                        if (planesController.players is { Count: > 0 })
                        {
                            foreach (var player in planesController.players)
                            {
                                if (player is PlanesPlayer && player.transform != null)
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