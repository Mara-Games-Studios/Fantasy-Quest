using System;
using UnityEngine;

[Serializable]
public class RunStateConfig
{
    [SerializeField]
    [Range(0, 10)]
    private float speed;

    public float Speed => speed;
}
