using EndlessRunner;
using UnityEngine;
using UnityEngine.Pool;
public class PoolingManager<T> where T : MonoBehaviour, IPoolable
{
    public ObjectPool<T> pool;
    private T prefab;
    private Transform poolParent;
    private int defaultCapacity;
    private int maxCapacity;

    public PoolingManager(T prefab, Transform parent, int defaultCapacity, int maxCapacity)
    {
        this.prefab = prefab;
        this.poolParent = parent;
        this.defaultCapacity = defaultCapacity;
        this.maxCapacity = maxCapacity;
        InitiatePool();
    }

    private void InitiatePool()
    {
        pool = new ObjectPool<T>(CreateNewObject, GetObject, ReleaseObject, DestroyObject, false, defaultCapacity, maxCapacity);
    }

    private T CreateNewObject()
    {
        T newObj = Object.Instantiate(prefab, poolParent);
        return newObj;
    }
    private void GetObject(T obj)
    {
        obj.gameObject.SetActive(true);
        obj.OnGet();
    }
    private void ReleaseObject(T obj)
    {
        obj.OnRelease();
        obj.gameObject.SetActive(false);
    }
    private void DestroyObject(T obj)
    {
        obj.OnDestroy();
        Object.Destroy(obj.gameObject);
    }
}


