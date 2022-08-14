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
    private IntegerSO dungeon1Kills;
    [SerializeField]
    private IntegerSO dungeon2Kills;
    [SerializeField]
    private IntegerSO dungeon4Kills;
    [SerializeField]
    private StringSO currentDungeon;
    void Awake() {
        // Setting default values
        playerHealthSO.Value = 3;
        playerLivesSO.Value = 3;
        dungeon1Kills.Value = 10;
        dungeon2Kills.Value = 20;
        dungeon4Kills.Value = 30;
        currentDungeon.Value = "Dungeon1";
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
