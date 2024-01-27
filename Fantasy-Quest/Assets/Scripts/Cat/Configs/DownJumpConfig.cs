using System;
using UnityEngine;

[Serializable]
public class DownJumpConfig
{
    [SerializeField, Range(0, 5)]
    private float maxJumpHeight;

    [SerializeField, Range(0, 10)]
    private float timeToReachMaxHeight;

    [SerializeField, Range(0, 10)]
    private float xVelocity;

    [SerializeField, Range(0, 10)]
    private float xMoveResistance;

    public float MaxJumpHeight => maxJumpHeight;
    public float TimetoReachMaxHeight => timeToReachMaxHeight;
    public float StartYVelocity => 2 * maxJumpHeight / timeToReachMaxHeight;
    public float StartXVelocity => xVelocity;
    public float XMoveResistance => xMoveResistance;
    public float BaseGravity => 2f * maxJumpHeight / (timeToReachMaxHeight * timeToReachMaxHeight);
}
