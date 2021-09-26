using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject askForTutorial, setup, shortJump, longJump, tutorialMeteorit, mainCamera;
    private float cameraSpeedHolder, cameraSpeedAddHolder;
    [SerializeField] private bool UseOldTutorial, showTutorial;
    [SerializeField] GameObject video1, video2;
    void Start()
    {
        if (showTutorial)
        {
            PlayerPrefs.SetInt("FirstStart", 0);
        }
        PlayerPrefs.SetInt("LongJumpTutorial", 0);
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt("LongJumpTutorial", 0);
    }
    public void ResetTutorial()
    {
        PlayerPrefs.SetInt("FirstStart", 0);
        
    }
    public void StartTutorial()
    {
        if (UseOldTutorial)
        {
            if (PlayerPrefs.GetInt("FirstStart", 0) == 0)
            {
                //Ask if the player wants tutorial
                askForTutorial.gameObject.SetActive(true);
                Time.timeScale = 0.1f;

            }
            else
            {
                tutorialMeteorit.SetActive(false);
                //Destroy this yeet
                GameObject.Destroy(gameObject);
                if (PlayerPrefs.GetInt("LongJumpTutorial", 0) == 1)
                {
                    //tutorialHandler.GetComponent<tutorialHandler>().LongJumpStop();
                    PlayerPrefs.SetInt("LongJumpTutorial", 0);
                }

            }
        }
        else
        {
            if (PlayerPrefs.GetInt("FirstStart", 0) == 0)
            {
                //show tutorial 1
                Time.timeScale = 0.0f;
                video1.SetActive(true);
                tutorialAccepted();
            }
            else
            {
                tutorialMeteorit.SetActive(false);
                //Yeet this
                GameObject.Destroy(gameObject);
                if (PlayerPrefs.GetInt("LongJumpTutorial", 0) == 1)
                {
                    //tutorialHandler.GetComponent<tutorialHandler>().LongJumpStop();
                    PlayerPrefs.SetInt("LongJumpTutorial", 0);
                }
            }
        }
    }

    // Update is called once per frame
    public void tutorialAccepted()
    {
        //Accepted tutorial - use this for tutorial
        Debug.Log("Tutorial Accepted");
        PlayerPrefs.SetInt("FirstStart", 1);
        PlayerPrefs.SetInt("LongJumpTutorial", 1);
        Destroy(askForTutorial);
        ShortJumpStart();

    }
    public void ShortJumpStart()
    {
        if (UseOldTutorial)
        {
            shortJump.SetActive(true);
        }
        Time.timeScale = 0f;
    }
    public void ShortJumpStop()
    {
        
        GameObject.Destroy(video1);
        GameObject.Destroy(shortJump);
        tutorialMeteorit.GetComponent<rotate>().speedDownwards = 0f;
        tutorialMeteorit.GetComponent<Transform>().localScale = new Vector2(1f, 1f);
        tutorialMeteorit.GetComponent<rotate>().rotationSpeed = -10f;
        PlayerPrefs.SetInt("LongJumpTutorial", 1);
        setup.GetComponent<Transform>().position = new Vector2(setup.GetComponent<Transform>().position.x, setup.GetComponent<Transform>().position.y+2);
        Time.timeScale = 1f;
    }
    public void LongJumpStart()
    {
        if (UseOldTutorial)
        {
            longJump.SetActive(true);
        }
        else
        {
            video2.SetActive(true);
        }
        Time.timeScale = 0.0f;
        mainCamera.GetComponent<FollowPlayer>().StopCamera();
        
    }
    public void LongJumpStop()
    {
        GameObject.Destroy(longJump);
        GameObject.Destroy(video2);
        Time.timeScale = 1f;
        mainCamera.GetComponent<FollowPlayer>().StartCamera();
    }
    public void tutorialRefused()
    {
        //Refused tutorial
        Debug.Log("Tutorial Refused");
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("FirstStart", 1);
        GameObject.Destroy(gameObject);
        
    }
}
