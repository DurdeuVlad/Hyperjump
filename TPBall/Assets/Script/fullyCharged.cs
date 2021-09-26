using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fullyCharged : MonoBehaviour
{
    [SerializeField] private float intervalSeconds;
    [SerializeField] private string text1= "Fully charged!", text2= "RELEASE to teleport!";
    [SerializeField] private Image chargeBar;
    [SerializeField] private Color startColor, endColor;
    private bool Stop = false;
    private Text text;
    private void OnEnable()
    {
        text = GetComponent<Text>();
        StartCoroutine("flash");
        Stop = false;
        GetComponent<Text>().color = Color.Lerp(startColor, endColor, 0.5f);

    }
    private void OnDisable()
    {
        Stop = true;
    }
    private IEnumerator flash()
    {
        GetComponent<Text>().color = startColor;
        if (PlayerPrefs.GetInt("LongJumpTutorial", 0) == 1)
        {
            text.text = text2;
        }
        else
        {
            text.text = text1;
        }
        text.enabled = true;
        yield return new WaitForSeconds(intervalSeconds);
        text.enabled = false;
        yield return new WaitForSeconds(intervalSeconds);
        if (!Stop)
        { 
          StartCoroutine("flash");
        }

    }
    private void FixedUpdate()
    {
        //GetComponent<Text>().color=chargeBar.color;
        //GetComponent<Text>().color = Color.Lerp(startColor, endColor, 0.5f);
    }
}
