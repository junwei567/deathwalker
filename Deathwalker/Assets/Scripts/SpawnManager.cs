using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager SharedInstance;
    private float enemy_type;

    void  spawnFromPooler(ObjectType i){
        // static method access
        GameObject item =  ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item  !=  null){
            // x offmap bounds: -1.4, 1.4
            // y offmap bounds: -1.2, 1.2
            // xRand will determine if enemy spawns from left or right side
            int xRand = Random.Range(0,2);
            float xPos; 
            if (xRand == 0) xPos = -1.5f;
            else xPos = 1.5f;

            item.transform.position  =  new  Vector3(xPos, Random.Range(-1.15f, 1.15f), 0);
            item.SetActive(true);
        }
        else{
            Debug.Log("not enough items in the pool.");
        }
    }

    void spawnMobs(int stage)
    {
        if (stage == 1) {
            StartCoroutine(stage1Mobs());
        }
        if (stage == 2) {
            StartCoroutine(stage1MeleeMobs());
        }
    }
    IEnumerator stage1Mobs()
    {
        int counter = 0;
        while (counter < 20) {
            if (counter % 2 == 0) {
                // spawnFromPooler(ObjectType.archer);
                spawnFromPooler(ObjectType.skeleton);
            } else {
                // spawnFromPooler(ObjectType.wizard);
                spawnFromPooler(ObjectType.knight);
            }
            counter++;
            yield return new WaitForSeconds(1.5f);
        }
        yield return null;

    }
    IEnumerator stage1MeleeMobs()
    {
        int counter = 0;
        while (counter < 1) {
            enemy_type = Random.Range(0.0f, 1.0f);
            if (enemy_type >= 0.0f) {
                spawnFromPooler(ObjectType.skeleton);
            } else {
                spawnFromPooler(ObjectType.knight);
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
