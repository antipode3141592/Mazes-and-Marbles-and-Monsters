using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    public class Marble : CharacterControl
    {
        protected override void Awake()
        {
            base.Awake();

        }
        //mables apply a touch attack to everything they collide with
        private void OnCollisionEnter2D(Collision2D other)
        {
            audioSource.clip = MySheet.baseStats.ClipHit;
            audioSource.Play(); //no matter what is struck, play the hit sound
            

            IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
            if (damagable != null) 
            {
                DealDamageTo(damagable);
            }
        }

        protected override IEnumerator DeathAnimation(DeathType deathType)
        {
            float animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            string animationName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            Debug.Log(string.Format("DeathAnimation {0} has died of {1}!  the animation named {2} takes {3:#,###.###} sec",
                gameObject.name, deathType.ToString(), animationName, animationLength));
            //Debug.Log(string.Format("DeathAnimation {0} has died of {1}!", gameObject.name, deathType.ToString()));
            yield return new WaitForSeconds(0.75f);  //death animations are 6 frames, current fps is 12
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
