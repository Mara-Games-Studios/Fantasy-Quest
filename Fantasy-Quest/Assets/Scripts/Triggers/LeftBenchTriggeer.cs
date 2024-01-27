using Cat;
using Cat.Strategies.Move;
using UnityEngine;

public class LeftBenchTriggeer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CatImpl>(out CatImpl cat))
        {
            cat.ChangeMovementType(new OnlyRight(cat.transform, cat.StateMachine.Data));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CatImpl>(out CatImpl cat))
        {
            cat.ChangeMovementType(new AnyWay(cat.transform, cat.StateMachine.Data));
        }
    }
}
