using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;

namespace MarblesAndMonsters
{
    public class SwitchItem : MonoBehaviour
    {
        [SerializeField]
        private GameObject targetObject;
        [SerializeField]
        private float onTime = 2.0f;
        // Start is called before the first frame update
        void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

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
                        targetObject.SetActive(false);
                        StartCoroutine(MomentaryOff());
                        //targetObject.SetActive(false); 
                    }
                    //else { targetObject.SetActive(true); }
                }
            }
        }

        private IEnumerator MomentaryOff()
        {
            yield return new WaitForSeconds(onTime);

            targetObject.SetActive(true);
        }
    }
}