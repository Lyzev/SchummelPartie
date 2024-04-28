using UrGUI.UWindow;

namespace SchummelPartie.setting;

public interface ISetting
{
    string Container { get; }
    string Name { get; }
    public object GetValue();
    public void SetValue(object value);
    public void OnSettings(UWindow window);
}