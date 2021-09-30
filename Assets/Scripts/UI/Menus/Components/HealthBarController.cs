using MarblesAndMonsters.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Menus.Components
{
    //View Controller for HealthBar
    //  current system uses animated hearts to represent health, one heart = 1hp
    // public functions:
    //  UpdateHealth() - 
    //  
    public class HealthBarController : MonoBehaviour
    {
        public List<GameObject> Hearts; //currently set in Unity to twenty hearts
        public List<Animator> HeartAnimators;

        private List<GameObject> heartsToDelete;

        public Transform ParentTransform;
        public GameObject heartIconPrefab;

        private void Awake()
        {
            HeartAnimators = new List<Animator>();
            Hearts = new List<GameObject>();
            heartsToDelete = new List<GameObject>();
        }

        private void OnEnable()
        {
            Debug.Log("HealthBarController's OnEnable() called");
            if (Player.Instance)
            {
                for (int i = Hearts.Count; i < Player.Instance.MySheet.MaxHealth; i++)
                {
                    AddHeart();
                }
            }
        }

        private void AddHeart()
        {
            GameObject heart = Instantiate(heartIconPrefab, ParentTransform);
            Hearts.Add(heart);
            HeartAnimators.Add(heart.GetComponent<Animator>());
        }

        private void RemoveHeart()
        {

        }

        public void UpdateHealth()
        {
            //Debug.Log("UpdateHealth() called");
            if ((Player.Instance != null) && (Player.Instance.MySheet != null))
            {
                //Debug.Log(string.Format("Hearts.Count = {0}, HeartAnimators = {1}, PlayerMaxHealth = {2}", Hearts.Count, HeartAnimators.Count, Player.Instance.MySheet.MaxHealth));
                if (Hearts.Count < Player.Instance.MySheet.MaxHealth)
                {
                    int diff = Player.Instance.MySheet.MaxHealth - Hearts.Count;
                    for (int j = 0; j < diff; j++)
                    {
                        AddHeart();
                    }
                }
                //activate heart objects equal to max health
                int i = 0;
                for (; i < Player.Instance.MySheet.CurrentHealth; i++)
                {
                    HeartAnimators[i].Play("FullHeart");
                }
                //set hearts up to max health to empty
                for (; i < Player.Instance.MySheet.MaxHealth; i++)
                {
                    HeartAnimators[i].Play("EmptyHeart");
                }
            }
        }
    }
}