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
    public Text timeTaken;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image heart4;
    public Image heart5;
    [SerializeField]
    public IntegerSO playerHealthSO;
    [SerializeField]
    private IntegerSO playerLivesSO;
    [SerializeField]
    public FloatSO timerSO;
    [SerializeField]
    private StringSO currentDungeon;
    [SerializeField]
    private IntegerSO dungeon1Kills;
    [SerializeField]
    private IntegerSO dungeon2Kills;
    [SerializeField]
    private IntegerSO dungeon4Kills;
    public HighScoreTable highScoreTable;
    
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
        if (scene.name == "Dungeon1" || scene.name == "NEWDungeon2" || scene.name == "Dungeon4") {
            Debug.Log("Start fight scene called");
            startFightScene();
        }
        if (!PlayerPrefs.HasKey("State")) {
            return;
        }

        if (!PlayerPrefs.HasKey("hScoreTable")) {
            List<HScoreEntry> hScoreEntryList = new List<HScoreEntry>() {
                new HScoreEntry{score=900.0f, name="Darkio"}
            };
            string json = JsonUtility.ToJson(hScoreEntryList);
            PlayerPrefs.SetString("hScoreTable", json);
            PlayerPrefs.Save();
        }
        string state = PlayerPrefs.GetString("State");
        collisions = int.Parse(state);
    }
    
    void startOver(int livesLeft) {
        if (livesLeft == 2) {
            displayHealth(5);
            playerHealthSO.Value = 5;
            UIObject.SetActive(false);
            SceneManager.LoadScene("Cutscene-DeathNumbaOne");
        }
        if (livesLeft == 1) {
            displayHealth(5);
            playerHealthSO.Value = 5;
            UIObject.SetActive(false);
            SceneManager.LoadScene("Cutscene-DeathNumbaOne");
        }
    }

    void startFightScene() {
        UIObject.SetActive(true);
        playerObject = GameObject.Find("Player");
        playerController = playerObject.GetComponent<PlayerController>();
        // Activate player's power ups
        if (playerLivesSO.Value == 2) {
            playerController.activateLongDash();    
        }
        else if (playerLivesSO.Value == 1) {
            playerController.activateLongDash();    
            playerController.activateDoubleDash();
        }
        displayHealth(playerHealthSO.Value);
    }
    public void stageComplete() {
        if (currentDungeon.Value == "Dungeon1") {
            currentDungeon.Value = "NEWDungeon2";
            SceneManager.LoadScene("NEWDungeon2");
        }
        else if (currentDungeon.Value == "NEWDungeon2") {
            currentDungeon.Value = "Dungeon4";
            SceneManager.LoadScene("Dungeon4");
        }
        else if (currentDungeon.Value == "Dungeon4") {
            UIObject.SetActive(false);
            SceneManager.LoadScene("Cutscene-GameClear");
        }
    }
    public void enemyKilled() {
        // Decrease the kills left in order for player to clear the stage
        if (currentDungeon.Value == "Dungeon1") {
            dungeon1Kills.Value --;
        } 
        else if (currentDungeon.Value == "NEWDungeon2") {
            dungeon2Kills.Value --;
        } 
        else if (currentDungeon.Value == "Dungeon4") {
            dungeon4Kills.Value --;
        }
        // If kills left is zero, call function in GameManager to signify stage clear
        if (dungeon1Kills.Value == 0 && currentDungeon.Value == "Dungeon1") {
            stageComplete();
        } else if (dungeon2Kills.Value == 0 && currentDungeon.Value == "NEWDungeon2") {
            stageComplete();
        } else if (dungeon4Kills.Value == 0 && currentDungeon.Value == "Dungeon4") {
            stageComplete();
        }
    }

    public void displayHealth(int health) {
        playerHealth.text = "Health: " + health;
        // TODO BIG ASSUMPTION: every attack only reduce player health by 1
        if (health == 5) {
            heart5.gameObject.SetActive(true);
            heart4.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart1.gameObject.SetActive(true);
        }
        if (health == 4) {
            heart5.gameObject.SetActive(false);
            heart4.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart1.gameObject.SetActive(true);
        }
        if (health == 3) {
            heart5.gameObject.SetActive(false);
            heart4.gameObject.SetActive(false);
            heart3.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart1.gameObject.SetActive(true);
        }
        if (health == 2) {
            heart5.gameObject.SetActive(false);
            heart4.gameObject.SetActive(false);
            heart3.gameObject.SetActive(false);
            heart2.gameObject.SetActive(true);
            heart1.gameObject.SetActive(true);
        }
        if (health == 1) {
            heart5.gameObject.SetActive(false);
            heart4.gameObject.SetActive(false);
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
                playerLivesSO.Value--;
                UIObject.SetActive(false);
                SceneManager.LoadScene("Cutscene-GameOva");
            }
        }
    }

    public void timerTicker() {
        timeTaken.text = "Time: " + timerSO.Value.ToString("F2");
    }

    public void declareWinner(string winnerName) {
        if (winnerName == "") {
            winnerName = "Osiris";
        }
        highScoreTable.AddHScoreEntry( Mathf.Round(timerSO.Value * 100f) / 100f, winnerName);
    }

    private class HScoreEntry {
        public float score;
        public string name;
    }
}
