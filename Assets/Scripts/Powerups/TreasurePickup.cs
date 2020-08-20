using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using HutongGames.PlayMaker;
using LevelManagement;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Items
{
    public class TreasurePickup : MonoBehaviour
    {
        //Player playerController;
        ////TreasureCounterController treasureUI;

        //void Awake()
        //{
        //    playerController = GameObject.FindObjectOfType<Player>();   //grab that player controller
        //                                                                //treasureUI = GameObject.FindObjectOfType<TreasureCounterController>();
        //}


        private void Reset()
        {
            gameObject.SetActive(true);
            //FsmVariables.GlobalVariables.FindFsmInt("TreasureCount_global").Value -= 1;
            //GameMenu.Instance.UpdateTreasureCounter();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //FsmVariables.GlobalVariables.FindFsmInt("TreasureCount_global").Value += 1;
                //GameMenu.Instance.UpdateTreasureCounter();
                Player.Instance.AddTreasure(+1);
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}