using MarblesAndMonsters.Characters;
using UnityEngine;

namespace LevelManagement.Menus.Components
{
    //View Controller for HealthBar
    //  current system uses animated hearts to represent health, one heart = 1hp
    // public functions:
    //  UpdateHealth() - 
    //  
    public class HealthBarController : MonoBehaviour
    {
        public GameObject[] heartArray; //currently set in Unity to twenty hearts
        Animator[] heartAnimators;

        private void Awake()
        {
            heartAnimators = new Animator[heartArray.Length];
            //populate array of Animators
            for (int i = 0; i < heartArray.Length; i++)
            {
                heartAnimators[i] = heartArray[i].GetComponent<Animator>();
            }
        }

        public void UpdateHealth()
        {
            if ((Player.Instance != null) && (Player.Instance.MySheet != null))
            {
                //playerCurrentHealth = Player.Instance.MySheet.CurrentHealth;
                //playerMaxHealth = Player.Instance.MySheet.MaxHealth;
                //activate heart objects equal to max health
                int i = 0;
                for (; i < Player.Instance.MySheet.CurrentHealth; i++)
                {
                    heartArray[i].SetActive(true);
                    heartAnimators[i].Play("FullHeart");
                }
                //set hearts up to max health to empty
                for (; i < Player.Instance.MySheet.MaxHealth; i++)
                {
                    heartArray[i].SetActive(true);
                    heartAnimators[i].Play("LoseHeart");
                }
                //deactivate remaining hearts
                for (; i < heartArray.Length; i++)
                {
                    heartArray[i].SetActive(false);
                }
            }
        }
    }
}