using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        
        StartCoroutine("waitALittle");
    }

    IEnumerator waitALittle()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSecondsRealtime(0.2f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSecondsRealtime(0.1f);
        GetComponent<Animator>().enabled = true;
        
        yield return new WaitForSecondsRealtime(0.1f);
    }

}
