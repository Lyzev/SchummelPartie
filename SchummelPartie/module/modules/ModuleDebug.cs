namespace SchummelPartie.module.modules;

public class ModuleDebug : Module
{
    public ModuleDebug() : base("Debug", "Toggle the Debug mode.")
    {
    }

    protected override void OnEnable()
    {
        GameManager.DEBUGGING = true;
    }

    protected override void OnDisable()
    {
        GameManager.DEBUGGING = false;
    }
}