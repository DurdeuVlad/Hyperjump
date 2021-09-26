using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    Text text;
    public Text highscoreText;
    public float hs = 0, timestamp, aux;
    [SerializeField] private Transform playerTransform;
    [HideInInspector] public float highscore;
    private bool public_flag_HighscoreMode;
    public GameObject setup;
    public bool hasMoved;
    [SerializeField] private TextMesh HighScoreBar_Text;
    [SerializeField] public float scoreMultiplier;
    //private PlayerController playerScript;
    public void hasMovedd()
    {
        hasMoved = true;
    }
    IEnumerator changeScore()
    {
        //if (public_flag_HighscoreMode)
        {
            hs = 0;
            while (true)
            {
                float auxx = 0;
                float pos = playerTransform.position.y;
                hasMoved = false;
                if (highscore <= hs)
                {
                    highscore = hs;
                    highscoreText.gameObject.SetActive(false);
                    //PlayerPrefs.SetFloat("Highscore", highscore);
                    setup.GetComponent<Setup>().highScore = highscore;
                }
                else
                {
                    highscoreText.gameObject.SetActive(true);
                }
                if (hs < 0)
                {
                    text.text = "" + 0;
                    hs = 0;
                    if (auxx < -pos)
                    {
                        auxx = -pos;
                    }
                }
                else
                {
                    hs = pos + auxx;
                }
                text.text = "" + (int)(hs * scoreMultiplier);
                highscoreText.text = "Highscore: " + (int)(highscore * scoreMultiplier);
                setup.GetComponent<Setup>().highScore = highscore;
                yield return new WaitUntil(() => hasMoved);
            }
        }
    }
    private void OnEnable()
    {
        HighScoreBar_Text.gameObject.SetActive(true);
        highscore = setup.GetComponent<Setup>().highScore;
        HighScoreBar_Text.text="Highscore: " + (int)(highscore * scoreMultiplier);
        //playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        //highscore = PlayerPrefs.GetFloat("Highscore");
        hs= setup.GetComponent<Setup>().highScore;
        text = GetComponent<Text>();
        for (int i = 0; i <= 1000; i++)
        {
            text.text = "" + i;
        }
        hs = 0;
        text.text = "" + (int)hs;
        highscoreText.text = "Highscore: " + (int)highscore;
        StartCoroutine("changeScore");
        public_flag_HighscoreMode = setup.GetComponent<Setup>().public_flag_HighscoreMode;
        /*for (int i = 5; i <= 1000; i++)
        {
            hs = i;
            text.text = "Score: " + (int)hs;
        }*/

    }/*
    private void LateUpdate()
    {
        float pos = playerTransform.position.y;
        if (pos > hs/2&&hasMoved)
        {
            hs = pos*2;
            hasMoved = false;
        }
        if (highscore <= hs)
        {
             text.text = "" + (int)hs;
             highscore = hs;
             highscoreText.gameObject.SetActive(false);
             //PlayerPrefs.SetFloat("Highscore", highscore);
        }
        else
        {
           text.text = "" + (int)hs;
           highscoreText.text = "Highscore: " + (int)highscore;
        }
    }*/
    private void OnApplicationQuit()
    {
        if (highscore <= hs)
        {
            setup.GetComponent<Setup>().highScore= highscore;
        }
    }
    private void OnDestroy()
    {
        if (highscore <= hs)
        {
            setup.GetComponent<Setup>().highScore = highscore;
        }
    }
}

