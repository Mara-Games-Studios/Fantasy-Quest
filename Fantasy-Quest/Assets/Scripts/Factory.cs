using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Factory : MonoBehaviour
{
    private Transform catTransform;
    private StateMashineData data;
    private List<IMoveable> moveTypes;
    private List<IJumpable> jumpTypes;

    public void Initialize(Transform catTransform, StateMashineData data)
    {
        this.catTransform = catTransform;
        this.data = data;

        moveTypes = new List<IMoveable>() {
            new NoMove(catTransform,data) ,
            new Movement(catTransform, data),
            new OnlyRightMove(catTransform, data) ,
            new OnleLeftMove(catTransform, data)
        };

        jumpTypes = new List<IJumpable>()
        {
            new NoJump(catTransform,data),
            new UpJump(catTransform,data),
            new DownJump(catTransform,data)
        };
    }

    public IMoveable GetMoveType<MoveType>() where MoveType : IMoveable
    {
        return moveTypes.FirstOrDefault(m => m is MoveType);
    }

    public IJumpable GetJumpType<JumpType>() where JumpType : IJumpable
    {
        return jumpTypes.FirstOrDefault(m => m is JumpType);
    }
}
