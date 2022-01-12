using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private int weaponLevel = 0;
    [SerializeField] private float weaponDamage = 20;
    [SerializeField] private float radius = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootProjectileRoutine());
    }
    private void OnEnable()
    {
        Observer.upgradeFireRate += UpgradeFireRate;
        Observer.upgradeWeaponLevel += UpgradeWeaponLevel;
    }

    private void OnDisable()
    {
        Observer.upgradeFireRate -= UpgradeFireRate;
        Observer.upgradeWeaponLevel -= UpgradeWeaponLevel;
    }
        // Update is called once per frame
        void Update()
    {
        
    }

    private void ShootProjectile()
    {
        var step = weaponLevel == 0 ? 90f : 180 / (weaponLevel);
        var angle = weaponLevel == 0 ? 90f: 0f;
        var center = transform.position;
        center.y -= weaponLevel*radius;
        for(int i=0; i<weaponLevel+1; i++)
        {
            var projectile = ObjectPooler.Instance.GetPooledProjectile();
            if (projectile != null)
            {
                projectile.isShooting = true;
                projectile.gameObject.SetActive(true);
                var newPos = new Vector2((radius*weaponLevel)* Mathf.Cos(angle*Mathf.Deg2Rad), (radius*weaponLevel) * Mathf.Sin (angle * Mathf.Deg2Rad));
                angle += step;
                projectile.transform.position =(Vector2)center + newPos;
                projectile.projectileDamage = weaponDamage;
            }
        }
    }

    public void UpgradeFireRate()
    {
        fireRate++;
    }

    public void UpgradeWeaponLevel()
    {
        weaponLevel++;
    }

    private IEnumerator ShootProjectileRoutine()
    {
        Debug.Log("test");
        while (true)
        {
            yield return new WaitForSeconds(1/fireRate);
            ShootProjectile();
        }
        
    }
}
