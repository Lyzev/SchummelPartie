using System;
using System.Reflection;
using HarmonyLib;
using MelonLoader;

namespace SchummelPartie.module.modules;

public class ModuleRythm : ModuleMinigame<RhythmController>
{
    public ModuleRythm() : base("Rockin Rythm", "Automatically hit the notes.")
    {
        Instance = this;
    }

    public static ModuleRythm Instance { get; private set; }
}

[HarmonyPatch(typeof(RhythmPlayer), "Update")]
public static class RhythmPlayerPatch
{
    [HarmonyPrefix]
    internal static bool Prefix(RhythmPlayer __instance)
    {
        if (ModuleRythm.Instance.Enabled)
            if (__instance.IsMe() && GameManager.Minigame.Playable)
            {
                RhythmUIPanel panel = UnityEngine.Object.FindObjectOfType<RhythmUIPanel>();
                var rhythmHitButtons = Enum.GetValues(typeof(RhythmHitButton));
                for (var i = 0; i < rhythmHitButtons.Length; i++)
                    try
                    {
                        var rhythmHitButton = (RhythmHitButton)rhythmHitButtons.GetValue(i);
                        var rhythmHitResult = panel.HitTrack(__instance.GamePlayer.GlobalID, rhythmHitButton,
                            out var btnIndex, false);
                        if (rhythmHitResult == RhythmHitResult.Perfect)
                        {
                            __instance.RPCHitButtonResult(null, (byte) rhythmHitResult, btnIndex);
                            __instance.Combo += 1;
                        }
                    }
                    catch (Exception e)
                    {
                        MelonLogger.Error(e.ToString());
                    }
            }
        return true;
    }
}