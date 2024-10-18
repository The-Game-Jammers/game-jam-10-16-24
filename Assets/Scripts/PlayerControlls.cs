using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;


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
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationThreshold;



    void Start()
    {
        rigidLink = GetComponent<Rigidbody2D>();
        colliderLink = GetComponent<CircleCollider2D>();
        inputLink = GetComponent<PlayerInput>();
        transform.position = spawnLocation.transform.position;
        animatorLink = GetComponent<Animator>();
        transform.Rotate(0, 0, 0);

    }

    // Calls once every fixed number of Frames
    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        velocityX = moveInputX * Time.deltaTime * speedModifier;
        velocityY = moveInputY * Time.deltaTime * speedModifier;
        //speedX = moveInputX;
        //speedY = moveInputY;
        rigidLink.velocity = new Vector2(velocityX, velocityY);
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

    private void Rotate()
    {
        // Only rotate if there's movement
        if (moveInputX != 0 || moveInputY != 0)
        {
            // Only rotate if the input is greater than a small threshold(to avoid jittering when input is close to zero)
            if (Mathf.Abs(moveInputX) > rotationThreshold || Mathf.Abs(moveInputY) > rotationThreshold)
            {
                // Calculate the angle based on the input direction
                float angle = Mathf.Atan2(moveInputY, moveInputX) * Mathf.Rad2Deg;

                // Apply a rotation offset to correct the orientation (90 degrees or -90 degrees)
                angle -= -90f;  // Adjust this value if necessary (e.g., -90 if the sprite is rotated another way)

                // Smooth the rotation using rotationSpeed
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
