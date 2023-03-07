using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    public class SpawnPoint_Marble : SpawnPoint
    {
        [SerializeField]
        private Vector2 launchForce;  //the force to apply to spawned object

        protected override void AfterSpawn(CharacterControl character)
        {
            character.ApplyImpulse(launchForce);
        }
    }
}
