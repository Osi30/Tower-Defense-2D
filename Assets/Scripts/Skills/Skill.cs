

using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class Skill : MonoBehaviour
    {
        [SerializeField]
        private BaseAnimationControl _control;

        [SerializeField]
        private ATrigger _triggerFlag;

        [SerializeField]
        private float _radius;

        [SerializeField] 
        private int _skillDamage;

        [SerializeField]
        private LayerMask _hitLayer;

        [SerializeField]
        private Transform _target;

        private Collider2D[] _hitColliders;

        public void ActivateSkill(Vector3 position)
        {
            transform.position = transform.position - _target.position + position;  
            _control.ActivateTriggerFlag(_triggerFlag);
        }

        public void SkillDamage()
        {
            _hitColliders = Physics2D.OverlapCircleAll(_target.position, _radius, _hitLayer);

            for (int i = 0; i < _hitColliders.Length; i++)
            {
                if (_hitColliders[i].TryGetComponent<Health>(out Health hit))
                {
                    hit.TakeDamage(_skillDamage);
                }
            }
        }

        public void SetActiveFalse()
        {
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_target.position, _radius);
        }
    }
}
