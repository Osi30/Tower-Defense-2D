
using UnityEngine;

public class TestApiConfig : MonoBehaviour
{
    public APIConfig _apiConfig;

    private string _decryptedBaseUrl;
    private string _decryptedLoginEndpoint;

    void Awake()
    {
        // Giải mã dữ liệu khi ứng dụng khởi chạy
        _decryptedBaseUrl = EncryptionData.Decrypt(_apiConfig.BaseUrl);
        _decryptedLoginEndpoint = EncryptionData.Decrypt(_apiConfig.LoginEndpoint);
    }

    public void Login()
    {
        string fullLoginUrl = _decryptedBaseUrl + _decryptedLoginEndpoint;
        Debug.Log("Full login URL: " + fullLoginUrl);
        // ... (sử dụng fullLoginUrl để gửi request)
    }
}
