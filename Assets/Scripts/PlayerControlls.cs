using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControlls : MonoBehaviour
{
    Rigidbody2D rigidLink;
    float moveInputX;
    float moveInputY;
    float velocityX;
    float velocityY;
    [SerializeField] float speedModifier = 100f;


    void Start()
    {
        rigidLink = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move() {

        velocityX = moveInputX * Time.deltaTime * speedModifier;
        velocityY = moveInputY * Time.deltaTime * speedModifier;
        rigidLink.velocity = new Vector2 (velocityX, velocityY);



    
    }
}
