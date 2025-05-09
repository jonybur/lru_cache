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
            value = default!;
            return false;
        }

        public void AddOrUpdate(TKey key, TValue value)
        {
        }

        public bool Delete(TKey key)
        {
            return false;
        }
    }
}
