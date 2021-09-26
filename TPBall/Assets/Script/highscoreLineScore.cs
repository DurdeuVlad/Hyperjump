using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscoreLineScore : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    // Start is called before the first frame update
    void Update()
    {
        if (GetComponent<TextMesh>().text != scoreText.text)
        {
            GetComponent<TextMesh>().text = scoreText.text;
        }
    }

}
