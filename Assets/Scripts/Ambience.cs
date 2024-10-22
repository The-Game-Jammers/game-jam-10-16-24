using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ambience : MonoBehaviour
{
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] AudioClip bubbles;
    [SerializeField] AudioClip rumble;
    [SerializeField] AudioSource audioSource2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rumble());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Rumble()
    {
        float waitTime = Random.Range(20, 50);
        Debug.Log(waitTime);
        yield return new WaitForSeconds(waitTime);
        audioSource2.PlayOneShot(rumble);
        StartCoroutine(Rumble());

    }
}
