using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargeSoundHandler : MonoBehaviour
{
    private GameObject setup;
    private AudioClip[] chargeSounds;
    private int Index;

    void OnEnable()
    {
        setup = GameObject.FindGameObjectWithTag("Setup");
        chargeSounds = setup.GetComponent<Setup>().charge;
        Index = chargeSounds.Length - 1;
        gameObject.GetComponent<AudioSource>().clip = chargeSounds[Random.Range(0, Index)];
        gameObject.GetComponent<AudioSource>().Play();
    }

}
