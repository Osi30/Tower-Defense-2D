

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Skills
{
    public class SkillUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _thunderSkillText;
        [SerializeField]
        private TextMeshProUGUI _boomSkillText;
        [SerializeField]
        private GameObject _thunderSkillActivation;
        [SerializeField]
        private GameObject _boomSkillActivation;
        [SerializeField]
        private Image _thunderSkillLock;
        [SerializeField]
        private Image _boomSkillLock;
        [SerializeField]
        private float _coolDownSpeed;

        private Coroutine _thunderCouroutine;
        private Coroutine _boomCouroutine;

        private void Awake()
        {
            UpdateSkill(0, 0);
        }

        private void UpdateSkill(int thunderSkill, int boomSkill)
        {
            // Update Current Inventory
            var inventory = GameManager.Instance.UserData.inventory;
            inventory.thunderSkill += thunderSkill;
            inventory.boomSkill += boomSkill;

            // Update UI
            _thunderSkillText.text = inventory.thunderSkill.ToString();
            _boomSkillText.text = inventory.boomSkill.ToString();

            // Active if not 0
            if (inventory.thunderSkill != 0)
            {
                _thunderSkillActivation.SetActive(true);
                _thunderSkillLock.fillAmount = 0;
            }
            else
            {
                _thunderSkillActivation.SetActive(false);
                _thunderSkillLock.fillAmount = 1;
            }

            if (inventory.boomSkill != 0)
            {
                _boomSkillActivation.SetActive(true);
                _boomSkillLock.fillAmount = 0;
            }
            else
            {
                _boomSkillActivation.SetActive(false);
                _boomSkillLock.fillAmount = 1;
            }
        }

        public void LockThunderSkill()
        {
            UpdateSkill(-1, 0);

            if (GameManager.Instance.UserData.inventory.thunderSkill != 0)
                _thunderCouroutine = StartCoroutine(UnclockThunderAfter());
        }

        private IEnumerator UnclockThunderAfter()
        {
            _thunderSkillActivation.SetActive(false);
            _thunderSkillLock.fillAmount = 1;

            while (!(_thunderSkillLock.fillAmount <= 0))
            {
                _thunderSkillLock.fillAmount -= _coolDownSpeed;
                yield return null;
            }
            _thunderSkillActivation.SetActive(true);
        }

        public void LockBoomSkill()
        {
            UpdateSkill(0, -1);

            if (GameManager.Instance.UserData.inventory.boomSkill != 0)
                _boomCouroutine = StartCoroutine(UnclockBoomAfter());
        }

        private IEnumerator UnclockBoomAfter()
        {
            _boomSkillActivation.SetActive(false);
            _boomSkillLock.fillAmount = 1;

            while (!(_boomSkillLock.fillAmount <= 0))
            {
                _boomSkillLock.fillAmount -= _coolDownSpeed;
                yield return null;
            }
            _boomSkillActivation.SetActive(true);
        }

        private void OnDisable()
        {
            Dispose();
        }

        private void Dispose()
        {
            if (_thunderCouroutine != null) StopCoroutine(_thunderCouroutine);
            if (_boomCouroutine != null) StopCoroutine(_boomCouroutine);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}
