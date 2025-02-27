using UnityEngine;
using UnityEngine.Pool;
namespace MoheyPoolingSystem
{
    public class PoolingManager<T> where T : MonoBehaviour, IPoolable
    {
        public ObjectPool<T> pool;
        PoolingData<T> poolingData;

        public PoolingManager(PoolingData<T> newData)
        {
            poolingData.prefabs = newData.prefabs;
            poolingData.poolParent = newData.poolParent;
            poolingData.defaultCapacity = newData.defaultCapacity;
            poolingData.maxCapacity = newData.maxCapacity;
            InitiatePool();
        }

        private void InitiatePool()
        {
            pool = new ObjectPool<T>(CreateNewObject, GetObject, ReleaseObject, DestroyObject, false, poolingData.defaultCapacity, poolingData.maxCapacity);
        }

        private T CreateNewObject()
        {
            T newObj = Object.Instantiate(poolingData.prefabs[Random.Range(0, poolingData.prefabs.Length)], poolingData.poolParent);
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
            obj.transform.parent=poolingData.poolParent;
        }
        private void DestroyObject(T obj)
        {
            obj.OnDestroy();
            Object.Destroy(obj.gameObject);
        }
    }
}