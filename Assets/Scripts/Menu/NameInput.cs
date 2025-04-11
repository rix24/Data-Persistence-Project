using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    public GameObject nameInputText;    
    
    public void SetPlayerName()
    {
        StartCoroutine(SetPlayerNameDelay());
    }

    public void SetPlayerNameTextOnLoad()
    {
        StartCoroutine(SetPlayerNameTextOnLoadCoroutine());
    }

    public IEnumerator SetPlayerNameTextOnLoadCoroutine()
    {
        yield return null;
        nameInputText.GetComponent<Text>().text = GameManager.Instance.playerName;
    }

    private IEnumerator SetPlayerNameDelay()
    {
        yield return null;
        string playerName = nameInputText.GetComponent<Text>().text;
        GameManager.Instance.SetPlayerName(playerName);
    }
}
