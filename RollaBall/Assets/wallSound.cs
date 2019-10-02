using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallSound : MonoBehaviour
{
    // For added audio hehe
    public AudioClip bwapClip;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        Debug.Log("HIT THE BALL?!?!?!?!");
        GameObject obj = collisionInfo.gameObject;
        if (obj.CompareTag("ball"))
            audioSource.PlayOneShot(bwapClip, 0.3f);

    }
}
