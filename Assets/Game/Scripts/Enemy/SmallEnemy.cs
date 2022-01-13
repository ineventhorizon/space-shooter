using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : EnemyBase
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] public float motionRange;


    private void OnEnable()
    {

        movementSpeed = enemyData.movementSpeed;
        health = enemyData.enemyHealth;
        center = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(startMovement) HandleMovement();

    }

    private void HandleMovement()
    {
        var pos = transform.position;
        pos.x = center.x + Mathf.Sin(Time.time) * motionRange;
        pos.y -= movementSpeed*Time.deltaTime;
        transform.position = pos;
    }

    public override void Die()
    {
        status = false;
        DropCollectable();
        enemyAnimator.SetBool("IsDead", true);
        Destroy(gameObject, 1);
    }
    
}
