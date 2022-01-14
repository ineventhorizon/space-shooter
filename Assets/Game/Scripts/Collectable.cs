using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public bool isDropped = false;
    public float speed = 0.5f;

    private void OnDisable()
    {
        this.isDropped = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isDropped)
        {
            HandleDropMovement();
        }
    }

    private void HandleDropMovement()
    {
        transform.position += Time.deltaTime*speed*Vector3.down;
        HandleLifeTime();
    }

    private void HandleLifeTime()
    {
        if (transform.position.y <= -5.5f) gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InGameScreen.Instance.UpdateScore(20);
            Observer.upgradeWeaponLevel?.Invoke();
            this.gameObject.SetActive(false);

        }
    }

}
