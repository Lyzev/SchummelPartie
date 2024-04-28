using System;
using UrGUI.UWindow;

namespace SchummelPartie.setting.settings;

public class SettingTextField : Setting<string>
{
    public SettingTextField(string container, string name, int maxLength, string value = default,
        Action<string> onChange = null) :
        base(container, name, value, onChange)
    {
        MaxLength = maxLength;
    }

    public int MaxLength { get; }

    public override void OnSettings(UWindow window)
    {
        base.OnSettings(window);
        window.TextField(Name, value => SetValue(value), (string)GetValue(), MaxLength);
    }
}