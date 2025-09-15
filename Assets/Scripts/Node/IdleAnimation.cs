using UnityEngine;

public class IdleAnimation : MonoBehaviour
{
    [SerializeField]
    private float _idleValue;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat(AFloat.IdleF.ToString(), _idleValue);
    }
}
