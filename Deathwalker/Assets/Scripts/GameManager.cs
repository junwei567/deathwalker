using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerObject;
    private PlayerController playerController;
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
        if (!PlayerPrefs.HasKey("State")) {
            return;
        }
        string state = PlayerPrefs.GetString("State");
        collisions = int.Parse(state);
    }
    // Start is called before the first frame update
    void Start()
    {
        displayHealth(playerHealthSO.Value);
        playerController = playerObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // testing Health
        // if (Input.GetKeyDown(KeyCode.Backspace)) {
        //     if (playerHealthSO.Value > 1) {
        //         playerHealthSO.Value--;
        //         displayHealth();
        //     }
        // }
        // if (Input.GetKeyDown(KeyCode.Tab)) {
        //     if (playerHealthSO.Value < 4) {
        //         playerHealthSO.Value++;
        //         displayHealth();
        //     }
        // }
    }
    void startOver(int livesLeft) {
        if (livesLeft == 2) {
            displayHealth(2);
            playerHealthSO.Value = 2;
            // Activate player's long dash
            playerController.activateLongDash();
        }
        if (livesLeft == 1) {
            displayHealth(1);
            playerHealthSO.Value = 1;
            // Activate player's double dash
            playerController.activateDoubleDash();
        }
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
