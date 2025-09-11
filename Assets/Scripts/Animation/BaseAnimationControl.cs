using UnityEngine;

public class BaseAnimationControl : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    public void ActivateBoolFlag(ABool flag, bool isActivate)
    {
        _animator.SetBool(flag.ToString(), isActivate);
    }

    public void ActivateTriggerFlag(ATrigger flag)
    {
        _animator.SetTrigger(flag.ToString());
    }

    public void ActivateFloatFlag(AFloat flag, float value)
    {
        _animator.SetFloat(flag.ToString(), value);
    }

    public void ActivateMultiplierFlag(AMultiplier flag, float value)
    {
        _animator.SetFloat(flag.ToString(), value);
    }

    public void ActivateCrossFadeState(AState state, float transitionDuration)
    {
        _animator.CrossFadeInFixedTime(state.ToString(), transitionDuration);
    }
}
