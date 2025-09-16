using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private Material material; // Material của đối tượng
    private float distance;    // Khoảng cách đã di chuyển

    [SerializeField]
    [Range(0f, 0.5f)]
    private float speed = 0.2f; // Tốc độ di chuyển của hiệu ứng parallax

    private void Awake()
    {
        // Lấy Material từ Renderer
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("⚠️ Renderer not found on the GameObject.");
            return;
        }

        material = renderer.material;
        if (material == null)
        {
            Debug.LogError("⚠️ Material not found on the Renderer.");
        }
    }

    private void LateUpdate()
    {
        if (material == null) return;

        // Tính toán khoảng cách di chuyển dựa trên thời gian và tốc độ
        distance += Time.deltaTime * speed;

        // Cập nhật offset của texture để tạo hiệu ứng parallax
        material.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
}