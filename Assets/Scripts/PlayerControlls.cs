using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using UnityEngine.Windows;


public class PlayerControlls : MonoBehaviour
{
    Rigidbody2D rigidLink;
    Vector2 moveInput;
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip flashSound;
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
    public GameObject playerLight;
    [SerializeField] Light2D lightScript;
    float elapsedTime;
    float percentComplete;
    public bool flashing;
    public float sizeInnerRadius;
    public float sizeOuterRadius;
    float health = 2; 
    [SerializeField] GameObject enemyLink;
    [SerializeField] Enemy enemyScriptLink;
    [SerializeField] double stunDistance;
    [SerializeField] Buttons buttonsScriptLink;
    [SerializeField] GameObject gameManager;
    GameManager gameManagerLink;
    
    void Start()
    {
        rigidLink = GetComponent<Rigidbody2D>();
        colliderLink = GetComponent<CircleCollider2D>();
        inputLink = GetComponent<PlayerInput>();
        transform.position = spawnLocation.transform.position;
        animatorLink = GetComponent<Animator>();
        transform.Rotate(0, 0, 0);
        sizeInnerRadius = lightScript.pointLightInnerRadius;
        sizeOuterRadius = lightScript.pointLightOuterRadius;
        Debug.Log(sizeInnerRadius + " " + sizeOuterRadius);
        gameManagerLink = gameManager.GetComponent<GameManager>();
    }

    // Calls once every fixed number of Frames
    void FixedUpdate()
    {
        Move();
        Rotate();
        elapsedTime += Time.deltaTime;
        if (flashing == true)
        {
            IncreaseLight();
        }

        if (flashing == false)
        {
            DecreaseLight();
        }

        setRadius(sizeInnerRadius, sizeOuterRadius);
    }
    public void setRadius(float innerRadius, float outRadius)
    {
        sizeInnerRadius = innerRadius;
        sizeOuterRadius = outRadius;
        if (lightScript.pointLightInnerRadius < sizeInnerRadius)
        {
            lightScript.pointLightInnerRadius += 3f * Time.deltaTime;
        }

        if (lightScript.pointLightOuterRadius < sizeOuterRadius)
        {
            lightScript.pointLightOuterRadius += 3f * Time.deltaTime;
        }
    }

    public float getInnerRadius()
    {
        Debug.Log(sizeInnerRadius);
        return sizeInnerRadius;
    }

    public float getOuterRadius()
    {
        Debug.Log(sizeOuterRadius);
        return sizeOuterRadius;
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

    public void IncreaseLight()
    {
        if (lightScript.pointLightInnerRadius < 8)
        {
            lightScript.pointLightInnerRadius += 100f * Time.deltaTime;
        }

        if (lightScript.pointLightOuterRadius < 30)
        {
            lightScript.pointLightOuterRadius += 100f * Time.deltaTime;
            lightScript.intensity = 200f;
        }

        if (lightScript.pointLightInnerRadius >= 8 && lightScript.pointLightOuterRadius >= 30)
        {
            flashing = false;
        }

    }

    public void DecreaseLight()
    {
        if (lightScript.pointLightInnerRadius > sizeInnerRadius)
        {
            lightScript.pointLightInnerRadius -= 30f * Time.deltaTime;
        }

        if (lightScript.pointLightOuterRadius > sizeOuterRadius)
        {
            lightScript.pointLightOuterRadius -= 30f * Time.deltaTime;
        }

        if (lightScript.intensity > 1)
        {
            lightScript.intensity -= 200 * Time.deltaTime;
        }
        else
        {
            lightScript.intensity = 1f;
        }

        if(lightScript.pointLightInnerRadius < sizeInnerRadius)
        {
            lightScript.pointLightInnerRadius = sizeInnerRadius;
        }

        if(lightScript.pointLightOuterRadius < sizeOuterRadius)
        {
            lightScript.pointLightOuterRadius = sizeOuterRadius;
        }
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

    public void OnFlash()
    {
        if (getInnerRadius() != 0 && getOuterRadius() != 0 && flashing == false)
        {
            playerAudio.PlayOneShot(flashSound, 0.7f);
            float flashDuration = 3f;
            percentComplete = elapsedTime / flashDuration;
            sizeInnerRadius = lightScript.pointLightInnerRadius - 0.25f;
            sizeOuterRadius = lightScript.pointLightOuterRadius - 1f;
            flashing = true;
            stun();
            
        }
    }

    private void stun()
    {
        double playerX = transform.position.x;
        double playerY = transform.position.y;
        double distance = Math.Sqrt(Math.Pow((playerX - enemyScriptLink.getEnemyX()), 2.0d) + Math.Pow((playerY - enemyScriptLink.getEnemyY()), 2.0d));
        Debug.Log("Distance: " + distance);
        if (distance <= stunDistance)
        {
            enemyScriptLink.isStuned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitTag = collision.gameObject.tag;
        if (hitTag == "End Point")
        {
            buttonsScriptLink.GameWin();
            gameManagerLink.GameWon();
        }
        else if(hitTag == "Fuel Cell")
        {
            setRadius(sizeInnerRadius + 0.25f, sizeOuterRadius + 1f);
        }   
        else if (hitTag == "Enemy")
        {
            health -= 1;
            Debug.Log("Ow");
        }
    }


}
