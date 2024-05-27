using UnityEngine;
using UrGUI.UWindow;

namespace SchummelPartie.module.modules;

public class ModuleGUI : Module
{
    private readonly UWindow _window;

    public ModuleGUI() : base("Graphical User Interface", "Toggle the GUI with Insert or RightShift.")
    {
        Instance = this;
        _window = UWindow.Begin("Schummel Partie by Lyzev", 0, 0, 350, startHeight: 400, dynamicHeight: true);
        _window.SameLine();
        _window.Button("Discord", () => Application.OpenURL("https://lyzev.github.io/discord/"));
        _window.Button("GitHub", () => Application.OpenURL("https://github.com/Lyzev/SchummelPartie"));
        _window.IsDrawing = false;
    }

    public static ModuleGUI Instance { get; private set; }

    public void InitSettings()
    {
        ModuleManager.OnSettings(_window);
    }

    protected override void OnEnable()
    {
        _window.IsDrawing = true;
    }

    protected override void OnDisable()
    {
        _window.IsDrawing = false;
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Insert) || Input.GetKeyDown(KeyCode.RightShift)) Toggle();
    }
}