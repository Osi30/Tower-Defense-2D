using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField]
    private bool isTrigger = false;
    [SerializeField]
    private BaseAnimationControl control;

    private void Update()
    {
        if (isTrigger)
        {
            control.ActivateTriggerFlag(ATrigger.Attack);
            isTrigger = false;
        }
    }
}
