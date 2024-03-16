using System.Linq;
using SchummelPartie.render;
using UnityEngine;

namespace SchummelPartie.module.modules;

public class ModuleTreasureHunt : ModuleMinigame<TreasureHuntController>
{
    public ModuleTreasureHunt() : base("Treasure Hunt", "Shows the path to the treasure.")
    {
    }

    public override void OnGUI()
    {
        base.OnGUI();
        if (Enabled)
        {
            if (GameManager.Minigame is TreasureHuntController treasureHuntController)
            {
                if (!treasureHuntController.introStarted && !treasureHuntController.introFinished) return;
                SphereCollider[] gems = Object.FindObjectsOfType<SphereCollider>();
                gems = gems.ToList().Where(gem => !gem.name.ToLower().Contains("cloth"))
                    .ToArray();

                GUI.Label(new Rect(0, 0, 100, 20), $"Gems: {gems.Length.ToString()}", Style);

                CharacterBase me = null;
                foreach (CharacterBase player in treasureHuntController.players)
                {
                    if (player.GamePlayer.IsLocalPlayer && !player.GamePlayer.IsAI)
                    {
                        me = player;
                        break;
                    }
                }

                if (me == null)
                    return;

                float distance = 55f;
                SphereCollider closest = null;

                foreach (SphereCollider gem in gems)
                {
                    if (gem.transform != null)
                    {
                        float tempDistance = Vector3.Distance(me.transform.position, gem.transform.position);

                        if (distance > tempDistance || closest == null)
                        {
                            distance = tempDistance;
                            closest = gem;
                        }

                        Render.DrawESP(Camera.current.WorldToScreenPoint(gem.transform.position), 40f, 40f, Color.white, name: "[Gem]");
                    }
                }

                Vector3 pw2s = Camera.current.WorldToScreenPoint(me.transform.position);
                if (closest != null)
                {
                    Render.DrawESP(Camera.current.WorldToScreenPoint(closest.transform.position), 40f, 40f, Color.blue, me: pw2s, name: "[Gem]");
                }

                if (treasureHuntController.treasure.interactable)
                {
                    Render.DrawESP(Camera.current.WorldToScreenPoint(treasureHuntController.treasure.transform.position), 40f, 40f, Color.yellow, me: pw2s, name: "[Treasure]");
                }
            }
        }
    }
}