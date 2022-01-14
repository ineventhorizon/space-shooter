using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float projectileSpeed;
    [SerializeField] public float projectileDamage;
    public bool isShooting = false;
    public bool hasCollide = false;


    private void OnEnable()
    {
        //StartCoroutine(LifeTimeRoutine());
    }

    private void OnDisable()
    {
        //StopCoroutine(LifeTimeRoutine());
        //this.isShooting = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isShooting)
        {
            transform.position += Time.deltaTime * projectileSpeed*Vector3.up;
            HandleLifeTime();
        }
    }

    private void HandleLifeTime()
    {
        if (transform.position.y >= 5.1f) 
        {
            gameObject.SetActive(false);
            this.isShooting = false;
        }
    }

    private IEnumerator LifeTimeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            gameObject.SetActive(false);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isShooting && collision.CompareTag("Enemy"))
        {
            StopCoroutine(LifeTimeRoutine());
            collision.gameObject.GetComponent<EnemyBase>().TakeDamage(projectileDamage);
            gameObject.SetActive(false);
        }
    }
}
