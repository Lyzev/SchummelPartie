using SchummelPartie.render;
using UnityEngine;

namespace SchummelPartie.module.modules;

public class ModuleSelfishStride : ModuleMinigame<SelfishStrideController>
{
    public ModuleSelfishStride() : base("Selfish Stride", "Show the target bridge.")
    {
    }

    public override void OnGUI()
    {
        base.OnGUI();
        if (Enabled)
            if (GameManager.Minigame is SelfishStrideController selfishStrideController &&
                selfishStrideController.bridges != null)
                foreach (var player in selfishStrideController.players)
                    if (player is SelfishStridePlayer selfishStridePlayer)
                        if (!selfishStridePlayer.IsMe())
                        {
                            var targetBridge = GetTargetBridge(selfishStrideController, selfishStridePlayer);
                            if (targetBridge != null)
                            {
                                var w2sPlayer =
                                    Camera.current.WorldToScreenPoint(selfishStridePlayer.transform.position);
                                var w2sTargetBridge =
                                    Camera.current.WorldToScreenPoint(targetBridge.bridgeCollider.transform.position);
                                Render.DrawESP(w2sTargetBridge, 20f, 20f, Color.red, me: w2sPlayer);
                            }
                        }
    }

    private SelfishStrideBridge GetTargetBridge(SelfishStrideController selfishStrideController,
        SelfishStridePlayer selfishStridePlayer)
    {
        return selfishStridePlayer.target.Value >= selfishStrideController.bridges.Count
            ? null
            : selfishStrideController.bridges[selfishStridePlayer.target.Value];
    }
}