using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string playerName;
    public GameObject nameInput;
    public List<HighScoreData> highScores;
    public HighScoreData highScore; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        LoadData();        
        if (nameInput != null && playerName != "")
        {
            nameInput.GetComponent<NameInput>().SetPlayerNameTextOnLoad();
        }
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }    

    [System.Serializable]
    public class saveData
    {
        public string playerName;
        public List<HighScoreData> highScores;
    }

    public void SaveData()
    {
        saveData data = new saveData();
        //Debug.Log(highScores[0].score + " " + highScores[0].playerName);
        data.playerName = playerName;
        data.highScores = highScores;
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Game saved with highscore: " + json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            saveData data = JsonUtility.FromJson<saveData>(json);
            playerName = data.playerName;
            highScores = data.highScores;
            if (highScores == null)
            {
                Debug.Log("High scores initialised");
                highScores = new List<HighScoreData>();
                highScores = new List<HighScoreData>();
                highScores.Add(new HighScoreData(0, ""));
            }
            else if (highScores.Count != 0)
            {
                highScore = highScores[0];
            }
        }
    }
}

[System.Serializable]
public class HighScoreData
{
    public int score;
    public string playerName;

    public HighScoreData(int points, string playerName)
    {
        this.score = points;
        this.playerName = playerName;
    }
}
