using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text highScoreText;
    void Start()
    {
        highScoreText = GetComponent<Text>();

        if (GameManager.Instance.highScore == null)
        {
            highScoreText.text = $"High Score : 0 ";           
        } else
        {
            highScoreText.text = $"High Score : {GameManager.Instance.highScore.score} " + $"{GameManager.Instance.highScore.playerName}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
