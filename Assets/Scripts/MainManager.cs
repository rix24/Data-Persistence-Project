using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points} : {GameManager.Instance.playerName}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        if (GameManager.Instance.highScores.Count == 0)
        {
            GameManager.Instance.highScores.Add(new HighScoreData(m_Points, GameManager.Instance.playerName));
        }
        else if (GameManager.Instance.highScores.Count < 10)
        {
            GameManager.Instance.highScores.Add(new HighScoreData(m_Points, GameManager.Instance.playerName));
            GameManager.Instance.highScores = GameManager.Instance.highScores.OrderByDescending(x => x.score).ToList();
        }
        else if (GameManager.Instance.highScores.Count == 10)
        {
            for (var i = 0; i < 10; i++)
            {
                Debug.Log("Loop: " + i);
                if (m_Points > GameManager.Instance.highScores[0].score)
                {
                    ShiftHighScoresDown(i, m_Points, GameManager.Instance.playerName);
                    break;
                }
                i++;
            }
        }
        GameManager.Instance.SaveData();
    }

    private void ShiftHighScoresDown(int startIndex, int newHighScore, string newHighScoreName)
    {
        // Start from the last index and shift each item down by one position
        for (int i = GameManager.Instance.highScores.Count - 1; i > startIndex; i--)
        {
            GameManager.Instance.highScores[i] = GameManager.Instance.highScores[i - 1];
        }

        // Insert the new high score at the specified index
        GameManager.Instance.highScores[startIndex] = new HighScoreData(newHighScore, newHighScoreName);
        Debug.Log(GameManager.Instance.highScores[startIndex].score + " " + GameManager.Instance.highScores[startIndex].playerName);

        // Ensure the list does not exceed 10 items
        if (GameManager.Instance.highScores.Count > 10)
        {
            GameManager.Instance.highScores.RemoveAt(10);
        }        
    }
}
