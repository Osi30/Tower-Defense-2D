using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.coinClip);
            }
            else
            {
                Debug.LogWarning("AudioManager.Instance bị null, hãy chắc chắn có AudioManager trong scene.");
            }

            Destroy(gameObject);
        }
    }
}
