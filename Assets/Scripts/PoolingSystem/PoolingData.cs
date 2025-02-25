using UnityEngine;
namespace MoheyPoolingSystem
{
    [System.Serializable]
    public struct PoolingData<T>
    {
        public T[] prefabs;
        public Transform poolParent;
        public int defaultCapacity;
        public int maxCapacity;

        public PoolingData(T[] prefabs, Transform poolParent, int defaultCapacity, int maxCapacity)
        {
            this.prefabs = prefabs;
            this.poolParent = poolParent;
            this.defaultCapacity = defaultCapacity;
            this.maxCapacity = maxCapacity;
        }
    }
}