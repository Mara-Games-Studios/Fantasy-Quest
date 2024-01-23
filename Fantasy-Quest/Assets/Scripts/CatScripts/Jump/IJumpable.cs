using UnityEngine;

public interface IJumpable 
{
    void Jump(StateMashineData data, float xStartVelocity, float yStartVelocity);

    void Update();
}
