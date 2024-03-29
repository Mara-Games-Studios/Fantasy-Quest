﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    [AddComponentMenu("Utils/Utils.CapsuleColliderDrawer")]
    internal class CapsuleColliderDrawer : MonoBehaviour
    {
        [SerializeField]
        private CapsuleCollider2D capsuleCollider;

        [SerializeField]
        private Color color = Color.white;

        private void OnValidate()
        {
            capsuleCollider = GetComponent<CapsuleCollider2D>();
        }

        private void OnDrawGizmos()
        {
            Color tempColor = Handles.color;
            Handles.color = color;
            Handles.DrawWireCube(
                transform.position + (Vector3)capsuleCollider.offset,
                capsuleCollider.size
            );
            Handles.color = tempColor;
        }
    }
}
#endif
