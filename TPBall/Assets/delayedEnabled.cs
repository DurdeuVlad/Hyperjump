using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class delayedEnabled : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine("waitALittle");
    }

    IEnumerator waitALittle()
    {
        GetComponent<Button>().enabled = false;
        yield return new WaitForSecondsRealtime(0.5f);
        GetComponent<Button>().enabled = true;
        yield return new WaitForSecondsRealtime(0.5f);
    }

}
