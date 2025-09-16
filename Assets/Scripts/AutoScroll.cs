using UnityEngine;

public class AutoScrollLoop : MonoBehaviour
{
    public float scrollSpeed = 2f;      // Tốc độ cuộn
    public float backgroundWidth = 20f; // Chiều rộng sprite (phải đúng bằng chiều rộng thực tế của background)

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Tính toán vị trí lặp vô tận
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, backgroundWidth);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}
