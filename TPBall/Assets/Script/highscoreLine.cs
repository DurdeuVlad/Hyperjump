using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highscoreLine : MonoBehaviour
{
    IEnumerator checkPlayerPosition()
    {
        yield return new WaitUntil(() => trn.position.y <= playerTrn.position.y);
        gameObject.SetActive(false);
    }
    private Transform playerTrn, trn;
    // Start is called before the first frame update
    private void OnEnable()
    {
        playerTrn = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        trn = GetComponent<Transform>();
        gameObject.GetComponent<Transform>().position = new Vector2(gameObject.GetComponent<Transform>().position.x, GameObject.FindGameObjectWithTag("Setup").GetComponent<Setup>().highScore+0.05f);
        StartCoroutine("checkPlayerPosition");
        if (playerTrn.position.y == 0)
        {
            gameObject.SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("Setup").GetComponent<Setup>().highScore == 0)
        {
            gameObject.SetActive(false);
        }
    }
    bool playerPos()
    {
        return (trn.position.y <= playerTrn.position.y);
    }
}
