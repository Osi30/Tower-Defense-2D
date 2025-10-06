using UnityEngine;
using TMPro;

public class LevelNodeWorld : MonoBehaviour
{
    [Header("Refs")]
    public SpriteRenderer baseSprite;
    public TextMeshPro label;
    public GameObject lockIcon;
    public SpriteRenderer[] starSprites;     // gán 3 sprite sao
    public Sprite starOn;
    public Sprite starOff;

    [Header("Data")]
    public int levelIndex = 1;
    public bool isUnlocked = false;
    public int starsEarned = 0;

    System.Action<int> onClicked;

    public void Setup(int index, bool unlocked, int stars, System.Action<int> onClick)
    {
        levelIndex = index;
        isUnlocked = unlocked;
        starsEarned = Mathf.Clamp(stars, 0, 3);
        onClicked = onClick;

        if (label) label.text = levelIndex.ToString();
        if (lockIcon) lockIcon.SetActive(!isUnlocked);

        if (starSprites != null)
            for (int i = 0; i < starSprites.Length; i++)
                starSprites[i].sprite = (i < starsEarned) ? starOn : starOff;
    }

    // đơn giản cho người mới: bắt click bằng OnMouseUpAsButton
    void OnMouseUpAsButton()
    {
        if (isUnlocked) onClicked?.Invoke(levelIndex);
    }
}
