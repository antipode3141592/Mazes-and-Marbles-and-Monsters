using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace MarblesAndMonsters.Pooling
{

    /// <summary>
    /// based on Matthew J Spencer's example code from https://github.com/Matthew-J-Spencer/Object-Pooler
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Pooler<T> : MonoBehaviour where T : MonoBehaviour
    {
        private T _prefab;
        private ObjectPool<T> _pool;

        private ObjectPool<T> Pool
        {
            get
            {
                if (_pool == null) throw new InvalidOperationException("You need to call InitPool before using it.");
                return _pool;
            }
            set => _pool = value;
        }

        protected void InitPool(T prefab, int initial = 10, int max = 20, bool collectionChecks = false)
        {
            _prefab = prefab;
            Pool = new ObjectPool<T>(
                CreatePooledItem,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                collectionChecks,
                initial,
                max);
        }

        #region Overrides
        protected virtual T CreatePooledItem() => Instantiate(_prefab);
        protected virtual void OnTakeFromPool(T obj) => obj.gameObject.SetActive(true);
        protected virtual void OnReturnedToPool(T obj) => obj.gameObject.SetActive(false);
        protected virtual void OnDestroyPoolObject(T obj) => Destroy(obj);
        #endregion

        #region Getters
        public T Get() => Pool.Get();
        public void Release(T obj) => Pool.Release(obj);
        #endregion
    }
}
