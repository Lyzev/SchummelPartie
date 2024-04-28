using System.Reflection;
using HarmonyLib;
using MelonLoader;

namespace SchummelPartie.module.modules;

public class ModuleMysteryMaze : ModuleMinigame<MysteryMazeController>
{
    public ModuleMysteryMaze() : base("Mystery Maze", "Shows the path to the exit.")
    {
        Instance = this;
    }

    public static ModuleMysteryMaze Instance { get; private set; }
}

[HarmonyPatch(typeof(MysteryMazeController), "Update")]
public static class MysteryMazeControllerPatch
{
    [HarmonyPrefix]
    internal static bool Prefix(MysteryMazeController __instance)
    {
        if (ModuleMysteryMaze.Instance.Enabled)
        {
            var mysteryMazeControllerType = __instance.GetType();
            var m_clipRadiusTargetFieldInfo = mysteryMazeControllerType.GetField("m_clipRadiusTarget",
                BindingFlags.NonPublic | BindingFlags.Instance);

            if (m_clipRadiusTargetFieldInfo != null)
                m_clipRadiusTargetFieldInfo.SetValue(__instance, 50f);
            else
                MelonLogger.Error(
                    $"[{ModuleMysteryMaze.Instance.Name}] Could not find field m_clipRadiusTarget in MysteryMazeController.");
        }

        return true;
    }
}