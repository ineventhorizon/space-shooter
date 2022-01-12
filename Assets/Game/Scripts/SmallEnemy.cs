using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : MonoBehaviour, EnemyBase
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] public float motionRange;
    //true if alive, false if dead
    private bool status = true;
    
    
    private Vector2 center;

    public float movementSpeed { get; set; }
    public float health { get; set; }

    private void OnEnable()
    {

        movementSpeed = enemyData.movementSpeed;
        health = enemyData.enemyHealth;
        center = this.transform.position;
    }

    private void OnDisable()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        var pos = transform.position;
        pos.x = center.x + Mathf.Sin(Time.time) * motionRange;
        //pos.y = center.y + Mathf.Sin(Time.time) * motionRange;
        pos.y -= movementSpeed*Time.deltaTime;
        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            
        }

    }

    public void TakeDamage(float damage)
    {
        if (!status) return;
        health -= damage;
        if (health <= 0) Die();
    }

    public void Die()
    {
        status = false;
        DropCollectable();
        enemyAnimator.SetBool("IsDead", true);
        Destroy(gameObject, 1);
    }

    public void DropCollectable()
    {
        int random = Random.Range(0, 5);
        var dropCollectable = random == 1;
        if (dropCollectable)
        {
            var collectable = ObjectPooler.Instance.GetPooledCollectable();
            collectable.isDropped = true;
            collectable.transform.position = transform.position;
            collectable.gameObject.SetActive(true);
        }
    }
}
