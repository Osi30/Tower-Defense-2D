using UnityEngine;

[CreateAssetMenu(fileName = "ApiConfig", menuName = "Data/Api")]
public class APIConfig : ScriptableObject
{
    [SerializeField] 
    private string baseUrl;
    [SerializeField] 
    private string loginEndpoint;

    public string BaseUrl => baseUrl;
    public string LoginEndpoint => loginEndpoint;

    public void SetData(string newBaseUrl, string newLoginEndpoint)
    {
        baseUrl = newBaseUrl;
        loginEndpoint = newLoginEndpoint;
    }
}
