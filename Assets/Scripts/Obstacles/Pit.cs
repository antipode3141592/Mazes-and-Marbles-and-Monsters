using System.Collections;
using UnityEngine;
using MarblesAndMonsters.Characters;
using UnityEngine.Tilemaps;

namespace MarblesAndMonsters.Objects
{

    public class Pit : MonoBehaviour
    {
        public Tilemap tileMap;
        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    if (other)
        //    {
        //        //GameController.Instance.DestroyCharacter(other.gameObject);

        //        Debug.Log(string.Format("{0} has a speed of {1}", other.gameObject.name, other.attachedRigidbody.velocity.magnitude));
        //        CharacterSheetController character = other.GetComponent<CharacterSheetController>();
        //        character.CharacterDeath(DeathType.Falling);
        //    }
        //}

        private void OnTriggerStay2D(Collider2D other)
        {
            Characters.CharacterControl character = other.GetComponent<Characters.CharacterControl>();
            var position = other.transform.position;
            var tilemapPosition = tileMap.WorldToCell(position);
            Debug.Log(string.Format("Pit position World: {0}, Local: {1}", position.ToString(), tilemapPosition.ToString()));
            //other.transform.position = CenteredTilemapPosition(tilemapPosition);
            other.attachedRigidbody.MovePosition(CenteredTilemapPosition(tilemapPosition));
            //StartCoroutine(MoveOverTime(other.transform, CenteredTilemapPosition(tilemapPosition, tileMap), 6));
            character.CharacterDeath(DeathType.Falling);
        }


        //utility script to apply an offset to an input vector3int 
        public Vector3 CenteredTilemapPosition(Vector3Int tilemapPosition)
        {
            Vector3 offset = new Vector3(0.5f, 0.5f);
            return (Vector3)tilemapPosition + offset;
        }


        ////move along a vector in increments equal to frames
        //public IEnumerator MoveOverTime(Transform _transform, Vector3 finalPosition, int frames)
        //{
        //    float timeperframe = 1.0f / 12.0f;
        //    Vector3 initialPosition = _transform.position;
        //    Vector3 initialToFinalPosition = finalPosition - initialPosition;
        //    Debug.Log(string.Format("TimeStep: {0:#.###}, Initial Position: {1}, Final Position: {2}", 
        //        timeperframe, initialPosition.ToString(), initialToFinalPosition.ToString()));
        //    Vector3[] intermediatePositions = new Vector3[frames];
        //    for(int i = 0; i < frames; i++)
        //    {
        //        //float scaleFactor = i / frames;
        //        intermediatePositions[i] = timeperframe * initialToFinalPosition;
        //        if (_transform != null)
        //        {
        //            _transform.position = intermediatePositions[i];
        //        }
        //        Debug.Log(string.Format("Frame: {0}, Position: {1}", i, intermediatePositions[i].ToString()));
        //        //wait for a frame with fps 12
        //        yield return null;
        //    }

            
        //}
    }
}