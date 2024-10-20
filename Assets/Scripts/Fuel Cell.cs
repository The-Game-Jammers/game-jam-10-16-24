using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCell : MonoBehaviour
{
    [SerializeField] GameObject playerLink;
    [SerializeField] PlayerControlls playerControlls;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitTag = collision.tag;
        if(hitTag == "Player")
        {
           
        }
    }

}
