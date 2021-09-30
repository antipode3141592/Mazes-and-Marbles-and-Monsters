using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Tiles;

//  Class:  SwitchItem
//  Purpose:  Switches are colliders with triggers that acts on a remote object, targetObject
//      1)  targetObject is active, switch is awaiting collision
//      2)  upon collision with another game object (typically a player, but could be a marble or monster), set targetObject inactive
//      3)  wait for onTime seconds and then set targetObject active
//  Note:  This could end up being a "normallyOn" variant of the SwitchItem type.  I may want to have a few different Switch prefabs for
//      all manner of remote triggering.

namespace MarblesAndMonsters
{

    public class SwitchItem : MonoBehaviour
    {
        [SerializeField]
        private GameObject targetObject;
        [SerializeField]
        private float onTime = 2.0f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other != null)
            {
                //if anything triggers switch, do something with the targetObject
                if (targetObject.TryGetComponent<Gate>(out Gate gate))
                {
                    //do something with gate object?
                    if (targetObject.activeInHierarchy) 
                    {
                        targetObject.SetActive(!targetObject.activeSelf);
                        StartCoroutine(MomentaryOff());
                    }
                }
            }
        }

        private IEnumerator MomentaryOff()
        {
            yield return new WaitForSeconds(onTime);

            targetObject.SetActive(!targetObject.activeSelf);
        }
    }
}