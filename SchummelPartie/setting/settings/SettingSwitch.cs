using System;
using UrGUI.UWindow;

namespace SchummelPartie.setting.settings;

public class SettingSwitch : Setting<bool>
{
    public UWindowControls.WToggle toggle;

    public SettingSwitch(string container, string name, bool value = default, Action<bool> onChange = null) :
        base(container, name, value, onChange)
    {
    }

    public override void OnSettings(UWindow window)
    {
        base.OnSettings(window);
        toggle = window.Toggle(Name, value => SetValue(value), (bool)GetValue());
    }
}