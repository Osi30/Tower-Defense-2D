
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class SkillControl : MonoBehaviour
    {
        [SerializeField]
        private Skill[] _skills;

        [SerializeField]
        private Transform _skillPrepare;

        [SerializeField]
        private SkillUI _skillUI;

        private int _chooseSkill = -1;
        private Camera _mainCamera;
        private bool _isAiming = false;
        private Coroutine _aimingCoroutine;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void ActivateSkill()
        {
            if (!_isAiming) return;

            _isAiming = false;

            if (_chooseSkill != -1)
            {
                _skillPrepare.gameObject.SetActive(false);
                _skills[_chooseSkill].gameObject.SetActive(true);
                _skills[_chooseSkill].ActivateSkill(_skillPrepare.position);
                if (_chooseSkill == 0) _skillUI.LockThunderSkill();
                else _skillUI.LockBoomSkill();
            }
        }

        public void ChooseSkill(int index)
        {
            _isAiming = true;
            _chooseSkill = index;
            Aim();
            _skillPrepare.gameObject.SetActive(true);
            _aimingCoroutine = StartCoroutine(Aiming());
        }

        private void Aim()
        {
            Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;
            _skillPrepare.position = mouseWorldPosition;
        }

        private IEnumerator Aiming()
        {
            while (_isAiming)
            {
                Aim();
                yield return null;
            }
        }

        private void OnDisable()
        {
            if (_aimingCoroutine != null)
                StopCoroutine(_aimingCoroutine);
        }

        private void OnDestroy()
        {
            if (_aimingCoroutine != null)
                StopCoroutine(_aimingCoroutine);
        }
    }
}
