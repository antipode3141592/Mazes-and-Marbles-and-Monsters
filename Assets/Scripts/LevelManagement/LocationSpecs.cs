using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Levels
{
    [CreateAssetMenu(fileName = "Loc_", menuName = "Levels/LocationSpecs")]
    public class LocationSpecs : SerializedScriptableObject 
    {
        [SerializeField] string locationId;
        [SerializeField] string displayName;
        [SerializeField] Sprite thumbnail;
        [SerializeField] Sprite relicImage;
        [SerializeField] int totalRelics;
        [SerializeField] bool isKnown;
        [SerializeField] bool isAvailable;
        [SerializeField] List<LevelSpecs> levelSpecs;
        [SerializeField] List<LocationSpecs> nextLocations;

        public string LocationId => locationId;
        public string DisplayName => displayName;
        public Sprite Thumbnail => thumbnail;
        public Sprite RelicImage => relicImage;
        public int TotalRelics => totalRelics;
        public bool IsKnown => isKnown;
        public bool IsAvailable => isAvailable;

        public List<LevelSpecs> LevelSpecs => levelSpecs;
        public List<LocationSpecs> NextLocations => nextLocations;
    }

}
