using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueResetter : MonoBehaviour
{
    [SerializeField]
    private IntegerSO playerHealthSO;
    void Awake() {
        playerHealthSO.Value = 4;
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
