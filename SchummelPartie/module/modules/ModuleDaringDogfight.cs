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
    public SettingSwitch BurstShot;
    public SettingSwitch GodMode;
    public SettingSwitch KillAll;

    public ModuleDaringDogfight() : base("Daring Dogfight", "God Mode, Kill All, Burst Shot, ESP.")
    {
        GodMode = new SettingSwitch(Name, "God Mode");
        KillAll = new SettingSwitch(Name, "Kill All");
        BurstShot = new SettingSwitch(Name, "Burst Shot");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Enabled && ((bool)GodMode.GetValue() || (bool)BurstShot.GetValue() || (bool)KillAll.GetValue()))
            if (GameManager.Minigame is PlanesController planesController)
                foreach (var player in planesController.players)
                    if (player is PlanesPlayer planesPlayer)
                        if (player.IsMe())
                        {
                            if ((bool)GodMode.GetValue()) planesPlayer.Health = 5;

                            if ((bool)KillAll.GetValue())
                                foreach (var enemy in planesController.players)
                                    if (enemy is PlanesPlayer planesEnemy && !enemy.IsDead && !enemy.IsMe())
                                        planesController.TryDamagePlayer(planesPlayer, planesEnemy,
                                            enemy.GetPlayerPosition());

                            if ((bool)BurstShot.GetValue())
                                typeof(PlanesPlayer)
                                    .GetField("m_fireCooldown", BindingFlags.NonPublic | BindingFlags.Instance)
                                    .SetValue(planesPlayer, 0f);
                        }
    }
}