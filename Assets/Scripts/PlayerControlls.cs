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
    //[SerializeField] float speedX;
    //[SerializeField] float speedY; 
    [SerializeField] float averageSpeed;
    Animator animatorLink;
    


    void Start()
    {
        rigidLink = GetComponent<Rigidbody2D>();
        colliderLink = GetComponent<CircleCollider2D>();
        inputLink = GetComponent<PlayerInput>();
        transform.position = spawnLocation.transform.position;
        animatorLink = GetComponent <Animator>();
        transform.Rotate(0, 0, 0);
        
    }

    // Calls once every fixed number of Frames
    void FixedUpdate()
    {
        Move();
    }

    private void Move() {
        velocityX = moveInputX * Time.deltaTime * speedModifier;
        velocityY = moveInputY * Time.deltaTime * speedModifier;
        //speedX = moveInputX;
        //speedY = moveInputY;
        rigidLink.velocity = new Vector2 (velocityX, velocityY);
        //transform.position = spawnLocation.transform.position;
        averageSpeed = Mathf.Abs((velocityX + velocityY) / 2);
        animatorLink.SetFloat("Speed", averageSpeed);
        
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        moveInputX = moveInput.x;
        moveInputY = moveInput.y;
    }
}
