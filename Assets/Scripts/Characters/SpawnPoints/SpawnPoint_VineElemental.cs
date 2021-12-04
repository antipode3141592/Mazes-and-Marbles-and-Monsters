using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MarblesAndMonsters.Pooling;
using System.Collections;

namespace MarblesAndMonsters.Characters
{

    public class SpawnPoint_VineElemental: SpawnPoint
    {
        [SerializeField]
        private ProjectilePooler _projectilePooler;
        [SerializeField]
        private Collider2D _triggerCollider;


        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !collision.isTrigger)
            {
                animator.SetTrigger("Open");
                _triggerCollider.enabled = false;
                StartCoroutine(Open(1f));
            }
        }

        public override void Reset()
        {
            base.Reset();
            animator.SetTrigger("Close");
            _triggerCollider.enabled = true;
        }

        private IEnumerator Open(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            isAvailable = true;
            SpawnCharacter();
        }

        protected override void AfterSpawn(CharacterControl character)
        {
            base.AfterSpawn(character);
            isAvailable = false;
            var ranged = character.gameObject.GetComponent<RangedController>();
            ranged.ProjectilePooler = _projectilePooler;
        }
    }
}
