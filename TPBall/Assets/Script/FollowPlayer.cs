using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    private Transform playerTrn, trn;
    private bool notDead, stopCamera;
    [SerializeField] public float cameraSpeed, cameraSpeedAdd;
    [SerializeField] public GameObject right, left;
    // Start is called before the first frame update
    IEnumerator checkPlayerPosition()
    {
        while (true)
        {
            yield return new WaitUntil(() => playerTrn.position.y - trn.position.y > 0.4f);
            //yield return new WaitUntil(() => playerTrn.position.y - trn.position.y < 0);
            cameraSpeed+= cameraSpeedAdd/30*Time.deltaTime;
        }
    }
    void Start()
    {
        cameraSpeed = cameraSpeed / 10;
        Player = GameObject.FindGameObjectWithTag("Player");
        notDead = Player.GetComponent<PlayerController>().notDead;
        playerTrn = Player.GetComponent<Transform>();
        trn = GetComponent<Transform>();
        StartCoroutine("checkPlayerPosition");
        stopCamera = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (notDead)
        {
            if (!stopCamera)
            {
                trn.position = new Vector3(trn.position.x, trn.position.y + Time.deltaTime * cameraSpeed, trn.position.z);
            }
            if (playerTrn.position.y - trn.position.y > 0)
            {
                trn.position = new Vector3(trn.position.x, trn.position.y + Time.deltaTime * (playerTrn.position.y - trn.position.y)*2, trn.position.z);
            }
            notDead = Player.GetComponent<PlayerController>().notDead;
        }
    }
    public void StopCamera()
    {
        stopCamera = true;
    }
    public void StartCamera()
    {
        stopCamera = false;
    }
}
