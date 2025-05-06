# LRU Cache API Exercise

This exercise sets up a minimal .NET 8 Web API and core library for implementing an LRU (Least Recently Used) cache.

## Projects

- **Lru.Api**: Minimal API entrypoint (Program.cs)
- **Lru.Core**: Class library to implement `ILruCache<TKey,TValue>`
- **Lru.Tests**: xUnit test project with a failing test stub

## Task

1. Implement `LruCache<TKey,TValue>` in **Lru.Core**:
   - Constructor: `LruCache(int capacity)`
   - Methods:
     - `bool TryGet(TKey key, out TValue value)`
     - `void AddOrUpdate(TKey key, TValue value)`
   - Eviction policy: On exceeding capacity, remove the least-recently used item.
   - Ensure both operations run in amortized **O(1)** time.

2. In **Lru.Api**, register your implementation with DI and map the following endpoints:
   - `GET /cache/{key}` → returns 200 + value or 404
   - `PUT /cache/{key}` with body `"value"` → returns 204
   - `DELETE /cache/{key}` → returns 204 or 404

3. Run and ensure tests in **Lru.Tests** pass.

## Running

```bash
cd src/Lru.Api
dotnet run
```

Use curl or Postman to exercise the endpoints.

## Test Stub

A failing test is provided to guide your implementation:

```csharp
[Fact]
public void WhenCapacityExceeded_LeastRecentlyUsedIsEvicted()
{
    var cache = new LruCache<int,string>(2);
    cache.AddOrUpdate(1,"A");
    cache.AddOrUpdate(2,"B");
    cache.TryGet(1, out _);    // access 1, so 2 is LRU
    cache.AddOrUpdate(3,"C");  // should evict key=2
    Assert.False(cache.TryGet(2, out _));
}
```

## Example Usage

### 1. Add items to the cache (capacity = 2)
```bash
curl -X PUT http://localhost:5000/cache/Key1 \
     -H "Content-Type: application/json" \
     -d '"Value1"'
# → HTTP 204 No Content

curl -X PUT http://localhost:5000/cache/Key2 \
     -H "Content-Type: application/json" \
     -d '"Value2"'
# → HTTP 204 No Content
```

### 2. Retrieve an item
```bash
curl http://localhost:5000/cache/Key1
# → HTTP 200 OK
# Response body:
# "Value1"
```

### 3. Exceed capacity and evict LRU (Key2 is now least recently used)
```bash
curl -X PUT http://localhost:5000/cache/Key3 \
     -H "Content-Type: application/json" \
     -d '"Value3"'
# → HTTP 204 No Content

curl http://localhost:5000/cache/Key2
# → HTTP 404 Not Found
```

### 4. Delete an item
```bash
curl -X DELETE http://localhost:5000/cache/Key1
# → HTTP 204 No Content

curl http://localhost:5000/cache/Key1
# → HTTP 404 Not Found
```
