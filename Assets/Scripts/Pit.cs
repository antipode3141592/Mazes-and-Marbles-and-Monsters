using System.Collections;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Objects
{

    public class Pit : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other)
            {
                //GameController.Instance.DestroyCharacter(other.gameObject);
                CharacterSheetController character = other.GetComponent<CharacterSheetController>();
                character.CharacterDeath();
            }
        }
    }
}