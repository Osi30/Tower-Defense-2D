using System;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.LevelManagement.Dtos;
using Assets.Scripts.Security;
using Assets.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class AuthManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject choicePanel;
    public GameObject loginFormPanel;
    public GameObject registerFormPanel;

    [Header("Login Inputs")]
    public TMP_InputField inputLoginUsername;
    public TMP_InputField inputLoginPassword;

    [Header("Register Inputs")]
    public TMP_InputField inputRegisterUsername;
    public TMP_InputField inputRegisterPassword;
    public TMP_InputField inputRegisterConfirmPassword; // Thêm confirm password

    [Header("Managers")]
    public MainMenuManager mainMenuManager;

    [Header("Spinner")]
    [SerializeField]
    private Spinner spinner;

    private const string LoginEndpoint = "/api/Customer/login";   // endpoint login
    private const string RegisterEndpoint = "/api/Customer/register"; // endpoint register

    // ==== Models khớp JSON response bạn gửi ====
    [Serializable]
    private class Inventory
    {
        public int id;
        public int thunderSkill;
        public int boomSkill;
        public int upgradePoint;
        public int attackSpeed;
        public int damage;
        public int range;
    }

    [Serializable]
    private class GameProgress
    {
        public int id;
        public int currentCoin;
        public int currentHeart;
        public int currentPoint;
        public int? waveId;
    }

    [Serializable]
    private class AuthResponse
    {
        public int id;
        public string username;
        public int point;
        public Inventory inventory;
        public GameProgress gameProgress;
        public object[] resultLevels;
    }

    [Serializable]
    private class AuthRequest
    {
        public string username;
        public string password;
    }

    // --- Hiển thị các form ---
    public void ShowLoginForm()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        choicePanel.SetActive(false);
        loginFormPanel.SetActive(true);
        registerFormPanel.SetActive(false);
    }

    public void ShowRegisterForm()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        choicePanel.SetActive(false);
        loginFormPanel.SetActive(false);
        registerFormPanel.SetActive(true);
    }

    // 👉 Hàm quay lại Choice Panel
    public void BackToChoice()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        choicePanel.SetActive(true);
        loginFormPanel.SetActive(false);
        registerFormPanel.SetActive(false);
    }

    // --- Submit ---
    public void SubmitLogin()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (inputLoginUsername == null || inputLoginPassword == null)
        {
            Debug.LogError("Login input fields are not assigned in the inspector.");
            return;
        }

        string username = inputLoginUsername.text != null ? inputLoginUsername.text.Trim() : "";
        string password = inputLoginPassword.text != null ? inputLoginPassword.text.Trim() : "";

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.Log("[Login] Please enter username & password");
            return;
        }

        StartCoroutine(LoginAction(username, password));
    }

    public void SubmitRegister()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (inputRegisterUsername == null || inputRegisterPassword == null || inputRegisterConfirmPassword == null)
        {
            Debug.LogError("Register input fields are not assigned in the inspector.");
            return;
        }

        string username = inputRegisterUsername.text != null ? inputRegisterUsername.text.Trim() : "";
        string password = inputRegisterPassword.text != null ? inputRegisterPassword.text.Trim() : "";
        string confirmPassword = inputRegisterConfirmPassword.text != null ? inputRegisterConfirmPassword.text.Trim() : "";

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.Log("[Register] Please enter username & password");
            return;
        }

        if (password != confirmPassword)
        {
            Debug.Log("[Register] Password and confirm password do not match.");
            return;
        }

        StartCoroutine(RegisterAction(username, password));
    }

    // --- Coroutines gọi API ---
    private IEnumerator LoginAction(string username, string password)
    {
        spinner.StartSpin();

        var url = CombineUrl(BuildConstants.PRODUCTION_URL, LoginEndpoint)
                  + $"?username={UnityWebRequest.EscapeURL(username)}&password={UnityWebRequest.EscapeURL(password)}";

        var uwr = new UnityWebRequest(url, "POST")
        {
            downloadHandler = new DownloadHandlerBuffer()
        };
        uwr.SetRequestHeader("Accept", "application/json");

        yield return uwr.SendWebRequest();

        // Check success or fail
        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log($"[Login] Error: {uwr.responseCode} - {uwr.error}\nBody: {uwr.downloadHandler.text}");
            spinner.StopSpin();
            yield break;
        }

        SetUserData(uwr);
        spinner.StopSpin();
        LoadLevelMenuScene();
    }

    private void SetUserData(UnityWebRequest uwr)
    {
        // Convert data
        string jsonResponse = uwr.downloadHandler.text;
        try
        {
            UserData userData = JsonUtility.FromJson<UserData>(jsonResponse);
            GameManager.Instance.UserData = userData;
        }
        catch (Exception e)
        {
            Debug.LogError("Error when deserialization (LevelData): " + e.Message);
        }
    }


    private IEnumerator RegisterAction(string username, string password)
    {
        spinner.StartSpin();

        var url = CombineUrl(BuildConstants.PRODUCTION_URL, RegisterEndpoint)
                  + $"?username={UnityWebRequest.EscapeURL(username)}&password={UnityWebRequest.EscapeURL(password)}";

        var uwr = new UnityWebRequest(url, "POST")
        {
            downloadHandler = new DownloadHandlerBuffer()
        };
        uwr.SetRequestHeader("Accept", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log($"[Register] Error: {uwr.responseCode} - {uwr.error}\nBody: {uwr.downloadHandler.text}");
            spinner.StopSpin();
            yield break;
        }

        Debug.Log($"Register success: {username}");
        SetUserData(uwr);

        spinner.StopSpin();
        LoadLevelMenuScene();
    }

    private static string CombineUrl(string baseUrl, string endpoint)
    {
        if (string.IsNullOrEmpty(baseUrl)) return endpoint ?? "";
        if (string.IsNullOrEmpty(endpoint)) return baseUrl;
        return baseUrl.TrimEnd('/') + "/" + endpoint.TrimStart('/');
    }

    private bool IsValidCredentials(string username, string password)
    {
        return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
    }

    private void LoadLevelMenuScene()
    {
        SceneController.LoadScene(1);
    }
}
