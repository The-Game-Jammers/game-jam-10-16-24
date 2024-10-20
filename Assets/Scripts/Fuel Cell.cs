using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCell : MonoBehaviour
{
    [SerializeField] GameObject playerLink;
    [SerializeField] PlayerControlls playerControlls;
    [SerializeField] float iN;
    [SerializeField] float oN;
    float inner;
    float outer;

    private void Start()
    {
        playerControlls = playerLink.GetComponent<PlayerControlls>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        var hitTag = collision.tag;
        if(hitTag == "Player")
        {
            playerControlls.setRadius((playerControlls.getInnerRadius() + iN), (playerControlls.getOuterRadius() + oN));
            Destroy(gameObject);
        }
    }

}
