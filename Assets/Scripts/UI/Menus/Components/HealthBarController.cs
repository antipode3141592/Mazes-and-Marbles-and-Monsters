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

        List<GameObject> heartsToDelete;

        public Transform ParentTransform;
        public GameObject heartIconPrefab;

        void Awake()
        {
            HeartAnimators = new List<Animator>();
            Hearts = new List<GameObject>();
            heartsToDelete = new List<GameObject>();
        }

        void OnEnable()
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

        void AddHeart()
        {
            GameObject heart = Instantiate(heartIconPrefab, ParentTransform);
            Hearts.Add(heart);
            HeartAnimators.Add(heart.GetComponent<Animator>());
        }

        void RemoveHeart()
        {
            Destroy(Hearts[Hearts.Count-1].gameObject);
            Hearts.RemoveAt(Hearts.Count-1);
            HeartAnimators.RemoveAt(HeartAnimators.Count - 1);
        }

        public void UpdateHealth()
        {
            if ((Player.Instance != null) && (Player.Instance.MySheet != null))
            {
                int diff;
                if (Hearts.Count > Player.Instance.MySheet.MaxHealth)
                {
                    diff = Hearts.Count - Player.Instance.MySheet.MaxHealth;
                    for (int k = 0; k < diff; k++)
                        RemoveHeart();
                }

                if (Hearts.Count < Player.Instance.MySheet.MaxHealth)
                {
                    diff = Player.Instance.MySheet.MaxHealth - Hearts.Count;
                    for (int j = 0; j < diff; j++)
                        AddHeart();
                }

                int i = 0;

                for (; i < Player.Instance.MySheet.CurrentHealth; i++)
                    HeartAnimators[i].Play("FullHeart");
                for (; i < Player.Instance.MySheet.MaxHealth; i++)
                    HeartAnimators[i].Play("EmptyHeart");
            }
        }
    }
}