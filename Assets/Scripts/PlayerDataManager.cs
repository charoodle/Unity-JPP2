using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Singleton holding the player's
/// </summary>
public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    private const string _saveDataFileName = "/savedata.json";

    // Persistent data across scenes
    public PlayerData CurrentPlayer;

    // Persistent data across sessions
    public PlayerData HighScorePlayer;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScorePlayer();
    }

    [System.Serializable] 
    public class PlayerData
    {
        public string PlayerName = "";
        public int HighScore = 0;

        public void SetFrom(PlayerData data)
        {
            PlayerName = data.PlayerName;
            HighScore = data.HighScore;
        }
    }

    /// <returns>True if new high score set and saved. False otherwise.</returns>
    public bool CurrentPlayerTrySubmitNewHighScore()
    {
        // Make sure high score is higher
        if (CurrentPlayer.HighScore <= HighScorePlayer.HighScore)
        {
            return false;
        }
        else
        {
            // Set it - Serializable is a REFERENCE TYPE, not a value type
            //HighScorePlayer = CurrentPlayer;      // Cannot do this; otherwise if set a high score, HighScore references CurrentPlayer, so any subsequent scores (high score or not) will have an inaccurate HighScore object
                                                    // so that's why in inspector, even tho no high score code was being run/hit, they still were updating to the same values since HighScorePlayer = CurrentPlayer reference
            HighScorePlayer.SetFrom(CurrentPlayer);
            SaveHighScorePlayer();

            // New high score is set
            return true;
        }
    }

    public void SaveHighScorePlayer()
    {
        // Get as JSON
        string json = JsonUtility.ToJson(HighScorePlayer);

        // Write to path
        string path = Application.persistentDataPath + _saveDataFileName;

        // Write data to that path
        File.WriteAllText(path, json);
    }

    public void LoadHighScorePlayer()
    {
        // Check if file exists
        string path = Application.persistentDataPath + _saveDataFileName;

        if(File.Exists(path))
        {
            // Get json from file (assuming it is json)
            string json = File.ReadAllText(path);

            // Read json into player object
            HighScorePlayer = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            HighScorePlayer = new PlayerData();
        }
    }

    public void ClearLocalHighScore()
    {
        // Check if file exists
        string path = Application.persistentDataPath + _saveDataFileName;

        if (File.Exists(path))
        {
            // Clear out file
            File.WriteAllText(path, "");
        }

        // Clear out memory of high score too
        HighScorePlayer = new PlayerData();
    }
}
