using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth = 10;
    [SerializeField]
    private Image _healthFill;

    private float _currentHealth;

    public delegate void OnDeath();

    public OnDeath OnDeathEvent;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void ResetHealth()
    {
        _healthFill.fillAmount = 1f;
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _healthFill.fillAmount = 0;
            // Inactive Member
            OnDeathEvent.Invoke();
        }
        else
        {
            _healthFill.fillAmount = _currentHealth / _maxHealth;
        }
    }
}
