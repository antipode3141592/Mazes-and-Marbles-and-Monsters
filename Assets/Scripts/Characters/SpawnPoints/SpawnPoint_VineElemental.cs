using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MarblesAndMonsters.Pooling;

namespace MarblesAndMonsters.Characters
{

    public class SpawnPoint_VineElemental: SpawnPoint
    {
        [SerializeField]
        private ProjectilePooler _projectilePooler;
        [SerializeField]
        private Collider2D _triggerCollider;

        protected override void AfterSpawn(CharacterControl character)
        {
            base.AfterSpawn(character);
            var ranged = character.gameObject.GetComponent<RangedController>();
            ranged.ProjectilePooler = _projectilePooler;
        }
    }
}
