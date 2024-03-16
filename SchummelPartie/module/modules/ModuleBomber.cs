namespace SchummelPartie.module.modules;

public class ModuleBomber : ModuleMinigame<BomberController>
{
    public ModuleBomber() : base("Bomber", "Bombs are infinite.")
    {
    }

    public override void OnUpdate()
    {
        if (Enabled)
        {
            if (GameManager.Minigame is BattyBatterController bomberController)
            {
                foreach (var player in bomberController.players)
                {
                    if (player is BomberPlayer bomberPlayer)
                    {
                        if (player.IsMe())
                        {
                            bomberPlayer.bombRange = 1000;
                            bomberPlayer.BombsRemaining = 1000;
                            bomberPlayer.MaxBombs = 1000;
                        }
                    }
                }
            }
        }
    }
}
