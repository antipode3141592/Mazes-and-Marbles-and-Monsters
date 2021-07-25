using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBubble : MonoBehaviour
{
    public CircleCollider2D circleCollider2D;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public bool inUse;

    public void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D.enabled = false;
        spriteRenderer.enabled = false;
    }
    
    public void Activate(float delayTime)
    {
        inUse = true;
        circleCollider2D.enabled = true;
        spriteRenderer.enabled = true;
        StartCoroutine(PowerTimer(delayTime));
    }

    IEnumerator PowerTimer(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        circleCollider2D.enabled = false;
        spriteRenderer.enabled = false;
    }

}
