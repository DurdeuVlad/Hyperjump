using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionHandler : MonoBehaviour
{
    private GameObject setup;
    private AudioClip[] explosionSounds;
    private int Index;

    void OnEnable()
    {
        setup = GameObject.FindGameObjectWithTag("Setup");
        explosionSounds = setup.GetComponent<Setup>().explosions;
        Index = explosionSounds.Length - 1;
        gameObject.GetComponent<AudioSource>().clip = explosionSounds[Random.Range(0, Index)];
        gameObject.GetComponent<AudioSource>().Play();
    }
}
