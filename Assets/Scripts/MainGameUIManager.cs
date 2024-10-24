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

        // Get current player name - data persistence through scenes
        if(PlayerDataManager.Instance != null)
        {
            _currentPlayerName = PlayerDataManager.Instance.CurrentPlayerName;
            SetScoreText(0);
        }
    }

    //void SetBestScoreText(string playerName, int highScore)
    //{
    //    _bestScoreText.text = $"Best Score : {playerName} : {highScore}";
    //}

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
}
