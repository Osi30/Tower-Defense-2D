
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

        public void ActivateSkill(Vector2 value)
        {
            if (!_isAiming) return;

            _isAiming = false;

            if (_chooseSkill != -1)
            {
                _skillPrepare.gameObject.SetActive(false);
                _skills[_chooseSkill].gameObject.SetActive(true);

                if (_chooseSkill == 0)
                {
                    AudioManager.Instance.PlaySFX("Thunder");
                    _skillUI.LockThunderSkill();
                }

                else
                {
                    AudioManager.Instance.PlaySFX("Boom");
                    _skillUI.LockBoomSkill();
                }

#if UNITY_ANDROID

                Vector3 targetPosition = _mainCamera.ScreenToWorldPoint(value);
                targetPosition.z = 0;
                _skills[_chooseSkill].ActivateSkill(targetPosition);
#else
                _skills[_chooseSkill].ActivateSkill( _skillPrepare.position);
#endif


            }
        }

        public void ChooseSkill(int index)
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            _isAiming = true;
            _chooseSkill = index;

#if !UNITY_ANDROID
            Aim();
            _skillPrepare.gameObject.SetActive(true);
            _aimingCoroutine = StartCoroutine(Aiming());
#endif
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
