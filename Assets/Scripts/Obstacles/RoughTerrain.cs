using UnityEngine;

namespace MarblesAndMonsters.Tiles
{
    public class RoughTerrain : MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D collision)
        {
            collision.attachedRigidbody.AddForce(-1.0f * collision.attachedRigidbody.velocity * collision.attachedRigidbody.mass);
        }
    }
}