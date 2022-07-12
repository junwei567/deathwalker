using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public Text playerHealth;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image heart4;
    [SerializeField]
    private IntegerSO playerHealthSO;
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
        displayHealth();
    }

    // Update is called once per frame
    void Update()
    {
        // testing Health
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            playerHealthSO.Value--;
            displayHealth();
        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
            playerHealthSO.Value++;
            displayHealth();
        }
    }

    private void displayHealth() {
        playerHealth.text = "Health: " + playerHealthSO.Value;
        // TODO BIG ASSUMPTION: every attack only reduce player health by 1
        if (playerHealthSO.Value == 4) {
            heart4.gameObject.SetActive(true);
        }
        if (playerHealthSO.Value == 3) {
            heart4.gameObject.SetActive(false);
            heart3.gameObject.SetActive(true);
        }
        if (playerHealthSO.Value == 2) {
            heart3.gameObject.SetActive(false);
            heart2.gameObject.SetActive(true);
        }
        if (playerHealthSO.Value == 1) {
            heart2.gameObject.SetActive(false);
        }
    }
}
