using UnityEngine;
using TMPro;

public class LevelNodeWorld : MonoBehaviour
{
    [Header("Refs")]
    public SpriteRenderer baseSprite;
    public TextMeshPro label;
    public GameObject lockIcon;
    public SpriteRenderer[] starSprites;
    public Sprite starOn;
    public Sprite starOff;

    [Header("Data")]
    public int levelIndex = 1;
    public bool isUnlocked = false;
    public int starsEarned = 0;

    public void Setup(int index, bool unlocked, int stars)
    {
        levelIndex = index;
        isUnlocked = unlocked;
        starsEarned = Mathf.Clamp(stars, 0, 3);

        if (label) label.text = levelIndex.ToString();
        if (lockIcon) lockIcon.SetActive(!isUnlocked);

        if (starSprites != null)
            for (int i = 0; i < starSprites.Length; i++)
                starSprites[i].sprite = (i < starsEarned) ? starOn : starOff;
    }
}
