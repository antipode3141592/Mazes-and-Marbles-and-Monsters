using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    //Characters are spawned when Player enters the trigger area
    //  For spawning with an animation, 
    public class TriggeredSpawnPoint : SpawnPoint
    {
        [SerializeField] MMFeedbacks triggerAreaFeedbacks;
        [SerializeField] MMFeedbacks openingFeedbacks;
        [SerializeField] MMFeedbacks closingFeedbacks;
        [SerializeField] Collider2D triggerCollider;
        [SerializeField] float spawningDelay = 0.25f;
        [SerializeField] float resettingDelay = 0.25f;
        [SerializeField] string animationOpenTriggerName = "Open";
        [SerializeField] string animationCloseTriggerName = "Close";

        int aOpen;
        int aClose;

        protected override void Awake()
        {
            base.Awake();
            aOpen = Animator.StringToHash(animationOpenTriggerName);
            aClose = Animator.StringToHash(animationCloseTriggerName);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !collision.isTrigger)
            {
                animator.SetTrigger(aOpen);
                triggerCollider.enabled = false;
                triggerAreaFeedbacks.StopFeedbacks();
                isAvailable = true;
                StartCoroutine(Open(spawningDelay));
            }
        }

        public override void Reset()
        {
            base.Reset();
            animator.SetTrigger(aClose);
            StartCoroutine(Close(resettingDelay));
        }

        IEnumerator Open(float waitTime)
        {
            openingFeedbacks.PlayFeedbacks();
            yield return new WaitForSeconds(waitTime);
            SpawnCharacter();
        }

        IEnumerator Close(float waitTime)
        {
            closingFeedbacks.PlayFeedbacks();
            yield return new WaitForSeconds(waitTime);
            isAvailable = false;
            triggerCollider.enabled = true;
            triggerAreaFeedbacks.PlayFeedbacks();
        }
    }
}