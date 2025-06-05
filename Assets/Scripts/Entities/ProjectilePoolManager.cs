using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePoolManager 
{
    private Projectile prefab;
    private IObjectPool<Projectile> objectPool;

    private bool collectionCheck;
    private int defaultCapacity;
    private int maxSize;

    public ProjectilePoolManager(Projectile prefab, bool collectionCheck = true, int defaultCapacity = 25, int maxSize = 100)
    {
        this.prefab = prefab;
        this.collectionCheck = collectionCheck;
        this.defaultCapacity = defaultCapacity;
        this.maxSize = maxSize;
        objectPool = new ObjectPool<Projectile>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);
    }

    public Projectile Get()
    {
        return objectPool.Get();
    }
    
    private Projectile CreateProjectile()
    {
        Projectile projectileInstance = GameObject.Instantiate(prefab);
        projectileInstance.ObjectPool = objectPool;
        return projectileInstance;
    }

    private void OnGetFromPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Projectile projectile)
    {
        GameObject.Destroy(projectile.gameObject);
    }
}
