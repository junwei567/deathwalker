using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueResetter : MonoBehaviour
{
    [SerializeField]
    private IntegerSO playerHealthSO;
    [SerializeField]
    private IntegerSO playerLivesSO;
    void Awake() {
        playerHealthSO.Value = 3;
        playerLivesSO.Value = 3;
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
