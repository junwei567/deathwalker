using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int collisions = 0;
    public FloatingTextManager floatingTextManager;
    public int Collisions
    {
        get { return collisions; }
        set { collisions = value; }
    }
    void Awake()
    {
        // Prevent creation of multiple GameManager instances
        if (GameManager.instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        // Call LoadState everytime a scene is loaded
        // LoadState will be called before Start
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }
    public void SaveState()
    {
        string s = "";
        s += collisions.ToString();
        PlayerPrefs.SetString("State", s);
    }
    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("State")) {
            return;
        }
        string state = PlayerPrefs.GetString("State");
        collisions = int.Parse(state);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
