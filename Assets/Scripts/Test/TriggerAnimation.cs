using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField]
    private bool isTrigger = false;

    [SerializeField]
    private Arrow _arrow;

    private void Update()
    {
        if (isTrigger)
        {
            _arrow.StartFire(Vector2.left);
            isTrigger = false;
        }
    }
}
