using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectTile : TileBase
{
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {

        return base.StartUp(position, tilemap, go);
    }
}
