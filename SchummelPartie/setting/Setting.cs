using System;
using UrGUI.UWindow;

namespace SchummelPartie.setting
{
    public abstract class Setting<T> : ISetting
    {
        public string Container { get; }
        public string Name { get; }
        public Action<T> OnChange { get; set; }

        private T _value;

        public Setting(string container, string name, T value = default, Action<T> onChange = null)
        {
            Container = container;
            Name = name;
            _value = value;
            OnChange = onChange ?? (_ => { });
            SettingManager.Settings.Add(this);
        }

        public object GetValue() => _value;

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
}