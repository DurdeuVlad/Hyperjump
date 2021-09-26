using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamemodeButton : MonoBehaviour
{
    public GameObject setup;
    public Text changeGamemode;
    [HideInInspector] public bool public_flag_HighscoreMode;
    // Start is called before the first frame update
    // Update is called once per frame
    void FixedUpdate()
    {
        public_flag_HighscoreMode = setup.GetComponent<Setup>().public_flag_HighscoreMode;

        if (public_flag_HighscoreMode)
        {
            changeGamemode.text = "highscore mode";

        }
        else
        {
            changeGamemode.text = "level mode";
        }
    }
}
