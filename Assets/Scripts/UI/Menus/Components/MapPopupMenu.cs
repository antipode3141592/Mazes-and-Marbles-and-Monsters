using MarblesAndMonsters.Menus.Components;
using LevelManagement.Menus;
using LevelManagement.DataPersistence; //for datamanager
using UnityEngine;
using LevelManagement.Levels;
using System.Collections.Generic;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus
{
    public class MapPopupMenu : Menu<MapPopupMenu>
    {
        public Text locationId;
        public Text locationDescriptionText;
        public Button PlayButton;
        public Button ResetButton;

        public void OnDisable()
        {
            PlayButton.onClick.RemoveAllListeners();
            ResetButton.onClick.RemoveAllListeners();
        }

        public void OnClosePopupClicked()
        {
            _menuManager.CloseMenu();
        }

        public void OnPlayClicked()
        {

        }

        public void OnResetClicked()
        {

        }
    }
}
