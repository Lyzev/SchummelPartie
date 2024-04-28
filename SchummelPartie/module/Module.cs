using MelonLoader;
using SchummelPartie.module.modules;
using SchummelPartie.setting;
using SchummelPartie.setting.settings;
using UnityEngine;
using UrGUI.UWindow;

namespace SchummelPartie.module;

public abstract class Module
{
    public readonly string Description;
    public readonly string Name;
    protected readonly GUIStyle Style;
    public SettingSwitch EnabledSetting;

    protected Module(string name, string description)
    {
        Name = name;
        Description = description;
        Style = new GUIStyle();
        Style.fontSize = 30;
        Style.normal.textColor = Color.white;
        Style.fontStyle = FontStyle.Bold;
        EnabledSetting = new SettingSwitch(Name, "Enabled", true, isEnabled =>
        {
            if (isEnabled)
                OnEnable();
            else
                OnDisable();
        });
        MelonLogger.Msg($"[Module Manager] {Name} loaded.");
    }

    public bool Enabled => (bool)EnabledSetting.GetValue();

    public void Toggle()
    {
        EnabledSetting.SetValue(!Enabled);
        if (EnabledSetting.toggle != null)
            EnabledSetting.toggle.Value = Enabled;
        if (Enabled)
            OnEnable();
        else
            OnDisable();
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnGUI()
    {
    }

    public virtual void OnSettings(UWindow window)
    {
        window.Separator();
        window.Label($"<b>{Name}</b>");
        window.Label($"<i>{Description}</i>");
        SettingManager.Get(Name).ForEach(setting => setting.OnSettings(window));
    }
}

public abstract class Minigame : Module
{
    public readonly UWindow Window;

    protected Minigame(string name, string description) : base(name, description)
    {
        Window = UWindow.Begin(name, 0, 0, 360, startHeight: 400, dynamicHeight: true);
        Window.Label($"<i>{Description}</i>");
        Window.Separator();
        Window.IsDrawing = false;
    }

    public void InitSettings()
    {
        OnSettings(Window);
    }

    public override void OnSettings(UWindow window)
    {
        SettingManager.Get(Name).ForEach(setting => setting.OnSettings(window));
    }
}

public abstract class ModuleMinigame<T> : Minigame
{
    protected ModuleMinigame(string name, string description) : base(name, description)
    {
    }

    public override void OnGUI()
    {
        if (GameManager.Minigame != null && GameManager.Minigame is T)
            Window.IsDrawing = ModuleGUI.Instance.Enabled;
        else
            Window.IsDrawing = false;
    }
}