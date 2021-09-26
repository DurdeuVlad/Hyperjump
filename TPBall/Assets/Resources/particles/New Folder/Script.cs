using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    public ParticleSystem ss;
    private Vector3 sss;
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem ww;
        ww=Instantiate(ss, transform.parent);
    }

}
