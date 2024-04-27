using System;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace SchummelPartie.module.modules;

public class ModuleSwiftShooters : ModuleMinigame<SwiftShootersController>
{
    public static ModuleSwiftShooters Instance { get; private set; }

    public ModuleSwiftShooters() : base("Swift Shooters", "Automatically shoot the good targets.")
    {
        Instance = this;
    }
}

[HarmonyPatch(typeof(SwiftShootersPlayer), "Update")]
public static class SwiftShootersPlayerPatch
{
    private static float _lastShootTime;
    private static SwiftShooterTarget _target = null;

    [HarmonyPrefix]
    internal static bool Prefix(SwiftShootersPlayer __instance)
    {
        if (ModuleSwiftShooters.Instance.Enabled)
        {
            if (__instance.IsMe())
            {
                Type swiftShootersPlayerType = __instance.GetType();
                FieldInfo m_spawnerFieldInfo =
                    swiftShootersPlayerType.GetField("m_spawner", BindingFlags.NonPublic | BindingFlags.Instance);
                SwiftShooterTargetSpawner
                    m_spawner = (SwiftShooterTargetSpawner)m_spawnerFieldInfo.GetValue(__instance);
                for (int i = 0; i < 3; i++)
                {
                    SwiftShooterTarget target = m_spawner.GetTarget(i);
                    if (target.GetTargetType() == TargetType.Good && target.IsTargetUp())
                    {
                        _target = target;
                        __instance.TargetPos = (byte)target.TargetIndex;
                        return true;
                    }
                }
                _target = null;
            }
        }
        return true;
    }

    [HarmonyPrefix]
    internal static bool Postfix(SwiftShootersPlayer __instance)
    {
        if (ModuleSwiftShooters.Instance.Enabled)
        {
            if (__instance.IsMe())
            {
                if (_target != null && _target.GetTargetType() == TargetType.Good && _target.IsTargetUp())
                {
                    if (Time.time - _lastShootTime <= 0.25f)
                        return true;
                    Type swiftShootersPlayerType = __instance.GetType();
                    FieldInfo m_gunMuzzleFieldInfo =
                        swiftShootersPlayerType.GetField("m_gunMuzzle", BindingFlags.NonPublic | BindingFlags.Instance);
                    Transform m_gunMuzzle = (Transform)m_gunMuzzleFieldInfo.GetValue(__instance);
                    int mask = LayerMask.GetMask("MinigameUtil1");
                    RaycastHit hitInfo;
                    if (!Physics.Raycast(m_gunMuzzle.position, m_gunMuzzle.forward, out hitInfo, 100f, mask, QueryTriggerInteraction.Ignore))
                        return true;
                    SwiftShooterTarget componentInParent = hitInfo.collider.gameObject.GetComponentInParent<SwiftShooterTarget>();
                    if (componentInParent != null)
                    {
                        MethodInfo fireMethodInfo =
                            swiftShootersPlayerType.GetMethod("Fire", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (fireMethodInfo != null)
                        {
                            _lastShootTime = Time.time;
                            fireMethodInfo.Invoke(__instance, null);
                        }
                        else
                        {
                            MelonLogger.Error(
                                $"[{ModuleSwiftShooters.Instance.Name}] Could not find method Fire in SwiftShootersPlayer.");
                        }
                    }
                }
            }
        }
        return true;
    }
}