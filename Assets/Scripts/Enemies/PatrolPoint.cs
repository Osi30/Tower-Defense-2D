using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField]
    private float _radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
