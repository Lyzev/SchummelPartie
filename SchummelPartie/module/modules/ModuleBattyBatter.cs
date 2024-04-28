using System;
using System.Reflection;
using HarmonyLib;
using MelonLoader;

namespace SchummelPartie.module.modules;

public class ModuleBattyBatter : ModuleMinigame<BattyBatterController>
{
    public ModuleBattyBatter() : base("Batty Batter", "Automatically hit the ball.")
    {
        Instance = this;
    }

    public static ModuleBattyBatter Instance { get; private set; }
}

[HarmonyPatch(typeof(BattyBatterPlayer), "Update")]
public static class BattyBatterPlayerPatch
{
    [HarmonyPrefix]
    internal static bool Postfix(BattyBatterPlayer __instance)
    {
        try
        {
            if (GameManager.Minigame.Playable && __instance.IsMe() && __instance.canHit)
                if (((BattyBatterController)GameManager.Minigame).currentBalls[__instance.OwnerSlot].transform.position
                    .y <= ((BattyBatterController)GameManager.Minigame).ballHitPoints[__instance.OwnerSlot].y + .05f)
                {
                    var battyBatterPlayerType = __instance.GetType();
                    var localSwingMethodInfo =
                        battyBatterPlayerType.GetMethod("LocalSwing", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (localSwingMethodInfo != null)
                        localSwingMethodInfo.Invoke(__instance, null);
                    else
                        MelonLogger.Error(
                            $"[{ModuleBattyBatter.Instance.Name}] Could not find method LocalSwing in BattyBatterPlayer.");
                }
        }
        catch (Exception e)
        {
            MelonLogger.Error(e.ToString());
        }

        return true;
    }
}