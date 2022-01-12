using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //This is for projectiles
    [SerializeField] private Projectile projectileObject;
    [SerializeField] public List<Projectile> pooledProjectiles;
    [SerializeField] public int projectilePoolAmount;

    //This is for collectables
    [SerializeField] private Collectable powerUpWeaponLevel;
    [SerializeField] private Collectable powerUpFireRate;
    [SerializeField] public List<Collectable> pooledCollectables;
    [SerializeField] public int fireRatePoolAmount;
    [SerializeField] public int weaponDamagePoolAmount;
    private static ObjectPooler instance;
    public static ObjectPooler Instance => instance ?? (instance = FindObjectOfType<ObjectPooler>());
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        CreateProjectiles();
        CreateCollectables();
    }

    private void CreateProjectiles()
    {
        for(int i = 0; i < projectilePoolAmount; i++)
        {
            var obj = Instantiate(projectileObject, this.transform);
            pooledProjectiles.Add(obj);
            obj.gameObject.SetActive(false);
        }
    }

    public Projectile GetPooledProjectile()
    {

        for (int i = 0; i < pooledProjectiles.Count; i++)
        {
            if (!pooledProjectiles[i].gameObject.activeInHierarchy)
            {
                return pooledProjectiles[i];
            }
        }

        var obj = Instantiate(projectileObject, this.transform);
        pooledProjectiles.Add(obj);

        return obj;
    }

    private void CreateCollectables()
    {
        for(int i = 0; i < fireRatePoolAmount; i++)
        {
            var obj = Instantiate(powerUpFireRate, this.transform);
            pooledCollectables.Add(obj);
            obj.gameObject.SetActive(false);
        }
        for (int i = 0; i < weaponDamagePoolAmount; i++)
        {
            var obj = Instantiate(powerUpWeaponLevel, this.transform);
            pooledCollectables.Add(obj);
            obj.gameObject.SetActive(false);
        }
    }

    public Collectable GetPooledCollectable()
    {
        for (int i = 0; i < pooledCollectables.Count; i++)
        {
            if (!pooledCollectables[i].gameObject.activeInHierarchy)
            {
                return pooledCollectables[i];
            }
        }

        var obj = Instantiate(powerUpWeaponLevel, this.transform);
        pooledCollectables.Add(obj);

        return obj;
    }


}
