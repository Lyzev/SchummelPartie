using System;
using UrGUI.UWindow;

namespace SchummelPartie.setting.settings;

public class SettingSlider : Setting<float>
{
    public SettingSlider(string container, string name, float min, float max, float value = default,
        Action<float> onChange = null) :
        base(container, name, value, onChange)
    {
        Min = min;
        Max = max;
    }

    public float Min { get; }
    public float Max { get; }

    public override void OnSettings(UWindow window)
    {
        base.OnSettings(window);
        window.Slider(Name, value => SetValue(value), (float)GetValue(), Min, Max);
    }
}