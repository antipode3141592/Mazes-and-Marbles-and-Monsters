using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using MarblesAndMonsters.Items;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Tiles
{

    public class GateController : MonoBehaviour
    {
        [SerializeField]
        private Tilemap WallTileMap;

        private Tilemap tilemap;

        private Dictionary<TileBase, Vector3Int> storedWallTiles = new Dictionary<TileBase, Vector3Int>();
        private Dictionary<TileBase, Vector3Int> storedGateTiles = new Dictionary<TileBase, Vector3Int>();

        protected void Awake()
        {
            tilemap = GetComponent<Tilemap>();
        }

        public bool OpenGate(Vector3 gatePosition)
        {
            Vector3Int gateTilePosition = tilemap.WorldToCell(gatePosition);
            TileBase wallTile = WallTileMap.GetTile(gateTilePosition);
            TileBase gateTile = tilemap.GetTile(gateTilePosition);
            if (wallTile != null)
            {
                storedWallTiles.Add(wallTile, gateTilePosition);
                storedGateTiles.Add(gateTile, gateTilePosition);
                WallTileMap.SetTile(gateTilePosition, null);
                tilemap.SetTile(gateTilePosition, null);
                Debug.Log(string.Format("GateController opening gate at {0}, {1}, {2}", gateTilePosition.x, gateTilePosition.y, gateTilePosition.z));
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CloseAllGates()
        {
            //use storedTiles dictionary to replace all tiles removed by OpenGate()
            foreach (KeyValuePair<TileBase, Vector3Int> tilePair in storedWallTiles)
            {
                WallTileMap.SetTile(tilePair.Value, tilePair.Key);
            }
            storedWallTiles.Clear();    //remove all stored tiles
        }

        public bool CloseGate(Vector3Int gatePosition)
        {

            Debug.Log(string.Format("GateController closing gate at {0}, {1}, {2}", gatePosition.x, gatePosition.y, gatePosition.z));
            return false;
        }

        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    //only a Player can open the gate
        //    if (other.CompareTag("Player"))
        //    {
                

        //        Vector3Int currentTile = tilemap.WorldToCell(other.transform.position);
        //        Debug.Log(string.Format("Gate collision at {0}, {1}, {2}", currentTile.x, currentTile.y, currentTile.z));
        //        Gate gate = tilemap.GetTile<Gate>(currentTile);
        //        if (gate.Unlock())
        //        {
        //            OpenGate(gameObject.transform.position);
        //        }
        //    }
        //}
    }
}