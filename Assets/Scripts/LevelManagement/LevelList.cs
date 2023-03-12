using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Levels
{
    [CreateAssetMenu(fileName = "LevelList", menuName = "Levels/Create LevelList", order =1)]

    //a readonly list of all levels
    //  levelspecs are edited in Unity editor
    //  a levellist scriptable object is attached to objects on the map screen
    //  
    public class LevelList : ScriptableObject
    {
        #region INSPECTOR
        [SerializeField] LevelSpecs mainMap;
        [SerializeField] LevelSpecs startingLevel;
        [SerializeField] int managerSceneIndex = 0;   //splash screen is build index 0
        [SerializeField] int lightingSceneIndex = 1;   //splash screen is build index 0
        [SerializeField] List<LevelSpecs> levels;
        #endregion

        public List<LevelSpecs> Levels => levels;
        public LevelSpecs MainMapSpecs => mainMap;
        public LevelSpecs StartingLevel => startingLevel;

        public int ManagerSceneIndex => managerSceneIndex;
        public int LightingSceneIndex => lightingSceneIndex;
    }
}