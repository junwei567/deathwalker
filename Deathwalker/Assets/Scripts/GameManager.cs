using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerObject;
    public PlayerController playerController;
    public int collisions = 0;
    public FloatingTextManager floatingTextManager;
    public int Collisions
    {
        get { return collisions; }
        set { collisions = value; }
    }
    public GameObject UIObject;
    public Text playerHealth;
    public Image heart1;
    public Image heart2;
    public Image heart3;

    [SerializeField]
    public IntegerSO playerHealthSO;
    [SerializeField]
    private IntegerSO playerLivesSO;
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
        if (scene.name == "Dungeon1") {
            Debug.Log("Start fight scene called");
            startFightScene();
        }
        if (!PlayerPrefs.HasKey("State")) {
            return;
        }
        string state = PlayerPrefs.GetString("State");
        collisions = int.Parse(state);
    }
    
    void startOver(int livesLeft) {
        if (livesLeft == 2) {
            displayHealth(2);
            playerHealthSO.Value = 2;
            UIObject.SetActive(false);
            SceneManager.LoadScene("Cutscene-DeathNumbaOne");
        }
        if (livesLeft == 1) {
            displayHealth(1);
            playerHealthSO.Value = 1;
            playerController.activateDoubleDash();
            UIObject.SetActive(false);
            SceneManager.LoadScene("Cutscene-DeathNumbaOne");
        }
    }

    void startFightScene() {
        UIObject.SetActive(true);
        playerObject = GameObject.Find("Player");
        playerController = playerObject.GetComponent<PlayerController>();
        // Activate player's power ups
        if (playerHealthSO.Value == 2) {
            playerController.activateLongDash();    
        }
        else if (playerHealthSO.Value == 1) {
            playerController.activateDoubleDash();
        }
        displayHealth(playerHealthSO.Value);
    }

    public void displayHealth(int health) {
        playerHealth.text = "Health: " + health;
        // TODO BIG ASSUMPTION: every attack only reduce player health by 1
       
        if (health == 3) {
            heart3.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart1.gameObject.SetActive(true);
        }
        if (health == 2) {
            heart3.gameObject.SetActive(false);
            heart2.gameObject.SetActive(true);
            heart1.gameObject.SetActive(true);
        }
        if (health == 1) {
            heart3.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            heart1.gameObject.SetActive(true);
        }
        if (health == 0) {
            // If player still has lives remaining
            if (playerLivesSO.Value > 1) {
                Debug.Log("Lives decreased");
                playerLivesSO.Value--;
                startOver(playerLivesSO.Value);
            } 
            // Game Over
            else {
                Debug.Log("No lives left");
            }
        }
    }
}
