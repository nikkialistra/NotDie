using System;
using System.Collections.Generic;
using Core.StatSystem;
using UnityEngine;
using Zenject;

namespace Things.Item
{
    public class ItemFacade : MonoBehaviour, IPoolable<Data.Item, IMemoryPool>, IDisposable
    {
        [SerializeField] private Data.Item _item;
        [SerializeField] private int _currentLevel;

        public Data.Item Item => _item;

        private IMemoryPool _pool;

        public bool TryGetStatModifiers(out List<StatModifier> statModifiers)
        {
            if (!Item.HasModifiers || _item.Levels.Count <= _currentLevel)
            {
                statModifiers = null;
                return false;
            }

            statModifiers = _item.Levels[_currentLevel].StatModifiers;
            return true;
        }

        public void OnSpawned(Data.Item item, IMemoryPool pool)
        {
            _pool = pool;
            _item = item;
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public class Factory : PlaceholderFactory<Data.Item, ItemFacade>
        {
        }
    }
}