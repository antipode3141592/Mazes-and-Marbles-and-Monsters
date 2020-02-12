using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public GameObject[] objectsToPool;
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;

    //public List<GameObject> pooledMarbles;
    //public GameObject marbleToPool;
    //public int amountToPoolMarbles;

    //public List<GameObject> pooledMonsters;
    //public GameObject monsterToPool;
    //public int amountToPoolMonsters;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
        //pooledMarbles = new List<GameObject>();
        //pooledMonsters = new List<GameObject>();
        //GameObject[] startingMarbles = GameObject.FindGameObjectsWithTag("Marble");
        //pooledMarbles.AddRange(startingMarbles);
        //for (int i = startingMarbles.Length-1; i < amountToPoolMarbles; i++)
        //{
        //    GameObject obj = (GameObject)Instantiate(marbleToPool);
        //    obj.SetActive(false);
        //    pooledMarbles.Add(obj);
        //}

        //for (int i = 0; i < amountToPoolMonsters; i++)
        //{
        //    GameObject obj = (GameObject)Instantiate(monsterToPool);
        //    obj.SetActive(false);
        //    pooledMonsters.Add(obj);
        //}
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }


    //public GameObject GetPooledMarble()
    //{
    //    //1
    //    for (int i = 0; i < pooledMarbles.Count; i++)
    //    {
    //        //2
    //        if (!pooledMarbles[i].activeInHierarchy)
    //        {
    //            return pooledMarbles[i];
    //        }
    //    }
    //    //3   
    //    return null;
    //}

    //public GameObject GetPooledMonster()
    //{
    //    //1
    //    for (int i = 0; i < pooledMonsters.Count; i++)
    //    {
    //        //2
    //        if (!pooledMonsters[i].activeInHierarchy)
    //        {
    //            return pooledMonsters[i];
    //        }
    //    }
    //    //3   
    //    return null;


    //}
}
