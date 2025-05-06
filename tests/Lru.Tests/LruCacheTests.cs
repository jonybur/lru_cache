// Lru.Tests/LruCacheTests.cs
using Xunit;
using Lru.Core;

public class LruCacheTests
{
    [Fact]
    public void WhenCapacityExceeded_LeastRecentlyUsedIsEvicted()
    {
        var cache = new LruCache<int,string>(2);
        cache.AddOrUpdate(1,"A");
        cache.AddOrUpdate(2,"B");
        cache.TryGet(1, out _);    // access 1, so 2 is LRU
        cache.AddOrUpdate(3,"C"); // should evict key=2
        Assert.False(cache.TryGet(2, out _));
    }
}
