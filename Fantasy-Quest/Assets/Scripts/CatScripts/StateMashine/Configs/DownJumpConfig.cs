using System;
using UnityEngine;

[Serializable]
public class DownJumpConfig 
{
    [SerializeField, Range(0, 10)] private float maxJumpHeight;
    [SerializeField, Range(0, 10)] private float timeToReachMaxHeight;
    [SerializeField, Range(0, 10)] private float xVelocity;

    public float MaxJumpHeight => maxJumpHeight;
    public float TimetoReachMaxHeight => timeToReachMaxHeight;
    public float StartYVelocity => 2 * maxJumpHeight / timeToReachMaxHeight;
    public float StartXVelocity => xVelocity;
}