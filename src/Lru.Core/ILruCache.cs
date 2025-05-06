// Lru.Core/ILruCache.cs
namespace Lru.Core
{
    public interface ILruCache<TKey, TValue>
    {
        bool TryGet(TKey key, out TValue value);
        void AddOrUpdate(TKey key, TValue value);
    }
}
