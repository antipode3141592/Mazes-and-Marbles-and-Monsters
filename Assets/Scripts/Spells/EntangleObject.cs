using MarblesAndMonsters.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Physical reprsentation of the Entangle spell on the game board.
/// Object has a circle collider trigger that checks for charcter objects that stay in its area.
///     Character objects are "held" by the vines of the entanglement. Animated vines are created at the edge of the character object
///     "Held" characters are immobile, but can still be injured
/// </summary>
public class EntangleObject : MonoBehaviour
{
    public Animator Animator;
    public CircleCollider2D CircleCollider2D;
    public GameObject VinePrefab;   //the vine object that enwraps entangled characters

    protected List<CharacterControl> entangledCharacters;
    private CharacterControl _caster;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        CircleCollider2D = GetComponent<CircleCollider2D>();
        entangledCharacters = new List<CharacterControl>();
        //vines = new List<GameObject>();
    }

    internal void SetCaster(CharacterControl characterControl)
    {
        _caster = characterControl;
    }

    public void EndEffect()
    {
        CircleCollider2D.enabled = false;
        foreach (CharacterControl character in entangledCharacters)
        {
            character.SetAnimationSpeed(1.0f);
            character.SetBodyType(RigidbodyType2D.Dynamic);
        }
        if (Animator)
        {
            Animator.SetTrigger("EndEffect");
        }
        StartCoroutine(EndEffectCleanup(0.5f));
    }

    IEnumerator EndEffectCleanup(float cleanupDelay)
    {
        yield return new WaitForSeconds(cleanupDelay);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CharacterControl character = collision.GetComponent<CharacterControl>();
        if (character && (character != _caster) && !entangledCharacters.Contains(character))
        {
            Debug.Log(string.Format("{0} is entangled!", character.name));
            character.SetAnimationSpeed(0.0f);
            character.SetBodyType(RigidbodyType2D.Static);
            //generate a Vine.  vines have this object's transform as a parent, so when it is destroyed, all vines are destroyed
            Instantiate(VinePrefab, character.transform.position, Quaternion.identity, transform);
            entangledCharacters.Add(character);
        }
    }
}