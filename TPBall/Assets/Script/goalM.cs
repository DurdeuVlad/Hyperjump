using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class goalM : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public bool checkMoney, checkHighscore, checkDeath, checkScore, checkBuy;
    [HideInInspector] public int posInVector;
    [HideInInspector] public string saveFileName;
    [SerializeField] private GameObject setup, dailyGoalsHub, score;
    private int startingValue;
    //[HideInInspector] public bool[] activeGoalsSlots;
    [HideInInspector] public int[] goalStatus, goalFinish;
    //[HideInInspector] public float[] timeTotal;
    //[HideInInspector] public int[] activeGoals;
    //[HideInInspector] public DateTime exitDate;
    void Awake()
    {
        setup = GameObject.FindGameObjectWithTag("Setup");
        dailyGoalsHub = setup.GetComponent<Setup>().dailyGoalsHub;
        score = setup.GetComponent<Setup>().score;
        //posInVector = int.Parse(gameObject.name);
        saveFileName = dailyGoalsHub.GetComponent<dailyGoalsScript>().saveFileName;
        LoadGoals();
        bool aux = dailyGoalsHub.activeSelf;
        if (!dailyGoalsHub.activeSelf)
        {
            dailyGoalsHub.SetActive(true);
        }
        if (checkMoney)
        {
            startingValue = setup.GetComponent<Setup>().Money;
        }
        else if (checkHighscore)
        {
            startingValue = Mathf.RoundToInt(setup.GetComponent<Setup>().highScore);
        }
       // else if (checkBuy)
        //{
            //score = GameObject.FindGameObjectWithTag("Score");
           // startingValue =
        //}
        else if (checkScore)
        {
            startingValue = 0;
        }
        if (!aux)
        {
            dailyGoalsHub.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (checkMoney)
        {
            if (startingValue < setup.GetComponent<Setup>().Money)
            {
                goalStatus[posInVector]++;
                startingValue++;
            }
        }
        else if(checkHighscore)
        {
            if (startingValue < Mathf.RoundToInt(setup.GetComponent<Setup>().highScore))
            {
                goalStatus[posInVector]++;
                startingValue++;
            }
        }
        else if (checkScore)
        {
            if(startingValue< score.GetComponent<score>().hs)
            {
                goalStatus[posInVector]++;
                startingValue++;
            }
        }
        //dailyGoalsHub.GetComponent<dailyGoalsScript>().goalStatus = goalStatus;
        SaveGoals();
    }
    private void LoadGoals()
    {
        goalStatus=dailyGoalsHub.GetComponent<dailyGoalsScript>().goalStatus;
        goalFinish = dailyGoalsHub.GetComponent<dailyGoalsScript>().goalFinish;
    }
    public void LoadGoalsFromFile()
    {
        if (File.Exists(Application.persistentDataPath + "/" + saveFileName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + saveFileName + ".dat", FileMode.Open);
            PlayerDataGoals data = (PlayerDataGoals)bf.Deserialize(file);

            //activeGoalsSlots = data.activeGoalsSlots;
            //activeGoals = data.activeGoals;
            goalStatus = data.goalStatus;
            goalFinish = data.goalFinish;
            //timeTotal = data.timeTotal;
            DateTime exitDate = data.exitDate;
            file.Close();
        }
    }
    public void SaveGoals()
    {
        if (!dailyGoalsHub.activeSelf)
        {
            dailyGoalsHub.SetActive(true);
            dailyGoalsHub.GetComponent<dailyGoalsScript>().goalStatus[posInVector] = goalStatus[posInVector];
            dailyGoalsHub.SetActive(false);
        }
        else
        {
            dailyGoalsHub.GetComponent<dailyGoalsScript>().goalStatus[posInVector] = goalStatus[posInVector];
        }
    }
    private void OnDestroy()
    {
        if (checkDeath)
        {
            goalStatus[posInVector]++;
        }
    }
}
[Serializable]
class PlayerDataGoals
{
    [HideInInspector] public bool[] activeGoalsSlots;
    [HideInInspector] public int[] goalStatus, goalFinish;
    [HideInInspector] public float[] timeTotal;
    [HideInInspector] public int[] activeGoals;
    [HideInInspector] public DateTime exitDate;
}
