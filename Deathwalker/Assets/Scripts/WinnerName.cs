using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerName : MonoBehaviour
{

    public string winnerName;
    public GameObject inputField;

    public void StoreName() {
        winnerName = inputField.GetComponent<Text>().text;
        GameManager.instance.declareWinner(winnerName);
        SceneManager.LoadScene("StartMenu");
    }
}
