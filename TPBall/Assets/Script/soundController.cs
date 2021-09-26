using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundController : MonoBehaviour
{
    [SerializeField] Sprite[] offOn;
    [HideInInspector] public int muteState;

    // Start is called before the first frame update
    void Start()
    {
        muteState = PlayerPrefs.GetInt("muteState", 1);
        GetComponent<Image>().sprite = offOn[muteState];
        if (muteState == 1)
        {
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = true;
        }
    }

    public void MuteSound()
    {
        if (muteState == 1)
        {
            muteState = 0;
            GetComponent<Image>().sprite= offOn[muteState];
            PlayerPrefs.SetInt("muteState", 0);
            AudioListener.pause = true;
        }
        else
        {
            muteState = 1;
            GetComponent<Image>().sprite = offOn[muteState];
            PlayerPrefs.SetInt("muteState", 1);
            AudioListener.pause = false;
        }
    }
}
