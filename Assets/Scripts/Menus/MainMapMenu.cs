using MarblesAndMonsters.Menus.Components;
using LevelManagement.Menus;
using LevelManagement.Data; //for datamanager
using UnityEngine;
using LevelManagement.Levels;
using System.Collections.Generic;

namespace MarblesAndMonsters.Menus
{
    public class MainMapMenu : Menu<MainMapMenu>
    {
        private LevelSpecs levelSpecs;

        public void OnLocationPressed()
        {
            //
        }

        public bool GetLocationSummary(string locationId)
        {
            if (DataManager.Instance != null)
            {
                //get a list of all saved locations 
                List<LevelSaveData> levels = DataManager.Instance.LevelSaves.FindAll(x => x.LocationId == locationId);
                return true;
            } 
            else
            {
                return false;
            }
        }
    }
}
