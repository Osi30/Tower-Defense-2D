
using UnityEngine;

/// <summary>
/// Receive Signal From Animation
/// </summary>
public class EnemySignal : MonoBehaviour
{
    [SerializeField]
    private EnemyControl _control;

    private void OnDeathSignal()
    {
        _control.Death();
    }
}

