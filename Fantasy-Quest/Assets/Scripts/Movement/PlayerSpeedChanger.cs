using UnityEngine;
using UnityEngine.AI;

public class PlayerSpeedChanger
{
    private NavMeshAgent agent;
    private float pathDistance;
    private AnimationCurve speedChangeCurve;
    private float baseSpeed;

    public PlayerSpeedChanger(NavMeshAgent agent, AnimationCurve speedChangeCurve, float baseSpeed)
    {
        this.agent = agent;
        this.speedChangeCurve = speedChangeCurve;
        this.baseSpeed = baseSpeed;
    }

    public void ChangeSpeed(NavMeshPath currentPath)
    {
        float currentPathDistance = GetPathValue(currentPath);
        agent.speed =
            speedChangeCurve.Evaluate(1 - (currentPathDistance / pathDistance)) * baseSpeed;
    }

    public void CalculatePathDistance(NavMeshPath path)
    {
        pathDistance = GetPathValue(path);
    }

    private float GetPathValue(NavMeshPath path)
    {
        float distance = 0;

        if (path != null)
        {
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                distance += Vector2.Distance(path.corners[i], path.corners[i + 1]);
            }
        }

        return distance;
    }
}
