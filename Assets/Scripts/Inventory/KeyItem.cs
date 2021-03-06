﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Items
{

    public enum KeyType { Red, Blue, Green, Purple, Skeleton }

    public class KeyItem : MonoBehaviour
    {
        public KeyStats KeyStats;
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other != null && other.gameObject.CompareTag("Player"))
            {
                //add key to player's 
                Player.Instance.AddToKeyChain(this);
                audioSource.clip = KeyStats.ClipPickup;
                audioSource.Play();
                //disable the object
                //this.gameObject.SetActive(false);
                StartCoroutine(PickupKey());
            }
        }

        private IEnumerator PickupKey()
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }

        public void Reset()
        {
            gameObject.SetActive(true);
        }

    }
}
