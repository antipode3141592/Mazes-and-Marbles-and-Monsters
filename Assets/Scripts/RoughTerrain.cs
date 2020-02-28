using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoughTerrain : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.attachedRigidbody.AddForce(-1.0f*collision.attachedRigidbody.velocity * collision.attachedRigidbody.mass);
    }
}
