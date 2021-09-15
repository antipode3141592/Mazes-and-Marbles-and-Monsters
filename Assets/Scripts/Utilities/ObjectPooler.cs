using System;
using System.Collections.Generic;
using UnityEngine;


namespace MarblesAndMonsters.Utilities
{
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

        private void Awake()
        {
            SharedInstance = this;
            pooledObjects = new List<GameObject>();
            CreatePools();
        }

        //
        public void CreatePools()
        {
            foreach (ObjectPoolItem item in itemsToPool)
            {
                //check
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }
            }
        }

        /// <summary>
        /// Returns a disabled GameObject matching the requested tag, or null if tag not found
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>disabled Game Object matching the requested tag, or null if tag not found</returns>
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

        ///// <summary>
        ///// Return a disabled GameObject that contains the requested type, or null if type not found
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public GameObject GetPooledObject(Type type)
        //{
        //    for (int i = 0; i < pooledObjects.Count; i++)
        //    {
        //        if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].TryGetComponent(typeof(IPoolable), out var component))
        //        {
        //            if (component.GetType)
        //                return pooledObjects[i];
        //        }
        //    }
        //    foreach (ObjectPoolItem item in itemsToPool)
        //    {
        //        if (item.objectToPool.GetComponent<>())
        //        {
        //            if (item.shouldExpand)
        //            {
        //                GameObject obj = (GameObject)Instantiate(item.objectToPool);
        //                obj.SetActive(false);
        //                pooledObjects.Add(obj);
        //                return obj;
        //            }
        //        }
        //    }
        //    return null;
        //}
    }
}
