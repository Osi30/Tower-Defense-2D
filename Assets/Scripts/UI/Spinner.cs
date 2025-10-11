using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Spinner : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed = 1.0f;
        [SerializeField]
        private GameObject _spin;
        [SerializeField]
        private GameObject _spinPanel;

        private bool _isSpinning = false;
        private Coroutine _spinCoroutine;

        public void SetPanelStatus(bool isActive)
        {
            if (_spinPanel != null)
            {
                _spinPanel.SetActive(isActive);
            }
        }

        public void StopSpin()
        {
            _spin.SetActive(false);
            _isSpinning = false;
        }

        public void StartSpin()
        {
            _spin.SetActive(true);
            SetPanelStatus(true);
            _isSpinning = true;
            _spinCoroutine = StartCoroutine(SpinTheSpinner());
        }

        private IEnumerator SpinTheSpinner()
        {
            while (_isSpinning)
            {
                Spin();
                yield return null;
            }
            SetPanelStatus(false);
        }

        private void Spin()
        {
            _spin.transform.Rotate(0, 0, _rotationSpeed * -360f * Time.deltaTime, Space.Self);
        }

        private void OnDisable()
        {
            if (_spinCoroutine != null)
                StopCoroutine(_spinCoroutine);
        }
    }
}
