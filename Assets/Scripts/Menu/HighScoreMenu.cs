using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreMenu : MonoBehaviour
{
    public Transform highScoreParent;
    void Start()
    {
        PopulateHighScores();
    }

    void PopulateHighScores()
    {
        for (var i = 0; i < GameManager.Instance.highScores.Count; i++)
        {            
            var text = highScoreParent.GetChild(i).GetComponent<Text>();
            text.text = i.ToString() + ": "+ GameManager.Instance.highScores[i].playerName + " Score = " + GameManager.Instance.highScores[i].score;
        }
    }

    public void MainMen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
