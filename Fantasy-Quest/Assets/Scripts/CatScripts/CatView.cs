using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CatView : MonoBehaviour
{
    //private const string IsIdle;
    //private const string IsRun;

    private Animator animator;

    public void Initialize() => animator = GetComponent<Animator>();

    //public void StartIdle() => animator.SetBool(IsIdle, true);
    //public void StopIdle() => animator.SetBool(IsIdle, false);
    //public void StartRun() => animator.SetBool(IsRun, true);
    //public void StopRun() => animator.SetBool(IsRun, false);
}
