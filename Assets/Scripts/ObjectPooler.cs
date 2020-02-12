using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;

    public List<GameObject> pooledMarbles;
    public GameObject marbleToPool;
    public int amountToPoolMarbles;

    public List<GameObject> pooledMonsters;
    public GameObject monsterToPool;
    public int amountToPoolMonsters;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledMarbles = new List<GameObject>();
        pooledMonsters = new List<GameObject>();
        GameObject[] startingMarbles = GameObject.FindGameObjectsWithTag("Marble");
        pooledMarbles.AddRange(startingMarbles);
        for (int i = startingMarbles.Length-1; i < amountToPoolMarbles; i++)
        {
            GameObject obj = (GameObject)Instantiate(marbleToPool);
            obj.SetActive(false);
            pooledMarbles.Add(obj);
        }

        for (int i = 0; i < amountToPoolMonsters; i++)
        {
            GameObject obj = (GameObject)Instantiate(monsterToPool);
            obj.SetActive(false);
            pooledMonsters.Add(obj);
        }


    }

    public GameObject GetPooledMarble()
    {
        //1
        for (int i = 0; i < pooledMarbles.Count; i++)
        {
            //2
            if (!pooledMarbles[i].activeInHierarchy)
            {
                return pooledMarbles[i];
            }
        }
        //3   
        return null;
    }

    public GameObject GetPooledMonster()
    {
        //1
        for (int i = 0; i < pooledMonsters.Count; i++)
        {
            //2
            if (!pooledMonsters[i].activeInHierarchy)
            {
                return pooledMonsters[i];
            }
        }
        //3   
        return null;


    }
}
