using MarblesAndMonsters.Characters;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MarblesAndMonsters.Objects
{

    public class Pit : MonoBehaviour
    {
        public Tilemap tileMap;
        public TilemapCollider2D tilemapCollider;

        protected void Awake()
        {
            if (tileMap == null)
            {
                tileMap = gameObject.GetComponent<Tilemap>();
            }
            if (tilemapCollider == null)
            {
                tilemapCollider = GetComponent<TilemapCollider2D>();
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (tilemapCollider.OverlapPoint(other.transform.position))
            {
                CharacterControl character = other.GetComponent<CharacterControl>();
                if (character != null)
                {

                    //var position = other.transform.position;
                    //var tilemapPosition = tileMap.WorldToCell(position);
                    character.ApplyFalling(other.transform.position);
                }

            }
        }
    }
}