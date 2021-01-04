using System.Collections;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Objects
{

    public class Pit : MonoBehaviour
    {

        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    if (other)
        //    {
        //        //GameController.Instance.DestroyCharacter(other.gameObject);

        //        Debug.Log(string.Format("{0} has a speed of {1}", other.gameObject.name, other.attachedRigidbody.velocity.magnitude));
        //        CharacterSheetController character = other.GetComponent<CharacterSheetController>();
        //        character.CharacterDeath(DeathType.Falling);
        //    }
        //}

        private void OnTriggerStay2D(Collider2D other)
        {
            CharacterSheetController character = other.GetComponent<CharacterSheetController>();
            character.CharacterDeath(DeathType.Falling);
        }
    }
}