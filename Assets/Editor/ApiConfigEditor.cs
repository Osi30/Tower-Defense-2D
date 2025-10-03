
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(APIConfig))]
public class ApiConfigEditor : Editor
{
    // Biến tạm để giữ giá trị ban đầu chưa mã hóa
    private string _rawBaseUrl;
    private string _rawLoginEndpoint;

    private APIConfig _apiConfig;

    private void OnEnable()
    {
        _apiConfig = (APIConfig)target;

        // Giải mã dữ liệu khi mở Inspector để dễ chỉnh sửa
        _rawBaseUrl = EncryptionData.Decrypt(_apiConfig.BaseUrl);
        _rawLoginEndpoint = EncryptionData.Decrypt(_apiConfig.LoginEndpoint);
    }

    public override void OnInspectorGUI()
    {
        // Hiển thị các trường nhập liệu
        _rawBaseUrl = EditorGUILayout.TextField("Base URL", _rawBaseUrl);
        _rawLoginEndpoint = EditorGUILayout.TextField("Login Endpoint", _rawLoginEndpoint);

        // Nút "Lưu và Mã hóa"
        if (GUILayout.Button("Save and Encrypt"))
        {
            // Mã hóa dữ liệu trước khi lưu vào asset
            string encryptedBaseUrl = EncryptionData.Encrypt(_rawBaseUrl);
            string encryptedLoginEndpoint = EncryptionData.Encrypt(_rawLoginEndpoint);

            // Cập nhật dữ liệu vào ScriptableObject
            _apiConfig.SetData(encryptedBaseUrl, encryptedLoginEndpoint);

            // Lưu thay đổi vào asset
            EditorUtility.SetDirty(_apiConfig);
            AssetDatabase.SaveAssets();

            //Debug.Log("API configuration has been encrypted and saved.");
        }
    }
}
