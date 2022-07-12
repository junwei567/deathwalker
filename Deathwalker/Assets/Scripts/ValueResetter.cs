using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueResetter : MonoBehaviour
{
    [SerializeField]
    private IntegerSO playerHealthSO;
    // Start is called before the first frame update
    void Start()
    {
        playerHealthSO.Value = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
