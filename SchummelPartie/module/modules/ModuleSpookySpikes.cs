using System;
using System.Collections;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace SchummelPartie.module.modules;

public class ModuleSpookySpikes : ModuleMinigame<SpookySpikesController>
{
    public static ModuleSpookySpikes Instance { get; private set; }

    public ModuleSpookySpikes() : base("Spooky Spikes", "Automatically crouch or jump when needed.")
    {
        Instance = this;
    }
}

[HarmonyPatch(typeof(SpookySpikesPlayer), "OnTriggerEnter")]
public static class SpookySpikesPlayerPatch
{
    [HarmonyPrefix]
    internal static bool Postfix(SpookySpikesPlayer __instance, Collider other)
    {
        if (ModuleSpookySpikes.Instance.Enabled)
        {
            if (__instance.IsMe())
            {
                if (other.gameObject.name != "HitCollider" && other.gameObject.name != "ScoreCollider")
                {
                    Type spookySpikesPlayerType = __instance.GetType();
                    if (other.transform.parent.position.y > 0.75f)
                    {
                        MethodInfo crouchMethodInfo = spookySpikesPlayerType.GetMethod("Crouch", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (crouchMethodInfo != null)
                        {
                            __instance.StartCoroutine((IEnumerator) crouchMethodInfo.Invoke(__instance, null));
                        }
                        else
                        {
                            MelonLogger.Error(
                                $"[{ModuleSpookySpikes.Instance.Name}] Could not find method Crouch in SpookySpikesPlayer.");
                        }
                    }
                    else
                    {
                        MethodInfo jumpMethodInfo = spookySpikesPlayerType.GetMethod("Jump", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (jumpMethodInfo != null)
                        {
                            __instance.StartCoroutine((IEnumerator) jumpMethodInfo.Invoke(__instance, null));
                        }
                        else
                        {
                            MelonLogger.Error(
                                $"[{ModuleSpookySpikes.Instance.Name}] Could not find method Jump in SpookySpikesPlayer.");
                        }
                    }
                }
            }
        }
        return true;
    }
}