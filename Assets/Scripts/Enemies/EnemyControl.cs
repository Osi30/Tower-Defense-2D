using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField]
    private MapPatrolWayPoints _wayPoints;

    [SerializeField]
    private EnemyProperties _enemyProperties;

    [SerializeField]
    private BaseAnimationControl _animationControl;

    private Vector2 _targetPosition;
    private int _currentPositionIndex = 0;

    private void Awake()
    {
        UpdateTargetPosition();
        UpdateMovementAnimation(GetDirectionToTarget().normalized);
    }

    private void LateUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _enemyProperties.moveSpeed * Time.deltaTime);

        if (IsArriveTargetPosition())
        {
            if (!IsArriveEndPoition())
            {
                // Still not arrive to end position
                UpdateTargetPosition();
                UpdateMovementAnimation(GetDirectionToTarget().normalized);
            }
            else
            {
                // Arrive to end position
                gameObject.SetActive(false);
            }
        }
    }

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
}
