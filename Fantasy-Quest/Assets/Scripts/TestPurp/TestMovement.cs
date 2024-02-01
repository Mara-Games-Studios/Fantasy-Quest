using UnityEngine;

namespace TestPurp
{
    [AddComponentMenu("Scripts/TestPurp/TestPurp.TestMovement")]
    internal class TestMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private float moveSpeed = 10.0f;

        private Vector2 moveDirection;

        private void Update()
        {
            InputPr();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void InputPr()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector2(moveX, moveY).normalized;
        }

        private void Move()
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }
    }
}
