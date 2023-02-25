using Rewired;
using UnityEngine;
using Zenject;

namespace MarblesAndMonsters
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        ICharacterManager _characterManager;

        Player player;
        readonly int playerId = 0;
        [SerializeField] string horizontalAxisName;
        [SerializeField] string verticalAxisName;

        [Inject]
        public void Init(ICharacterManager characterManager)
        {
            _characterManager = characterManager;
        }

        void Awake()
        {
            player = ReInput.players.GetPlayer(playerId);
        }

        public void MeasureBoardTilt()
        {
            _characterManager.Input_Acceleration = player.GetAxis2D(horizontalAxisName, verticalAxisName);
        }

    }
}