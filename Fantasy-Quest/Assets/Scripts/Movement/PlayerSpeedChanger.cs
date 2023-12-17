using UnityEngine;
using UnityEngine.AI;

public class PlayerSpeedChanger
{
    private NavMeshAgent _agent;
    private float _pathDistance;
    private AnimationCurve _speedChangeCurve;
    private float _baseSpeed;

    public PlayerSpeedChanger(NavMeshAgent agent, AnimationCurve speedChangeCurve, float baseSpeed)
    {
        _agent = agent;
        _speedChangeCurve = speedChangeCurve;
        _baseSpeed = baseSpeed;
    }

    public void ChangeSpeed(NavMeshPath currentPath)
    {
        float currentPathDistance = GetPathValue(currentPath);
        _agent.speed = _speedChangeCurve.Evaluate(1 - currentPathDistance / _pathDistance) * _baseSpeed;
    }

    public void CalculatePathDistance(NavMeshPath path)
    {
        _pathDistance = GetPathValue(path);
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
