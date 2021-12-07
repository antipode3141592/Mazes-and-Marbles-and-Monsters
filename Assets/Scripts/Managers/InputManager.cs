using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace MarblesAndMonsters
{
    public class InputManager : MonoBehaviour
    {
        CharacterManager _characterManager;

        [Inject]
        public void Init(CharacterManager characterManager)
        {
            _characterManager = characterManager;
        }

        public void OnBoardTilt(InputValue inputValue)
        {
            _characterManager.Input_Acceleration = inputValue.Get<Vector2>();
        }

    }
}