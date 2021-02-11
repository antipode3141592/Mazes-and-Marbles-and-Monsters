using UnityEngine;

namespace MarblesAndMonsters.Utilities
{

    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            transform.SetParent(null);  //remove parent before tagging with dontdestroy
            Object.DontDestroyOnLoad(gameObject);
        }
    }
}