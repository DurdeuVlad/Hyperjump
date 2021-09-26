using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float rotationSpeed, speedDownwards, scale, deleteBlockHeight;
    private Transform playerTrn;
    [SerializeField] public bool tutorialAsteriod;
    // Start is called before the first frame update
    IEnumerator checkPlayerPosition()
    {
        yield return new WaitUntil(() => GetComponent<Transform>().position.y+deleteBlockHeight+GetComponent<Transform>().localScale.y <= playerTrn.position.y);
        Destroy(gameObject);
    }
    void Start()
    {
        playerTrn = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        deleteBlockHeight = GameObject.Find("Setup").GetComponent<Setup>().deleteBlockHeight;
        gameObject.GetComponent<Transform>().localScale = new Vector3(scale, scale, 1);
        StartCoroutine("checkPlayerPosition");
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Transform>().position = new Vector2(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y-speedDownwards*Time.deltaTime);
        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, gameObject.GetComponent<Transform>().rotation.eulerAngles.z+ rotationSpeed*Time.deltaTime);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && tutorialAsteriod)
        {
            PlayerPrefs.SetInt("LongJumpTutorial", 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.tag == "Player"&& tutorialAsteriod)
        {
            PlayerPrefs.SetInt("LongJumpTutorial", 1);
        }
        if (collision.gameObject.tag == "Kill")
        {
            rotationSpeed = collision.gameObject.GetComponent<rotate>().rotationSpeed;
            speedDownwards = collision.gameObject.GetComponent<rotate>().speedDownwards;
            Debug.Log("Asteroid hit trigger");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kill")
        {
            rotationSpeed = collision.gameObject.GetComponent<rotate>().rotationSpeed;
            speedDownwards = collision.gameObject.GetComponent<rotate>().speedDownwards;
            Debug.Log("Asteroid hit");
        }
    }
}
