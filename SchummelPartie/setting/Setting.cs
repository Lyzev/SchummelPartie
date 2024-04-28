using System;
using UrGUI.UWindow;

namespace SchummelPartie.setting;

public abstract class Setting<T> : ISetting
{
    private T _value;

    public Setting(string container, string name, T value = default, Action<T> onChange = null)
    {
        Container = container;
        Name = name;
        _value = value;
        OnChange = onChange ?? (_ => { });
        SettingManager.Settings.Add(this);
    }

    public Action<T> OnChange { get; set; }
    public string Container { get; }
    public string Name { get; }

    public object GetValue()
    {
        return _value;
    }

    public void SetValue(object value)
    {
        _value = (T)value;
        OnChange((T)value);
    }

    public virtual void OnSettings(UWindow window)
    {
        window.SameLine();
        window.Label(Name);
    }
}