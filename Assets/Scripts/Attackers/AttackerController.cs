using UnityEditor;
using UnityEngine;

public class AttackerController : MonoBehaviour
{
    [SerializeField]
    private float _attackRadius;
    [SerializeField]
    private float _attackCoolDownTime;
    [SerializeField]
    private LayerMask _enemyMask;
    [SerializeField]
    private BaseAnimationControl _animationControl;

    private Transform _target;

    private void Update()
    {
        if (_target == null)
        {
            FindTarget();
        }
        else
        {
            UpdateMovementAnimation(GetDirectionToTarget().normalized);
            if (!IsTargetInRange()) _target = null;
        }
    }

    #region Attack Target

    private Vector2 GetDirectionToTarget()
    {
        return _target.position - transform.position;
    }

    private void UpdateMovementAnimation(Vector2 direction)
    {
        _animationControl.ActivateFloatFlag(AFloat.DirX, direction.x);
        _animationControl.ActivateFloatFlag(AFloat.DirY, direction.y);
    }

    #endregion

    #region Detect Target

    private bool IsTargetInRange()
    {
        return Vector2.Distance(_target.position, transform.position) <= _attackRadius;
    }


    private void FindTarget()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, _attackRadius, (Vector2)transform.position, 0f, _enemyMask);

        // Something Hit Attack Range
        if (hit.collider != null) _target = hit.collider.transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }

    #endregion
}
