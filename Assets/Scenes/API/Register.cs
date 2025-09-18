using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    [Header("UI References")]
    public InputField usernameInput;
    public InputField passwordInput;
    public Button registerButton;
    public Text messageText;

    [Header("API")]
    public string baseUrl = "http://localhost:5154"; //link API localhost

    private string registerEndpoint = "/api/Auth/register"; //endpoint register

    //Class GameInfo
    [Serializable]
    public class GameInfo
    {
        public int stars;
        public int currentLevel;
        public int thunderSkill;
        public int boomSkill;
        public string username;
    }

    //Class của AuthResponse về
    [Serializable]
    public class AuthResponse
    {
        public string id;
        public string username;
        public GameInfo gameInfo;
    }

    //Class Login request
    [Serializable]
    public class LoginRequest
    {
        public string username;
        public string password;
    }

    private void Start()
    {
        if (registerButton != null)
            registerButton.onClick.AddListener(OnRegisterClicked);
    }

    private void OnDestroy()
    {
        if (registerButton != null)
            registerButton.onClick.RemoveListener(OnRegisterClicked);
    }

    private void OnRegisterClicked()
    {
        string u = usernameInput != null ? usernameInput.text : "";
        string p = passwordInput != null ? passwordInput.text : "";

        if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(p))
        {
            SetMessage("Please enter username & password");
            return;
        }

        StartCoroutine(RegisterAction(u, p));
    }

    public IEnumerator RegisterAction(string username, string password)
    {
        var reqData = new LoginRequest { username = username, password = password };
        string jsonData = JsonUtility.ToJson(reqData);

        var uwr = new UnityWebRequest(baseUrl + registerEndpoint, "POST");
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);
        uwr.uploadHandler = new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError ||
            uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            SetMessage("Error: " + uwr.error);
        }
        else
        {
            string res = uwr.downloadHandler.text;
            Debug.Log("Response: " + res);

            // Parse JSON thành object
            AuthResponse data = JsonUtility.FromJson<AuthResponse>(res);

            if (data != null && !string.IsNullOrEmpty(data.id))
            {
                // Lưu thông tin 
                //PlayerPrefs.SetString("userId", data.id);
                //PlayerPrefs.SetString("username", data.username);
                //PlayerPrefs.Save();

                SetMessage($"User: {data.username}\n " +
                    $"Star: {data.gameInfo.stars}\n" +
                    $"Level: {data.gameInfo.currentLevel}\n" +
                    $"Thunder Skill: {data.gameInfo.thunderSkill}\n" +
                    $"Boom Skill: {data.gameInfo.boomSkill}\n");
            }
            else
            {
                SetMessage("Something Error");
            }
        }

    }

    //Message hiện lên log đã config
    private void SetMessage(string msg)
    {
        if (messageText != null)
            messageText.text = msg;

        Debug.Log("[Register] " + msg);
    }
}
