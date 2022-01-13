using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private int weaponLevel = 0;
    [SerializeField] private float weaponDamage = 20;
    [SerializeField] private float radius = 1f;
    [SerializeField] private int maxWeaponLevel = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        Observer.upgradeFireRate += UpgradeFireRate;
        Observer.upgradeWeaponLevel += UpgradeWeaponLevel;
        Observer.handleShooting += Shooting;
    }

    private void OnDisable()
    {
        Observer.upgradeFireRate -= UpgradeFireRate;
        Observer.upgradeWeaponLevel -= UpgradeWeaponLevel;
        Observer.handleShooting -= Shooting;
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
        weaponLevel = weaponLevel != maxWeaponLevel ? weaponLevel + 1 : weaponLevel;
    }
    private void Shooting()
    {
        if(GameManager.Instance.currentGameState == GameState.Gameplay)
        {
           StartCoroutine(ShootProjectileRoutine());
        } 
        else
        {
            StopCoroutine(ShootProjectileRoutine());
        }
    }

    private IEnumerator ShootProjectileRoutine()
    {
        while (true)
        {
            if (GameManager.Instance.currentGameState != GameState.Gameplay) break;
            yield return new WaitForSeconds(1/fireRate);
            ShootProjectile();
        }
        
    }
}
