using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blinkText : MonoBehaviour
{
    public float VisibleTime, UnvisibleTime;
    public Text _text;
    // Start is called before the first frame update
    void OnEnable()
    {
        //_text = GetComponent<Text>();
        StartCoroutine("blink");
    }

    // Update is called once per frame
    IEnumerator blink()
    {
        while (true) {
            if (_text.color != Color.white) {
                _text.color = Color.white;
                //Debug.Log("enabled");
                yield return new WaitForSeconds(VisibleTime);
            }
            else {
                _text.color = Color.clear;
                //Debug.Log("disenabled");
                yield return new WaitForSeconds(UnvisibleTime);
            }
        }


    }
}
