using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    [SerializeField] MainGameUIManager MainGameUIManager;
    public GameObject GameOverText;
    public GameObject NewHighScoreText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;


    
    // Start is called before the first frame update
    void Start()
    { 
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        MainGameUIManager.SetScoreText(m_Points);
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        // Test best score UI display without saving
        //PlayerDataManager.Instance.CurrentPlayer.HighScore = m_Points;
        //StartCoroutine(SetBestScoreTextCoroutine());
        //return;

        // Set current player's score on game end
        if (PlayerDataManager.Instance != null)
        {
            PlayerDataManager.Instance.CurrentPlayer.HighScore = m_Points;

            // Is it better than high score?
            if (PlayerDataManager.Instance != null)
            {
                bool newHighScore = PlayerDataManager.Instance.CurrentPlayerTrySubmitNewHighScore();
                if(newHighScore)
                    StartCoroutine(SetBestScoreTextCoroutine());
            }
        }
    }

    /// <summary>
    /// Update the UI in the game over screen to reflect current player setting a new high score.
    /// </summary>
    /// <returns></returns>
    IEnumerator SetBestScoreTextCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        // Display congratulatory message
        NewHighScoreText.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        // Set UI in game to update
        if (PlayerDataManager.Instance == null)
            yield break;
        PlayerDataManager.PlayerData player = PlayerDataManager.Instance.CurrentPlayer;
        MainGameUIManager.SetBestScoreText(player.PlayerName, player.HighScore);
    }
}
