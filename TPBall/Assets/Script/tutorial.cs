using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public bool yeet=false;
    /*void Awake()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetFloat("tutorialDone", 0) == 0)
        {
            PlayerPrefs.SetFloat("tutorialDone", 1);
            Time.timeScale = 0;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }*/
    private void OnEnable()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetFloat("tutorialDone", 0) == 0||yeet)
        {
            PlayerPrefs.SetFloat("tutorialDone", 1);
            Time.timeScale = 0;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        Time.timeScale = 1;
    }
    // Update is called once per frame
    public void startGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void enableThis()
    {
        yeet = true;
        gameObject.SetActive(true);
    }
}
