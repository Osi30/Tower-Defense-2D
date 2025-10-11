using UnityEngine;
using TMPro;
using Assets.Scripts.UI;

public class LevelNodeWorld : MonoBehaviour
{
    [Header("Refs")]
    public TextMeshPro label;
    public GameObject lockIcon;
    public GameObject _unlockIcon;
    public GameObject _stars;
    public SpriteRenderer[] starSprites;
    public Sprite starOn;
    public Sprite starOff;

    [Header("Data")]
    [SerializeField]
    private int levelIndex = 1;

    [SerializeField]
    private Spinner _spinner;

    private bool isUnlocked = false;
    private int starsEarned = 0;

    private void Awake()
    {
        if (label) label.text = levelIndex.ToString();
    }

    public void Setup(bool unlocked, int stars)
    {
        isUnlocked = unlocked;
        starsEarned = Mathf.Clamp(stars, 0, 3);
        if (lockIcon && _stars)
        {
            lockIcon.SetActive(!isUnlocked);
            _stars.SetActive(isUnlocked);
            _unlockIcon.SetActive(isUnlocked);
        }

        if (starSprites != null)
            for (int i = 0; i < starSprites.Length; i++)
                starSprites[i].sprite = (i < starsEarned) ? starOn : starOff;
    }

    public void LoadGameScene()
    {
        _spinner.StartSpin();
        AudioManager.Instance.PlaySFX("ButtonClick");
        SceneController.LoadScene(levelIndex + 1);
        _spinner.StopSpin();
    }
}
