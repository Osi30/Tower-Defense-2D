using UnityEngine;

[CreateAssetMenu(fileName = "ApiConfig", menuName = "Data/Api")]
public class APIConfig : ScriptableObject
{
    [SerializeField] 
    private string _baseUrl;
    [SerializeField] 
    private string _getLevelByLevel;

    public string BaseUrl => _baseUrl;
    public string GetLevelByLevel => _baseUrl + _getLevelByLevel;

    public void SetData(string newBaseUrl, string getLvelByLevel)
    {
        _baseUrl = newBaseUrl;
        _getLevelByLevel = getLvelByLevel;
    }
}
