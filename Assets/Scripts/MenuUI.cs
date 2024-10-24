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
        PlayerDataManager.Instance.ClearLocalHighScore();
    }
}
