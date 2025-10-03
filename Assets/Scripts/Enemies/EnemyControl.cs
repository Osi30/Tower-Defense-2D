using System.Collections;
using UnityEngine;

public class EnemyControl : BasePoolMember
{
    [SerializeField]
    private MapPatrolWayPoints _wayPoints;

    [SerializeField]
    private EnemyProperties _enemyProperties;

    [SerializeField]
    private BaseAnimationControl _animationControl;

    [SerializeField]
    private Health _health;

    private Vector2 _targetPosition;
    private int _currentPositionIndex = 0;
    private Coroutine _patrolCoroutine;

    private void Awake()
    {
        _health.OnDeathEvent += OnDeath;
    }

    private void OnDisable()
    {
        if (_patrolCoroutine != null)
            StopCoroutine(_patrolCoroutine);
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            // 1. Move toward point
            transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _enemyProperties.moveSpeed * Time.deltaTime);

            if (IsArriveTargetPosition())
            {
                if (!IsArriveEndPoition())
                {
                    // 2. Still not arrive to end position
                    UpdateTargetPosition();
                    UpdateMovementAnimation(GetDirectionToTarget().normalized);
                }
                else
                {
                    // 3. Arrive to end position
                    break;
                }
            }

            yield return null;
        }

        MarkAsInactive();
    }

    #region Pool Member Setup

    public void InitializeEnemy()
    {
        MarkAsActive();

        // Poistion
        UpdateTargetPosition();
        transform.position = _targetPosition;

        // Target Patrol Points
        UpdateTargetPosition();
        UpdateMovementAnimation(GetDirectionToTarget().normalized);

        // Start Patrol
        _patrolCoroutine = StartCoroutine(Patrol());
    }

    #endregion

    #region Patrol Way Points Movement

    public void UpdateTargetPosition()
    {
        _targetPosition = _wayPoints.GetPatrolPoint(_currentPositionIndex);
        _currentPositionIndex++;
    }

    public Vector2 GetDirectionToTarget()
    {
        return _targetPosition - new Vector2(transform.position.x, transform.position.y);
    }

    public bool IsArriveTargetPosition()
    {
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), _targetPosition);

        return distance < 0.01f;
    }

    public bool IsArriveEndPoition()
    {
        return _currentPositionIndex == _wayPoints.GetPatrolNumber();
    }

    public void UpdateMovementAnimation(Vector2 direction)
    {
        _animationControl.ActivateFloatFlag(AFloat.WalkX, direction.x);
        _animationControl.ActivateFloatFlag(AFloat.WalkY, direction.y);
    }

    #endregion

    #region Health

    public void OnDeath()
    {
        // Some animation before death

        // Deactive
        MarkAsInactive();

        // Add point


        // Add coin

    }

    #endregion
}
