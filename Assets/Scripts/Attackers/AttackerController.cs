using System.Collections;
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
    private ArrowPool _arrowPool;
    private bool _isCooldown;

    private void Awake()
    {
        _arrowPool = GameObject.FindGameObjectWithTag("ArrowPool").GetComponent<ArrowPool>();
    }

    private void Update()
    {
        // Detect Target
        if (_target == null)
        {
            FindTarget();
        }
        // Attack Target
        else
        {
            if (!_isCooldown)
            {
                _animationControl.ActivateTriggerFlag(ATrigger.Attack);
                StartCoroutine(StartCoolDown());
                _isCooldown = true;
            }

            UpdateMovementAnimation(GetDirectionToTarget().normalized);
            if (!IsTargetInRange()) _target = null;
        }
    }

    #region Attack Target

    public void AttackTarget()
    {
        if (_target == null) return;

        // Get Arrow and Fire Target
        Vector2 direction = GetDirectionToTarget().normalized;
        Arrow arrow = _arrowPool.GetOneActiveArrow();
        arrow.InitializeArrow(transform.position, direction);
        arrow.StartFire(direction);
    }

    private IEnumerator StartCoolDown()
    {
        yield return new WaitForSeconds(_attackCoolDownTime);
        _isCooldown = false;
    }

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
