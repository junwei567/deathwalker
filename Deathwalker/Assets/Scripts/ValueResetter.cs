using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueResetter : MonoBehaviour
{
    [SerializeField]
    private IntegerSO playerHealthSO;
    [SerializeField]
    private IntegerSO playerLivesSO;
    [SerializeField]
    public FloatSO timerSO;
    [SerializeField]
    private IntegerSO dungeon1Kills;
    [SerializeField]
    private IntegerSO dungeon2Kills;
    [SerializeField]
    private IntegerSO dungeon4Kills;
    [SerializeField]
    private StringSO currentDungeon;
    void Awake() {
        // Setting default values
        playerHealthSO.Value = 5;
        playerLivesSO.Value = 3;
        dungeon1Kills.Value = 1;
        dungeon2Kills.Value = 1;
        dungeon4Kills.Value = 1;
        currentDungeon.Value = "Dungeon1";
        // Make cursor visible
        Cursor.visible = true;
        timerSO.Value = 0;
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
