using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager SharedInstance;
    private float enemy_type;
    private float time_spawn;
    [SerializeField]
    private StringSO currentDungeon;

    void  spawnFromPooler(ObjectType i){
        // static method access
        GameObject item =  ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item  !=  null){
            // x offmap bounds: -1.4, 1.4
            // y offmap bounds: -1.2, 1.2
            // xRand will determine if enemy spawns from left or right side
            int xRand = Random.Range(0,2);
            float xPos; 
            // Dungeon 1 spawn position
            if (currentDungeon.Value == "Dungeon1") {
                if (xRand == 0) xPos = -1.0f;
                else xPos = 1.0f;
                item.transform.position  =  new  Vector3(xPos, Random.Range(-1.15f, 1.15f), 0);
            } 
            // Dungeon 2 spawn position
            else if (currentDungeon.Value == "NEWDungeon2") {
                if (xRand == 0) xPos = -1.45f;
                else xPos = 1.515f;
                item.transform.position  =  new  Vector3(xPos, Random.Range(-0.9f, 0.9f), 0);
            } 
            // Dungeon 4 spawn position
            else {
                float yPos;
                if (xRand == 0) {
                    xPos = -1f;
                    yPos = -0.67f;
                }
                else {
                    xPos = 2.8f;
                    yPos = 1.096f;
                }
                item.transform.position  =  new  Vector3(xPos, yPos, 0);
            }
            
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
            StartCoroutine(stage2Mobs());
        }
        if (stage == 3) {
            StartCoroutine(stage3Mobs());
        }
    }
    IEnumerator stage1Mobs()
    {
        int counter = 0;
        while (counter < 15) {
            enemy_type = Random.Range(0.0f, 1.0f);
            // Spawn knights and skeletons
            if (enemy_type >= 0.7f) {
                spawnFromPooler(ObjectType.skeleton);
            } 
            else{
                spawnFromPooler(ObjectType.knight);
            }
            counter++;
            time_spawn = Random.Range(2f, 2.5f);
            yield return new WaitForSeconds(time_spawn);
        }
        while (counter >= 15 && counter < 40) {
            enemy_type = Random.Range(0.0f, 1.0f);
            // Spawn knights and skeletons
            if (enemy_type >= 0.9f) {
                spawnFromPooler(ObjectType.skeleton);
            } else if (enemy_type >= 0.5f) {
                spawnFromPooler(ObjectType.archer);
            }
            else {
                spawnFromPooler(ObjectType.knight);
            }
            counter++;
            time_spawn = Random.Range(1.5f, 2f);
            yield return new WaitForSeconds(time_spawn);
        }
        yield return null;

    }
    IEnumerator stage2Mobs()
    {
        int counter = 0;
        while (counter < 40) {
            enemy_type = Random.Range(0.0f, 1.0f);
            // Spawn more wizards
            if (enemy_type >= 0.9f) {
                spawnFromPooler(ObjectType.skeleton);
            } else if (enemy_type >= 0.65f) {
                spawnFromPooler(ObjectType.archer);
            } else if (enemy_type >= 0.4f) {
                spawnFromPooler(ObjectType.knight);
            } else{
                spawnFromPooler(ObjectType.wizard);
            }
            counter++;
            time_spawn = Random.Range(1.0f, 2.0f);
            yield return new WaitForSeconds(time_spawn);
        }
        while (counter >=40 && counter < 80) {
            enemy_type = Random.Range(0.0f, 1.0f);
            if (enemy_type >= 0.9f) {
                spawnFromPooler(ObjectType.skeleton);
            } else if (enemy_type >= 0.6f) {
                spawnFromPooler(ObjectType.archer);
            } else if (enemy_type >= 0.3f) {
                spawnFromPooler(ObjectType.knight);
            } else{
                spawnFromPooler(ObjectType.wizard);
            }
            counter++;
            time_spawn = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(time_spawn);
        }
        yield return null;
    }
    IEnumerator stage3Mobs()
    {
        int counter = 0;
        while (counter < 120) {
            enemy_type = Random.Range(0.0f, 1.0f);
            if (enemy_type >= 0.9f) {
                spawnFromPooler(ObjectType.skeleton);
            } else if (enemy_type >= 0.6f) {
                spawnFromPooler(ObjectType.archer);
            } else if (enemy_type >= 0.3f) {
                spawnFromPooler(ObjectType.knight);
            } else{
                spawnFromPooler(ObjectType.wizard);
            }
            counter++;
            time_spawn = Random.Range(0.0f, 1.0f);
            yield return new WaitForSeconds(time_spawn);
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
        if (currentDungeon.Value == "Dungeon1") {
            spawnMobs(1);
        }
        else if (currentDungeon.Value == "NEWDungeon2") {
            spawnMobs(2);
        }
        else if (currentDungeon.Value == "Dungeon4") {
            spawnMobs(3);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
