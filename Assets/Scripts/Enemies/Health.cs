using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _health = 10;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        
        if (_health <= 0)
        {
            // Inactive Member
        }
    }
}
