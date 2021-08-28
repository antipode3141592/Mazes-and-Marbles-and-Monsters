using System.Collections;
using UnityEngine;
using MarblesAndMonsters.Characters;
using UnityEngine.Tilemaps;

namespace MarblesAndMonsters.Objects
{

    public class Pit : MonoBehaviour
    {
        public Tilemap tileMap;
        public TilemapCollider2D tilemapCollider;

        protected void Awake()
        {
            //tileMap = gameObject.GetComponent<Tilemap>();
            //compositeCollider = GetCom
        }

        //if the 
        private void OnTriggerStay2D(Collider2D other)
        {
            if (tilemapCollider.OverlapPoint(other.transform.position))
            {
                Characters.CharacterControl character = other.GetComponent<Characters.CharacterControl>();
                if (character != null)
                {

                    //var position = other.transform.position;
                    //var tilemapPosition = tileMap.WorldToCell(position);
                    character.ApplyFalling(other.transform.position);
                }
            }
        }

        //utility script to apply an offset to an input vector3int 
        static public Vector3 CenteredTilemapPosition(Vector3Int tilemapPosition)
        {
            Vector3 offset = new Vector3(0.5f, 0.5f);
            return (Vector3)tilemapPosition + offset;
        }
    }
}