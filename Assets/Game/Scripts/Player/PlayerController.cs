using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 mouseInput;
    private Vector2 prevMousePos;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator shipAnimator;
    [SerializeField] private float movementSensivity = 0.001f;
    [SerializeField] Transform leftLimit, rightLimit;
    [SerializeField] Weapon playerWeapon;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.currentGameState == GameState.Gameplay ||
            GameManager.Instance.currentGameState == GameState.BetweenScenes)
        {
            HandleInput();
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        var pos = rb.position;
        pos += mouseInput* movementSensivity;
        pos.y = Mathf.Clamp(pos.y, -4.5f, 4f);
        rb.position = pos;
        HandleShipAnimation(mouseInput.normalized.x);
        HandleLeftRightLimit();
    }

    private void HandleShipAnimation(float value)
    {
        shipAnimator.SetFloat("Direction", value);
    }

    private void HandleLeftRightLimit()
    {
        var pos = rb.position;
        if (pos.x <= leftLimit.position.x)
        {
            //Debug.Log("Left");
            pos.x = rightLimit.position.x;
            rb.position = pos;
        }
        else if(pos.x >= rightLimit.position.x)
        {
            //Debug.Log("Right");
            pos.x = leftLimit.position.x;
            rb.position = pos;
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Clicked");
            prevMousePos = Input.mousePosition;

        }
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("Swirling");
            mouseInput = Input.mousePosition;
            var newPos = mouseInput - prevMousePos;
            prevMousePos = Input.mousePosition;
            mouseInput = newPos;
        }
        if(Input.GetMouseButtonUp(0))
        {
            //Debug.Log("Released");
            mouseInput = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            shipAnimator.SetBool("Dead", true);
            GameManager.Instance.GameOver();
        }
    }
}
