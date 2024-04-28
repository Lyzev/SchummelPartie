using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using SchummelPartie.setting.settings;
using ZP.Net;

namespace SchummelPartie.module.modules;

public class ModuleForcePresent : Module
{
    public SettingDropDown ActionID;

    public ModuleForcePresent() : base("Force Present", "Forces the present action. (NOT TESTED)")
    {
        Instance = this;
        ActionID = new SettingDropDown(Name, "Action ID", new Dictionary<int, string>
        {
            { 0, "Choose Minigame" },
            { 1, "Teleport To Goal" },
            { 2, "Swallow" }
        }, 1);
    }

    public static ModuleForcePresent Instance { get; private set; }
}

[HarmonyPatch(typeof(PresentItem), "OpenPresent")]
public static class PresentItemPatch
{
    [HarmonyPrefix]
    internal static bool Prefix(PresentItem __instance, ref byte actionID)
    {
        var player = (GamePlayer)typeof(PresentItem).GetField("player", BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(__instance);
        if (player.BoardObject.IsOwner && !player.IsAI)
            if (ModuleForcePresent.Instance.Enabled)
            {
                MelonLogger.Msg(
                    $"[{ModuleForcePresent.Instance.Name}] [Client] Force Present ID: {ModuleForcePresent.Instance.ActionID.GetValue()}");
                actionID = (byte)(int)ModuleForcePresent.Instance.ActionID.GetValue();
            }

        return true;
    }
}

[HarmonyPatch(typeof(NetBehaviour), "SendRPC")]
public static class NetBehaviourPatch
{
    [HarmonyPrefix]
    internal static bool Prefix(NetBehaviour __instance, string method, NetRPCDelivery delivery,
        params object[] parameters)
    {
        if (__instance.IsOwner)
            if (ModuleForcePresent.Instance.Enabled && method == "RPCOpenPresent")
            {
                MelonLogger.Msg(
                    $"[{ModuleForcePresent.Instance.Name}] [Server] Force Present ID: {ModuleForcePresent.Instance.ActionID.GetValue()}");
                parameters[0] = (byte)(int)ModuleForcePresent.Instance.ActionID.GetValue();
            }

        return true;
    }
}