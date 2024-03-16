using System.Collections.Generic;
using HarmonyLib;
using MelonLoader;
using SchummelPartie.setting.settings;
using ZP.Net;

namespace SchummelPartie.module.modules;

public class ModuleForcePresent : Module
{
    public static ModuleForcePresent Instance { get; private set; }

    public SettingDropDown ActionID;

    public ModuleForcePresent() : base("Force Present", "Forces the present to be the one you want.")
    {
        Instance = this;
        ActionID = new(Name, "Action ID", new Dictionary<int, string>
        {
            {0, "Choose Minigame"},
            {1, "Teleport To Goal"},
            {2, "Swallow"}
        }, 1);
    }
}

[HarmonyPatch(typeof(PresentItem), "OpenPresent")]
public static class PresentItemPatch
{
    [HarmonyPrefix]
    internal static bool Prefix(PresentItem __instance, ref byte actionID)
    {
        if (ModuleForcePresent.Instance.Enabled)
        {
            MelonLogger.Msg(
                $"[{ModuleForcePresent.Instance.Name}] [Client] Force Present ID: {ModuleForcePresent.Instance.ActionID}");
            actionID = (byte) (int) ModuleForcePresent.Instance.ActionID.GetValue();
        }

        return true;
    }
}

[HarmonyPatch(typeof(NetBehaviour), "SendRPC")]
public static class NetBehaviourPatch
{
    [HarmonyPrefix]
    internal static bool Prefix(string method, NetRPCDelivery delivery, params object[] parameters)
    {
        if (ModuleForcePresent.Instance.Enabled && method == "RPCOpenPresent")
        {
            MelonLogger.Msg(
                $"[{ModuleForcePresent.Instance.Name}] [Server] Force Present ID: {ModuleForcePresent.Instance.ActionID}");
            parameters[0] = ModuleForcePresent.Instance.ActionID;
        }

        return true;
    }
}