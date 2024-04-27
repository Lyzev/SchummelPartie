namespace SchummelPartie.module.modules;

public class ModuleSidestepSlope : ModuleMinigame<SidestepSlopeController>
{

    public ModuleSidestepSlope() : base("Sidestep Slope", "God Mode.")
    {
    }

    public override void OnUpdate()
    {
        if (Enabled)
        {
            if (GameManager.Minigame is SidestepSlopeController sidestepSlopeController)
            {
                foreach (var player in sidestepSlopeController.players)
                {
                    if (player is SidestepSlopePlayer sidestepSlopePlayer)
                    {
                        if (player.IsMe())
                        {
                            sidestepSlopePlayer.IsDead = false;
                        }
                    }
                }
            }
        }
    }
}