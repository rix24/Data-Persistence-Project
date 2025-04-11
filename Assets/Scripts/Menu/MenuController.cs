using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        GameManager.Instance.SaveData();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ViewHighscores()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
