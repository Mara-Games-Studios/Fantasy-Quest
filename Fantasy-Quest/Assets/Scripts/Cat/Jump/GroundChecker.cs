using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask ground;

    [SerializeField, Range(0.01f, 1)] private float distanceToCheckGround;

    public bool IsTouch { get; private set; }

    private void FixedUpdate()
    {
        IsTouch = Physics2D.OverlapCircle(transform.position, distanceToCheckGround, ground);
    }
}
