using MarblesAndMonsters.Characters;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MarblesAndMonsters.Objects
{
    public class Pit : MonoBehaviour
    {
        TilemapCollider2D tilemapCollider;

        protected void Awake()
        {
            tilemapCollider = GetComponent<TilemapCollider2D>();
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (tilemapCollider.OverlapPoint(other.transform.position))
            {
                CharacterControl character = other.GetComponent<CharacterControl>();
                if (character != null)
                    character.ApplyFalling(other.transform.position);
            }
        }
    }
}