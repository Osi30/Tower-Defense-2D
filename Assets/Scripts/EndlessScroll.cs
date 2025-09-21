using UnityEngine;

public class EndlessScroll : MonoBehaviour
{
    public float scrollSpeed = 2f; // tốc độ cuộn
    private float spriteWidth;     // chiều rộng của sprite
    private Vector3 startPos;

    void Start()
    {
        // Lấy chiều rộng sprite theo scale thực tế
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            spriteWidth = sr.bounds.size.x;
        }
        else
        {
            Debug.LogError("⚠️ Không tìm thấy SpriteRenderer trên " + gameObject.name);
        }

        startPos = transform.position;
    }

    void Update()
    {
        // Di chuyển sang trái
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Nếu sprite đi hết bên trái thì reset lại bên phải
        if (transform.position.x < startPos.x - spriteWidth)
        {
            transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);
        }
    }
}
