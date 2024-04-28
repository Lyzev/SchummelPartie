using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SchummelPartie.render;
using UnityEngine;

namespace SchummelPartie.module.modules;

public class ModuleMemoryMenu : ModuleMinigame<MemoryMenuController>
{
    public ModuleMemoryMenu() : base("Memory Menu", "Show the target food.")
    {
    }

    public override void OnGUI()
    {
        base.OnGUI();
        if (Enabled)
            if (GameManager.Minigame is MemoryMenuController memoryMenuController &&
                memoryMenuController.TargetFoods.Count > 0)
            {
                var targetPlayer = (MemoryMenuPlayer)memoryMenuController.players.First(player => player.IsMe());
                var w2sPlayer = Camera.current.WorldToScreenPoint(targetPlayer.transform.position);

                var m_foodIDs = (List<int>)typeof(MemoryMenuPlayer)
                    .GetField("m_foodIDs", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(targetPlayer);
                var shortList = new List<short>(memoryMenuController.TargetFoods);
                for (var index1 = m_foodIDs.Count - 1; index1 >= 0; --index1)
                for (var index2 = shortList.Count - 1; index2 >= 0; --index2)
                    if (m_foodIDs[index1] == shortList[index2])
                    {
                        shortList.RemoveAt(index2);
                        break;
                    }

                if (shortList.Count == 0)
                    GUI.Label(new Rect(0, 0, 100, 100), "All foods collected!");
                else
                    foreach (var item in memoryMenuController.m_itemManager.GetSpawnedItems())
                    foreach (var targetFood in shortList)
                        if (item.ItemTypeID == targetFood)
                        {
                            var w2sItem = Camera.current.WorldToScreenPoint(item.transform.position);
                            Render.DrawESP(w2sItem, 20f, 20f, Color.red, me: w2sPlayer);
                            break;
                        }
            }
    }
}