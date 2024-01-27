using System;
using UnityEngine;

[Serializable]
public class AirConfig
{
    [SerializeField]
    private UpJumpConfig jumpUpStateConfig;

    [SerializeField]
    private DownJumpConfig jumpDownStateConfig;

    [SerializeField]
    [Range(0, 10)]
    private float speed;

    [SerializeField]
    [Range(0, 10)]
    private float xMoveResistance;

    public UpJumpConfig JumpUpStateConfig => jumpUpStateConfig;
    public DownJumpConfig JumpDownStateConfig => jumpDownStateConfig;
    public float Speed => speed;
    public float XMoveResistance => xMoveResistance;
    public float BaseGravity =>
        2f
        * jumpUpStateConfig.MaxJumpHeight
        / (jumpUpStateConfig.TimetoReachMaxHeight * jumpUpStateConfig.TimetoReachMaxHeight);
}
