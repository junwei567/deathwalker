using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    // public Object ReloadScene;
    [SerializeField]
    private StringSO currentDungeon;
    [SerializeField]
    private IntegerSO playerLivesSO;

    void OnEnable() {
        LoadNextLevel();
    }

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        // Load start menu when player has no more lives
        if (playerLivesSO.Value == 0) {
            SceneManager.LoadScene("StartMenu");
        } else {
            SceneManager.LoadScene(currentDungeon.Value);
        }
    }
}
