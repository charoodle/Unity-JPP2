using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    // Persistent data across scenes
    public string CurrentPlayerName;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
