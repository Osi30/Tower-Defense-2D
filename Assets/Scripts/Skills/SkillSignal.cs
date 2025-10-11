
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class SkillSignal : MonoBehaviour
    {
        [SerializeField]
        private Skill _skill;

        private void Explosion()
        {
            _skill.SkillDamage();
        }

        private void Hide()
        {
            _skill.SetActiveFalse();
        }
    }
}
