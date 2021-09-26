using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterLifetime : MonoBehaviour
{
    ParticleSystem ps;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        GetComponent<Transform>().localScale = new Vector2(0.02f, 0.02f);
        Destroy(this.gameObject, ps.startLifetime);
    }
}
