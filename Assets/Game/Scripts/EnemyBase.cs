using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyBase
{
    float movementSpeed { get; set; }
    float health { get; set; }

    void TakeDamage(float damage);

    void Die();

    void DropCollectable();

}
