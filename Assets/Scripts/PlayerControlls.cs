using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControlls : MonoBehaviour
{
    Rigidbody2D rigidLink;
    Vector2 moveInput;
    float moveInputX;
    float moveInputY;
    float velocityX;
    float velocityY;
    [SerializeField] float speedModifier = 100f;
    [SerializeField] GameObject spawnLocation;
    CircleCollider2D colliderLink;
    PlayerInput inputLink;
    [SerializeField] float speedX;
    [SerializeField] float speedY; 


    void Start()
    {
        rigidLink = GetComponent<Rigidbody2D>();
        colliderLink = GetComponent<CircleCollider2D>();
        inputLink = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move() {

        velocityX = moveInputX * Time.deltaTime * speedModifier;
        velocityY = moveInputY * Time.deltaTime * speedModifier;
        speedX = moveInputX;
        speedY = moveInputY;
        rigidLink.velocity = new Vector2 (velocityX, velocityY);
        transform.position = spawnLocation.transform.position;



    
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        moveInputX = moveInput.x;
        moveInputY = moveInput.y;
    }
}
