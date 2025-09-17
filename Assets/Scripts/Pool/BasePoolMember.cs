
using UnityEngine;

public class BasePoolMember : MonoBehaviour, IBasePoolMember
{
    private bool isUsed = false;

    public bool IsActive => isUsed && gameObject.activeSelf;

    public void Initialize(Vector3 position)
    {
        transform.position = position;
    }

    public void MarkAsActive()
    {
        gameObject.SetActive(true);
        isUsed = true;
    }

    public void MarkAsInactive()
    {
        gameObject.SetActive(false);
        isUsed = false;
    }
}
