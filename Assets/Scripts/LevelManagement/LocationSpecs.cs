using UnityEngine;

namespace LevelManagement.Levels
{
    [CreateAssetMenu(fileName = "Loc_", menuName = "Levels/LocationSpecs")]
    public class LocationSpecs : ScriptableObject 
    {
        [SerializeField] string locationId;
        [SerializeField] string displayName;
        [SerializeField] Sprite thumbnail;
        [SerializeField] Sprite relicImage;
        [SerializeField] int totalRelics;

        public string LocationId => locationId;
        public string DisplayName => displayName;
        public Sprite Thumbnail => thumbnail;
        public Sprite RelicImage => relicImage;
        public int TotalRelics => totalRelics;
    }

}
