using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters
{
    public class Marble : CharacterSheetController<Marble>
    {
        //Rigidbody2D rigidbody2D;
        //Player player;

        //// Start is called before the first frame update
        //void Start()
        //{

        //    //rigidbody2D = GetComponent<Rigidbody2D>();
        //}

        //private void OnCollisionEnter2D(Collision2D other)
        //{
        //    if (other.gameObject.CompareTag("Player"))
        //    {
        //        var player = other.gameObject.GetComponent<Player>();
        //        player.ChangeHealth(-1);
        //    }

        //}

        protected override void Update()
        {
            base.Update();

        }
    }
}
