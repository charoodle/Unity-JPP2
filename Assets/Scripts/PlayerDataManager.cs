using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton holding the player's
/// </summary>
public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    // Persistent data across scenes
    public PlayerData CurrentPlayer;

    // Persistent data across sessions
    public PlayerData HighScorePlayer;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable] 
    public class PlayerData
    {
        public string PlayerName;
        public int HighScore;
    }

    public void SaveData()
    {
        // Save current player name, and current score
    }

    /// <returns>True if new high score set and saved. False otherwise.</returns>
    public bool CurrentPlayerTrySubmitNewHighScore()
    {
        // Make sure high score is higher
        if (CurrentPlayer.HighScore <= HighScorePlayer.HighScore)
            return false;

        // Set it
        HighScorePlayer = CurrentPlayer;
        SaveHighScorePlayer();

        // New high score is set
        return true;
    }

    public void SaveHighScorePlayer()
    {

    }
}
