using System.Collections;
using UnityEngine;

public class Arrow : BasePoolMember
{
    [SerializeField]
    private float _fireSpeed = 5f;
    [SerializeField]
    private float _lifeTime = 2f;


    private Coroutine _fireCoroutine;

    private void OnDisable()
    {
        if (_fireCoroutine != null)
            StopCoroutine(_fireCoroutine);
    }

    public void InitializeArrow(Vector3 position, Vector3 direction)
    {
        // Poistion
        transform.position = position;

        // Rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void StartFire(Vector2 direction)
    {
        _fireCoroutine = StartCoroutine(Fire(direction));
    }

    private IEnumerator Fire(Vector2 direction)
    {
        float startTime = Time.time;
        while (true)
        {
            // Out of Life Time
            if (Time.time > startTime + _lifeTime)
            {
                break;
            }

            transform.position += _fireSpeed * Time.deltaTime * new Vector3(direction.x, direction.y);
            yield return null;
        }

        MarkAsInactive();
    }
}
