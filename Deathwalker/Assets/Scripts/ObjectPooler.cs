using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObjectType {
    archer =  0,
    wizard =  1,
    knight =  2,
    skeleton =  3
}

public class ObjectPooler : MonoBehaviour
{
    public  List<ObjectPoolItem> itemsToPool; // types of different object to pool
    public  List<ExistingPoolItem> pooledObjects; // a list of all objects in the pool, of all types
    public static ObjectPooler SharedInstance;
    void  Awake()
    {
        SharedInstance = this;
        pooledObjects  =  new  List<ExistingPoolItem>();

        foreach (ObjectPoolItem item in  itemsToPool)
        {
            for (int i =  0; i  <  item.amount; i++)
            {
                // this 'prefab' a local variable, but Unity will not remove it since it exists in the scene
                GameObject prefab = (GameObject)Instantiate(item.prefab);
                prefab.SetActive(false);
                prefab.transform.parent  =  this.transform;
                ExistingPoolItem e =  new  ExistingPoolItem(prefab, item.type);
                pooledObjects.Add(e);
            }
        }
    }
    public  GameObject  GetPooledObject(ObjectType type)
    {
        // return inactive pooled object if it matches the type
        for (int i =  0; i  <  pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy  &&  pooledObjects[i].type  ==  type)
            {
                return  pooledObjects[i].gameObject;
            }
        }
        // If no more inactive objects in pool to return
        foreach (ObjectPoolItem item in itemsToPool) {
            if (item.type == type) {
                GameObject prefab = (GameObject)Instantiate(item.prefab);
                prefab.SetActive(false);
                prefab.transform.parent = this.transform;
                pooledObjects.Add(new ExistingPoolItem(prefab, item.type));
                return prefab;
            }
        }
        return null;
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

[System.Serializable]
public  class ObjectPoolItem
{
	public  int amount;
	public  GameObject prefab;
	public  ObjectType type;
}

public  class ExistingPoolItem
{
	public  GameObject gameObject;
	public  ObjectType type;

	// constructor
	public  ExistingPoolItem(GameObject gameObject, ObjectType type) {
		// reference input
		this.gameObject  =  gameObject;
		this.type  =  type;
	}
}
