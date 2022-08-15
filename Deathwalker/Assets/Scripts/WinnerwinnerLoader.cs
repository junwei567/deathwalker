using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerwinnerLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    void OnEnable() {
        LoadNextLevel();
    }

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        Cursor.visible = true;
        // Load start menu when player has no more lives
        SceneManager.LoadScene("Winnerwinner");
    }
}
