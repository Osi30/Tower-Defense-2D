
using UnityEngine;

public class AttackerSignal : MonoBehaviour
{
    [SerializeField]
    private AttackerController _controller;

    private void Fire()
    {
        _controller.AttackTarget();
    }
}
