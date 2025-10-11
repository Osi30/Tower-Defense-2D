using Assets.Scripts.Skills;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    [SerializeField]
    private SkillControl _skillControl;

    private PlayerInput _playerInput;
    private InputAction _executeSkill;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _executeSkill = _playerInput.actions["ExecuteSkill"];
        _executeSkill.performed += ExecuteSkill;
    }

    private void ExecuteSkill(InputAction.CallbackContext _)
    {
        _skillControl.ActivateSkill();
    }


    private void OnEnable()
    {
        _executeSkill.Enable();
    }

    private void OnDisable()
    {
        _executeSkill.Disable();
    }
}
