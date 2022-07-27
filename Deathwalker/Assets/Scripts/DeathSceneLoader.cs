using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public Object ReloadScene;

    void OnEnable() {
        LoadNextLevel();
    }

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(ReloadScene.name);
    }
}
