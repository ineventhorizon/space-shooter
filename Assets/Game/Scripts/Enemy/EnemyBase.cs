using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float health { get; set; }
    public float movementSpeed { get; set; }
    public bool startMovement = false;
    protected Vector2 center;
    //true if alive, false if dead
    protected bool status = true;
    public void TakeDamage(float damage)
    {
        if (!status) return;
        health -= damage;
        if (health <= 0) 
        {
            Die();
            EnemySpawner.Instance.enemies.Remove(gameObject);
            GameManager.Instance.NextWave();
        }
    }
    public abstract void Die();
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
    public void SetCenter(Vector2 newCenter)
    {
        center = newCenter;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //GAMEOVER
        }
        if (collision.CompareTag("Bottom"))
        {
            Die();
        }
    }

}
