using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    public Vector3 GetFirstWaypoint()
    {
        return _waypoints.First().position;
    }

    public (Vector3 position, bool last) GetNextWaypoint(int lastIndex)
    {
        return (_waypoints[lastIndex + 1].position, lastIndex + 1 == _waypoints.Length - 1);
    }

    private void OnDrawGizmos()
    {
        if (_waypoints == null)
            return;

        for (int i = 1; i < _waypoints.Length; i++)
        {
            Gizmos.DrawLine(_waypoints[i].position, _waypoints[i - 1].position);
        }
    }
}


