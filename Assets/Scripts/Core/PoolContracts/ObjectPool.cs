using System.Collections.Generic;
using UnityEngine;

namespace Services.PoolContracts
{
    public abstract class ObjectPool<T> : MonoBehaviour, IPool<T> where T : Component
    {
        [SerializeField] private T _prefab;
        [SerializeField] private float _numberToSpawnAtStart;

        private Queue<T> _objects = new Queue<T>();

        private void Start()
        {
            for (int i = 0; i < _numberToSpawnAtStart; i++)
            {
                AddObject();
            }
        }

        public T Get()
        {
            if (_objects.Count == 0)
                AddObject();
            return _objects.Dequeue();
        }

        public void ReturnToPool(T objectToReturn)
        {
            objectToReturn.gameObject.SetActive(false);
            _objects.Enqueue(objectToReturn);
        }

        private void AddObject()
        {
            var newObject = Instantiate(_prefab, gameObject.transform);
            newObject.gameObject.SetActive(false);
            _objects.Enqueue(newObject);

            SetObjectPool(newObject);
        }

        protected abstract void SetObjectPool(T newObject);
    }
}