using UnityEngine.InputSystem;

namespace MarblesAndMonsters
{
    public interface IInputManager
    {
        void Init(ICharacterManager characterManager);
        void MeasureBoardTilt();
    }
}