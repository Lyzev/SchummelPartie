using System;
using MelonLoader;
using SchummelPartie.module;
using SchummelPartie.module.modules;
using SchummelPartie.setting;

namespace SchummelPartie;

public class Loader : MelonMod
{
    public static Loader Instance;

    public override void OnUpdate()
    {
        if (Instance == null)
        {
            Instance = this;
            var start = DateTime.Now.Ticks;
            MelonLogger.Msg("[Loader] Loading Schummel Partie...");
            MelonLogger.Msg("[Loader] Initializing ModuleManager...");
            ModuleManager.Init();
            MelonLogger.Msg("[Loader] Initialized ModuleManager.");
            MelonLogger.Msg("[Loader] Patching All");
            global::Harmony.HarmonyInstance.Create("Lyzev.Schummel_Partie").PatchAll();
            MelonLogger.Msg("[Loader] Patched All.");
            SettingManager.LoadSettings();
            AppDomain.CurrentDomain.ProcessExit += (_, _) => SettingManager.SaveSettings();
            ModuleGUI.Instance.InitSettings();
            ModuleManager.GetModules<Minigame>().ForEach(m =>
            {
                m.InitSettings();
                MelonLogger.Msg($"[Loader] Initialized {m.Name}.");
            });
            MelonLogger.Msg("[Loader] Loaded Settings.");
            MelonLogger.Msg("[Loader] Finished Initializing SchummelPartie in " + (DateTime.Now.Ticks - start) / TimeSpan.TicksPerMillisecond + "ms.");
        }
        ModuleManager.OnUpdate();
    }

    public override void OnGUI()
    {
        ModuleManager.OnGUI();
    }
}