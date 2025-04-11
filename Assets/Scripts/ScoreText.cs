using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text scoreText;
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = $"Score : 0 : {GameManager.Instance.playerName}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
