using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/InventoryPrefabs")]
public class InventoryPrefabList : ScriptableObject
{
    public List<InventoryPrefabData> inventoryPrefabDatas;        
}

[Serializable]
public class InventoryPrefabData
{
    public string ID;
    public GameObject Prefab;
}