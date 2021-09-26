using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class releaseSoundHandler : MonoBehaviour
{
    private GameObject setup;
    public AudioClip[] releaseSounds;
    public int Index;

    void Start()
    {
        setup = GameObject.FindGameObjectWithTag("Setup");
        releaseSounds = setup.GetComponent<Setup>().release;
        Index = releaseSounds.Length-1;

    }
    void OnEnable()
    {
        setup = GameObject.FindGameObjectWithTag("Setup");
        releaseSounds = setup.GetComponent<Setup>().release;
        Index = releaseSounds.Length - 1;
        gameObject.GetComponent<AudioSource>().clip = releaseSounds[Random.Range(0, Index)];
        gameObject.GetComponent<AudioSource>().Play();
    }
}
