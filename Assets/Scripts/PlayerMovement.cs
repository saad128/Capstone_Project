using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D playerRb;
    Vector2 movement;

    public GameObject bombPrefab;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        movement.x =  Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bombPrefab,transform.position, Quaternion.identity);
        }

    }

    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + movement * speed *Time.fixedDeltaTime);
    }
}
