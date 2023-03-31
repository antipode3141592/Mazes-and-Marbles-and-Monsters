using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MarblesAndMonsters.Tiles
{
    public class PrefabTile : Tile
    {
        [SerializeField] GameObject _prefab;

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            TileBase tile = tilemap.GetTile(position);
            Matrix4x4 matrix = tilemap.GetTransformMatrix(position);
            go.transform.rotation = matrix.rotation;
            return base.StartUp(position, tilemap, go);
        }

    }
}