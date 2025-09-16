using UnityEngine;
using TMPro;

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

    // --- Hiển thị các form ---
    public void ShowLoginForm()
    {
        choicePanel.SetActive(false);
        loginFormPanel.SetActive(true);
        registerFormPanel.SetActive(false);
    }

    public void ShowRegisterForm()
    {
        choicePanel.SetActive(false);
        loginFormPanel.SetActive(false);
        registerFormPanel.SetActive(true);
    }

    // 👉 Hàm quay lại Choice Panel
    public void BackToChoice()
    {
        choicePanel.SetActive(true);
        loginFormPanel.SetActive(false);
        registerFormPanel.SetActive(false);
    }

    // --- Submit ---
    public void SubmitLogin()
    {
        if (inputLoginUsername == null || inputLoginPassword == null)
        {
            Debug.LogError("Login input fields are not assigned in the inspector.");
            return;
        }

        string username = inputLoginUsername.text.Trim();
        string password = inputLoginPassword.text.Trim();

        if (IsValidCredentials(username, password))
        {
            Debug.Log($"Login success: {username}");
            PlayerPrefs.SetString("player_name", username);
            mainMenuManager.ShowMainMenuPanel();
        }
        else
        {
            Debug.Log("Login failed — please enter username & password");
        }
    }

    public void SubmitRegister()
    {
        if (inputRegisterUsername == null || inputRegisterPassword == null || inputRegisterConfirmPassword == null)
        {
            Debug.LogError("Register input fields are not assigned in the inspector.");
            return;
        }

        string username = inputRegisterUsername.text.Trim();
        string password = inputRegisterPassword.text.Trim();
        string confirmPassword = inputRegisterConfirmPassword.text.Trim();

        if (password != confirmPassword)
        {
            Debug.Log("Register failed — password and confirm password do not match.");
            return;
        }

        if (IsValidCredentials(username, password))
        {
            Debug.Log($"Register success: {username}");
            PlayerPrefs.SetString("player_name", username);
            mainMenuManager.ShowMainMenuPanel();
        }
        else
        {
            Debug.Log("Register failed — please enter username & password");
        }
    }

    private bool IsValidCredentials(string username, string password)
    {
        return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
    }
}
