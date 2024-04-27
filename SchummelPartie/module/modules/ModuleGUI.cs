using UnityEngine;
using UrGUI.UWindow;

namespace SchummelPartie.module.modules;

public class ModuleGUI : Module
{
    public static ModuleGUI Instance { get; private set; }

    private readonly UWindow _window;

    public ModuleGUI() : base("Graphical User Interface", "Toggle the GUI with Insert or RightShift.")
    {
        Instance = this;
        _window = UWindow.Begin("Schummel Partie", startX: 0, startY: 0, startWidth: 350, startHeight: 400, dynamicHeight: true);
        _window.IsDrawing = false;
    }

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