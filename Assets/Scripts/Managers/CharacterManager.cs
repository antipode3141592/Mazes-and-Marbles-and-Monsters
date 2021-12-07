using MarblesAndMonsters.Characters;
using MarblesAndMonsters.Items;
using MarblesAndMonsters.Spells;
using MarblesAndMonsters.Tiles;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MarblesAndMonsters
{
    public class CharacterManager : MonoBehaviour
    {
        public Vector2 Input_Acceleration { get; set; }

        //references to various game objects
        private List<InventoryItem> inventoryItems;
        private List<SpellPickupBase> spellPickups;
        private List<KeyItem> keyItems;
        private List<SpawnPoint> spawnPoints;
        private List<Gate> gates;
        private List<SpellEffectBase> spellEffects;

        public List<CharacterControl> Characters;

        /// <summary>
        /// cache references to all spawnpoints, inventory items, gates, and keys
        /// </summary>
        /// <returns>spawnpoint count (-1 if none found)</returns>
        public int InitializeReferences()
        {
            //TODO this doesn't find 
            inventoryItems = new List<InventoryItem>(FindObjectsOfType<InventoryItem>());
            keyItems = new List<KeyItem>(FindObjectsOfType<KeyItem>());
            spawnPoints = new List<SpawnPoint>(FindObjectsOfType<SpawnPoint>());
            gates = new List<Gate>(FindObjectsOfType<Gate>());
            spellPickups = new List<SpellPickupBase>(FindObjectsOfType<SpellPickupBase>());
            spellEffects = new List<SpellEffectBase>();
            if (spawnPoints.Count == 0)
            {
                //player start position is a spawnpoint, so a valid level shall have at least 1 spawnpoint
                return -1;
            }
            else
            {
                return spawnPoints.Count;
            }
        }

        /// <summary>
        /// Reset all objects
        /// </summary>
        internal void ResetAll()
        {
            foreach (Gate gate in gates)
            {
                gate.Lock();
            }

            foreach (InventoryItem inventoryItem in inventoryItems)
            {
                inventoryItem.Reset();
            }
            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                spawnPoint.Reset();
            }
            foreach (KeyItem keyItem in keyItems)
            {
                keyItem.Reset();
            }
            foreach (SpellPickupBase spellPickup in spellPickups)
            {
                spellPickup.Reset();
            }
            foreach (SpellEffectBase spellEffect in spellEffects.FindAll(x => x != null))
            {
                spellEffect.EndEffect();
            }
            foreach (Projectile projectile in FindObjectsOfType<Projectile>())
            {
                Destroy(projectile, 0.01f);
            }
            //Destroy(Player.Instance);
        }

        internal void SpawnAll()
        {
            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                if (spawnPoint.isAvailable)
                { 
                    spawnPoint.SpawnCharacter(); 
                }
            }
            foreach (InventoryItem item in inventoryItems)
            {
                if (!item.isActiveAndEnabled) { item.gameObject.SetActive(true); }
            }

        }

        #region Character Management

        public void SetBoardTilt(InputValue inputValue)
        {
            Input_Acceleration = inputValue.Get<Vector2>();
        }

        internal int StoreCharacters()
        {
            Characters = new List<CharacterControl>(FindObjectsOfType<CharacterControl>());

            string charlist = "";
            foreach (var character in Characters) { charlist += $"{character.gameObject.name}, "; }
            //Debug.Log(String.Format("there are {0} characters : {1}", characters.Count,charlist));
            if (Characters.Count > 0)
            {
                return Characters.Count;
            }
            else
            {
                return -1;
            }
        }

        //return true if a character was added
        internal bool StoreCharacter(CharacterControl character)
        {
            if (Characters == null) { StoreCharacters(); }
            if (Characters.Contains(character)) { return false; }
            else
            {
                Characters.Add(character);
                return true;
            }
        }
        #endregion

        //move all characters
        public void MoveAll()
        {
            foreach (CharacterControl character in Characters.FindAll(x => x != null))
            {
                if (character.gameObject.activeInHierarchy && character.MySheet.IsBoardMovable)
                {
                    BoardMovement.Move(character.MyRigidbody, Input_Acceleration, character.ForceMultiplier);
                }
            }
        }
    }
}