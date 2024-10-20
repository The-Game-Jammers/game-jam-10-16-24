using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCell : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerControlls playerControlls;
    void Start()
    {
        playerControlls = player.GetComponent<PlayerControlls>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitTag = collision.tag;
        if(hitTag == "Player")
        {
            playerControlls.setRadius(playerControlls.sizeInnerRadius + 0.25f, playerControlls.sizeOuterRadius + 1f);
            Destroy(gameObject);
        }
    }
}
