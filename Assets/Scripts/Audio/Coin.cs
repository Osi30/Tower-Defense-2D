using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private string sfxKey = "coin"; // key trong AudioLibrary

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(sfxKey);
        }
        else
        {
            Debug.LogWarning("AudioManager.Instance is null. Hãy chắc chắn có AudioManager trong scene.");
        }

        Destroy(gameObject);
    }
}
