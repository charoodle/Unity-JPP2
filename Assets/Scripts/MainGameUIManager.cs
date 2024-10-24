using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameUIManager : MonoBehaviour
{
    [SerializeField] Text _bestScoreText;
    [SerializeField] Text _gameScoreText;
    [SerializeField] private string _currentPlayerName;


    void Start()
    {
        // Set best score text
        //string playerName = PlayerDataManager.Instance.CurrentPlayerName;
        //int highScore = 0;
        //SetBestScoreText(playerName, highScore);

        if(PlayerDataManager.Instance != null)
        {
            // Get current player name - data persistence through scenes
            _currentPlayerName = PlayerDataManager.Instance.CurrentPlayer.PlayerName;
            SetScoreText(0);

            // Get current high score
            PlayerDataManager.PlayerData highScorePlayer = PlayerDataManager.Instance.HighScorePlayer;
            SetBestScoreText(highScorePlayer.PlayerName, highScorePlayer.HighScore);
        }
    }

    /// <summary>
    /// Set current game score UI
    /// </summary>
    /// <param name="score"></param>
    public void SetScoreText(int score)
    {
        // Process with a dash at end if not empty player name
        string currentPlayer = string.Empty;
        if(_currentPlayerName != string.Empty)
        {
            currentPlayer = $"{_currentPlayerName} -";
        }

        // Update UI with current game's score 
        _gameScoreText.text = $"{currentPlayer} Score : {score}";
    }

    public void SetBestScoreText(string playerName, int highScore)
    {
        _bestScoreText.text = $"Best Score : {playerName} : {highScore}";
    }
}
