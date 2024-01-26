using UnityEngine;

public class LeftBenchTriggeer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cat>(out Cat cat))
        {
            cat.ChangeMovementType(new OnlyRightMove(cat.transform, cat.StateMashine.Data));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cat>(out Cat cat))
        {
            cat.ChangeMovementType(new Movement(cat.transform, cat.StateMashine.Data));
        }
    }
}
