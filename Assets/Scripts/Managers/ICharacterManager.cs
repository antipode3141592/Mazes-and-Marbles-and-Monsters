using UnityEngine;

namespace MarblesAndMonsters
{
    public interface ICharacterManager
    {
        public Vector2 Input_Acceleration { get; set; }
        public int InitializeReferences();
        public void MoveAll();
        void ResetAll();
        public void SpawnAll();
    }
}