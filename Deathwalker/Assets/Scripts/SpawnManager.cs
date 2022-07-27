using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager SharedInstance;

    void  spawnFromPooler(ObjectType i){
        // static method access
        GameObject item =  ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item  !=  null){
            // x offmap bounds: -1.4, 1.4
            // y offmap bounds: -1.2, 1.2
            // xRand will determine if enemy spawns from left or right side
            int xRand = Random.Range(0,2);
            float xPos; 
            if (xRand == 0) xPos = -0.95f;
            else xPos = 0.95f;

            item.transform.position  =  new  Vector3(xPos, Random.Range(-0.7f, 0.7f), 0);
            item.SetActive(true);
        }
        else{
            Debug.Log("not enough items in the pool.");
        }
    }

    void spawnMobs(int stage)
    {
        if (stage == 1) {
            // for (int j = 0; j < 20; j++) {
            //     spawnFromPooler(ObjectType.archer);
            //     spawnFromPooler(ObjectType.wizard);
            // }
            StartCoroutine(stage1Mobs());
        }
    }
    IEnumerator stage1Mobs()
    {
        int counter = 0;
        while (counter < 20) {
            if (counter % 2 == 0) {
                spawnFromPooler(ObjectType.archer);
            } else {
                spawnFromPooler(ObjectType.wizard);
            }
            counter++;
            yield return new WaitForSeconds(1.5f);
        }
        yield return null;

    }
    void Awake()
    {
        SharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnMobs(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
