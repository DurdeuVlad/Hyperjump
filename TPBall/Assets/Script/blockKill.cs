using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockKill : MonoBehaviour
{
    public Transform tr, playerTr;
    public float deleteBlockHeight;
    // Update is called once per frame
    void LateUpdate()
    {
        if (tr.position.y <= deleteBlockHeight)
        {
            Destroy(gameObject);
        }
    }
}
