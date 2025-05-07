using System;
using System.Collections.Generic;

namespace Lru.Core
{
    public class LruCache<TKey, TValue> : ILruCache<TKey, TValue>
    {
        public LruCache(int capacity)
        {
            if (capacity < 1)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be > 0");

            Capacity = capacity;
        }

        public int Capacity { get; }

        public bool TryGet(TKey key, out TValue value)
        {
            // TODO: if found, move node to end of _lruList, set value, return true
            value = default!;
            return false;
        }

        public void AddOrUpdate(TKey key, TValue value)
        {
            // TODO: if exists, remove old node; else if full, evict LRU
            // TODO: add new node to end of _lruList and to _map
        }

        public bool Delete(TKey key)
        {
            // TODO: if exists, remove node from both _map and _lruList, return true
            return false;
        }
    }
}
