using System;
using System.Reflection;
using HarmonyLib;
using MelonLoader;

namespace SchummelPartie.module.modules;

public class ModuleRythm : ModuleMinigame<RhythmController>
{
    public static ModuleRythm Instance { get; private set; }

    public ModuleRythm() : base("Rockin Rythm", "Automatically hit the notes.")
    {
        Instance = this;
    }
}

[HarmonyPatch(typeof(RhythmPlayer), "Update")]
public static class RhythmPlayerPatch
{
    [HarmonyPrefix]
    internal static bool Prefix(RhythmPlayer __instance)
    {
        if (ModuleRythm.Instance.Enabled)
        {
            if (__instance.IsMe() && GameManager.Minigame.Playable)
            {
                Type rhythmPlayerPlayerType = __instance.GetType();
                FieldInfo m_panelFieldInfo =
                    rhythmPlayerPlayerType.GetField("m_panel", BindingFlags.NonPublic | BindingFlags.Instance);
                RhythmUIPanel rhythmUIPanel = (RhythmUIPanel)m_panelFieldInfo.GetValue(__instance);
                Array rhythmHitButtons = Enum.GetValues(typeof(RhythmHitButton));
                for (int i = 0; i < rhythmHitButtons.Length; i++)
                {
                    try
                    {
                        RhythmHitButton rhythmHitButton = (RhythmHitButton) rhythmHitButtons.GetValue(i);
                        RhythmHitResult rhythmHitResult = rhythmUIPanel.HitTrack(__instance.GamePlayer.GlobalID, rhythmHitButton, out int btnIndex, false);
                        if (rhythmHitResult == RhythmHitResult.Perfect)
                        {
                            MethodInfo hitButtonResultMethodInfo = rhythmPlayerPlayerType.GetMethod("HitButtonResult",
                                BindingFlags.NonPublic | BindingFlags.Instance);
                            if (hitButtonResultMethodInfo != null)
                            {
                                hitButtonResultMethodInfo.Invoke(__instance, new object[] { rhythmHitResult, btnIndex });
                            }
                            else
                            {
                                MelonLogger.Error(
                                    $"[{ModuleRythm.Instance.Name}] Could not find method HitButtonResult in RhythmPlayer.");
                            }

                            __instance.Combo += 1;
                        }
                    } catch (Exception e)
                    {
                        MelonLogger.Error(e.ToString());
                    }
                }
            }
        }

        return true;
    }
}