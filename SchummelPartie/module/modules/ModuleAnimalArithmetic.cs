using System.Collections.Generic;
using System.Reflection;
using MelonLoader;
using SchummelPartie.setting.settings;
using UnityEngine;
using UrGUI.UWindow;

namespace SchummelPartie.module.modules;

public class ModuleAnimalArithmetic : ModuleMinigame<CountingController>
{

    private SettingDropDown _mode;

    public ModuleAnimalArithmetic() : base("Animal Arithmetic", "Show the answer to the animal arithmetic.")
    {
        _mode = new(Name, "Mode", new Dictionary<int, string>
        {
            {0, "Show Answer"},
            {1, "Automatic Answer"},
            {2, "Both"}
        },  2);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Enabled && (int) _mode.GetValue() != 0)
        {
            if (GameManager.Minigame is CountingController { curState: CountingController.CountingMinigameState.DoingRound } countingController)
            {
                CountingPlayer me =
                    (CountingPlayer)countingController.players.Find(player =>
                        player is CountingPlayer && player.IsMe());
                if (me.IsMe() && me.guessCount.Value < countingController.curCorrectCount)
                {
                    var countingPlayerType = me.GetType();
                    var pressButtonMethodInfo =
                        countingPlayerType.GetMethod("PressButton",
                            BindingFlags.NonPublic | BindingFlags.Instance);
                    if (pressButtonMethodInfo != null)
                    {
                        pressButtonMethodInfo.Invoke(me, null);
                        MelonLogger.Msg(
                            $"[{Name}] Pressed button. [{me.guessCount.Value}/{countingController.curCorrectCount}]");
                    }
                    else
                    {
                        MelonLogger.Error(
                            $"[{Name}] Could not find method PressButton in CountingPlayer.");
                    }
                }
            }
        }
    }

    public override void OnGUI()
    {
        base.OnGUI();
        if (Enabled && (int) _mode.GetValue() != 1)
        {
            if (GameManager.Minigame is CountingController countingController)
            {
                GUI.Label(new Rect(10, 10, 100, 20), $"Answer: {countingController.curCorrectCount}", Style);
            }
        }
    }
}