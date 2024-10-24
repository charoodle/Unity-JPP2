using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour
{
    [SerializeField] TMP_InputField PlayerNameInputField;
    [SerializeField] TextMeshProUGUI CurrentHighScoreText;

    private void Start()
    {
        // Show current high score on main menu
        if(PlayerDataManager.Instance != null)
        {
            UpdateHighScoreText();
        }
    }

    public void StartGame()
    {
        // Save across scenes
        PlayerDataManager.Instance.CurrentPlayer.PlayerName = PlayerNameInputField.text;

        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void ClearLocalHighScore()
    {
        if(PlayerDataManager.Instance != null)
        {
            PlayerDataManager.Instance.ClearLocalHighScore();
            UpdateHighScoreText();
        }
    }

    /// <summary>
    /// Update the high score on the main menu with whatever's in PlayerDataManager.
    /// </summary>
    private void UpdateHighScoreText()
    {
        if (PlayerDataManager.Instance == null)
            return;

        PlayerDataManager.PlayerData currentBest = PlayerDataManager.Instance.HighScorePlayer;

        // Does a high score player score exist with > 0 score?
        if (currentBest.PlayerName != "" && currentBest.HighScore > 0)
            CurrentHighScoreText.text = $"Current Best: {currentBest.PlayerName} - {currentBest.HighScore}";
        else
            CurrentHighScoreText.text = "No high score set yet.";
    }
}
