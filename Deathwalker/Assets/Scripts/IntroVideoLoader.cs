using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroVideoLoader : MonoBehaviour
{
    public float transitionTime = 17.5f;

    // Start is called before the first frame update
    void Start()
    {
        LoadNextLevel();
    }

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    // Update is called once per frame
    IEnumerator LoadLevel(int LevelIndex) {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(LevelIndex);
    }
}
