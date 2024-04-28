using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace SchummelPartie.module.modules;

public class ModulePresents : ModuleMinigame<PresentsController>
{
    public ModulePresents() : base("Presents", "Automatically collect the best presents.")
    {
        Instance = this;
    }

    public static ModulePresents Instance { get; private set; }
}

[HarmonyPatch(typeof(PresentsPlayer), "Update")]
public static class PresentsPlayerPatch
{
    [HarmonyPrefix]
    internal static bool Prefix(PresentsPlayer __instance)
    {
        if (ModulePresents.Instance.Enabled)
            if (GameManager.Minigame is PresentsController presentsController && GameManager.Minigame.Playable)
                if (__instance.IsMe())
                {
                    var m_nextGroupFieldInfo = AccessTools.Field(typeof(PresentsPlayer), "m_nextGroup");

                    if (presentsController.GetNextGroup() != null)
                    {
                        m_nextGroupFieldInfo.SetValue(__instance, presentsController.GetNextGroup());
                    }
                    else
                    {
                        var m_presentGroupsFieldInfo =
                            AccessTools.Field(typeof(PresentsController), "m_presentGroups");
                        var m_presentGroups = m_presentGroupsFieldInfo.GetValue(presentsController);
                        if (m_presentGroups != null)
                        {
                            var presentGroups = (List<PresentsGroup>)m_presentGroups;
                            if (presentGroups.Count > 0) m_nextGroupFieldInfo.SetValue(__instance, presentGroups[0]);
                        }
                    }

                    if (m_nextGroupFieldInfo.GetValue(__instance) != null)
                    {
                        var presentList =
                            ((PresentsGroup)m_nextGroupFieldInfo.GetValue(__instance))
                            .GetPresentList();
                        var playerPosition = __instance.transform.position;
                        var m_targetPosFieldInfo =
                            AccessTools.Field(typeof(PresentsPlayer), "m_targetPos");

                        var targetIndex = -1;
                        var coalDistance = float.MaxValue;
                        var presentDistance = float.MaxValue;
                        for (var i = 0; i < presentList.Count; i++)
                        {
                            if (presentList[i] == null || presentList[i].pfb == null) continue;

                            if (presentList[i].value >= 0 && presentDistance >
                                Vector3.Distance(playerPosition, presentList[i].pfb.transform.position))
                            {
                                targetIndex = i;
                                presentDistance = Vector3.Distance(playerPosition,
                                    presentList[i].pfb.transform.position);
                            }
                            else if (presentList[i].value < 0 &&
                                     coalDistance > Vector3.Distance(playerPosition,
                                         presentList[i].pfb.transform.position) && targetIndex == -1)
                            {
                                if (presentList[(int)m_targetPosFieldInfo.GetValue(__instance)] == null) return true;

                                targetIndex = presentList[1] == null || presentList[1].value >= 0
                                    ? 1
                                    : i == 2
                                        ? 0
                                        : 2;
                                coalDistance = Vector3.Distance(playerPosition,
                                    presentList[i].pfb.transform.position);
                            }
                        }

                        if (targetIndex == -1) targetIndex = 1;

                        if ((int)m_targetPosFieldInfo.GetValue(__instance) == targetIndex) return true;

                        m_targetPosFieldInfo.SetValue(__instance, targetIndex);
                        var m_moveFieldInfo = AccessTools.Field(typeof(PresentsPlayer), "m_move");
                        m_moveFieldInfo.SetValue(__instance, true);
                    }
                }

        return true;
    }
}