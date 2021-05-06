﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;
using System;

namespace MarblesAndMonsters.Characters
{
    public class SpawnPoint_Marble : SpawnPoint
    {
        [SerializeField]
        private Vector2 launchForce;  //the force to apply to spawned object
        private int spawnTriggerHash;

        protected override void Awake()
        {
            base.Awake();
            spawnTriggerHash = Animator.StringToHash("Spawn");
        }

        public override void SpawnCharacter()
        {
            //most of these spawnpoints will only have a handful of characters assigned, so a foreach should be fine
            foreach (var _char in characters)
            {
                //find the first available inactive character, activate it, then exit the loop
                if (!_char.isActiveAndEnabled)
                {
                    StartCoroutine(MarbleSpawn(_char));
                    //_char.gameObject.SetActive(true);
                    //_char.transform.position = transform.position;
                    //_char.GetComponent<Rigidbody2D>().AddForce(launchForce, ForceMode2D.Impulse);
                    //last stored rotation is sufficient
                    break;
                }
            }
        }

        private IEnumerator MarbleSpawn(CharacterControl character)
        {
            animator.SetTrigger(spawnTriggerHash);
            yield return new WaitForSeconds(0.75f);     //includes 0.5 sec ready blinking and 0.25 sec opening
            character.gameObject.SetActive(true);
            character.transform.position = transform.position;
            character.GetComponent<Rigidbody2D>().AddForce(launchForce, ForceMode2D.Impulse);
        }
    }
}
