using UnityEngine;

/// <summary>
/// Enemies's patrol way point in specific map
/// </summary>

[CreateAssetMenu(fileName = "Map Patrol Points", menuName = "Data/PatrolPoints")]
public class MapPatrolWayPoints : ScriptableObject
{
    [SerializeField]
    private PatrolWayPoint[] _waypoints;

    public Vector2 GetPatrolPoint(int index)
    {
        return _waypoints[index].position + Random.insideUnitCircle * _waypoints[index].nearByRadius;
    }

    public int GetPatrolNumber()
    {
        return _waypoints.Length;
    }
}

[System.Serializable]
public class PatrolWayPoint
{
    public Vector2 position;
    public float nearByRadius;
}