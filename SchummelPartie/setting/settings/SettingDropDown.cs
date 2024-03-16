using System;
using System.Collections.Generic;
using UrGUI.UWindow;

namespace SchummelPartie.setting.settings;

public class SettingDropDown : Setting<int>
{
    public Dictionary<int, string> Options { get; }

    public SettingDropDown(string container, string name, Dictionary<int, string> options, int value = default, Action<int> onChange = null) :
        base(container, name, value, onChange)
    {
        Options = options;
    }

    public override void OnSettings(UWindow window)
    {
        window.DropDown(Name, value => SetValue(value), (int)GetValue(), Options);
    }
}