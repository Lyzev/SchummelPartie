using System;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace SchummelPartie.module.modules;

public class ModulePackAndPile : ModuleMinigame<PackAndPileController>
{
    public static ModulePackAndPile Instance { get; private set; }

    public ModulePackAndPile() : base("Pack And Pile", "Automatically place boxes.")
    {
        Instance = this;
    }
}

[HarmonyPatch(typeof(PackAndPilePlayer), "Update")]
public static class PackAndPilePlayerPatch
{
    [HarmonyPrefix]
    internal static bool Postfix(PackAndPilePlayer __instance)
    {
        if (ModulePackAndPile.Instance.Enabled)
        {
            if (GameManager.Minigame.Playable && GameManager.Minigame is PackAndPileController && __instance.IsMe() && !__instance.finished)
            {
                Type packAndPilePlayerType = __instance.GetType();
                FieldInfo lastDropFieldInfo =
                    packAndPilePlayerType.GetField("lastDrop", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo dropWaitTimeFieldInfo =
                    packAndPilePlayerType.GetField("dropWaitTime", BindingFlags.NonPublic | BindingFlags.Instance);
                if (Time.time - (float)lastDropFieldInfo.GetValue(__instance) >= (float)dropWaitTimeFieldInfo.GetValue(__instance))
                {
                    FieldInfo curXFieldInfo =
                        packAndPilePlayerType.GetField("curX", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (curXFieldInfo != null)
                    {
                        int curX = (int)curXFieldInfo.GetValue(__instance);
                        if (curX == 3)
                        {
                            MethodInfo placeBoxLocalMethodInfo = packAndPilePlayerType.GetMethod("PlaceBoxLocal",
                                BindingFlags.NonPublic | BindingFlags.Instance);
                            if (placeBoxLocalMethodInfo != null)
                            {
                                placeBoxLocalMethodInfo.Invoke(__instance, null);
                            }
                            else
                            {
                                MelonLogger.Error(
                                    $"[{ModulePackAndPile.Instance.Name}] Could not find method PlaceBoxLocal in PackAndPilePlayer.");
                            }
                        }
                    }
                    else
                    {
                        MelonLogger.Error(
                            $"[{ModulePackAndPile.Instance.Name}] Could not find field curX in PackAndPilePlayer.");
                    }
                }
            }
        }

        return true;
    }
}