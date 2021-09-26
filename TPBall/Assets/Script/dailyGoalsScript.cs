using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class dailyGoalsScript : MonoBehaviour
{
    [SerializeField] public GameObject[] goals, goalPositions;
    [SerializeField] private float hoursGoalLasts;
    [SerializeField] public int goalNumber;
    [SerializeField] public string saveFileName;
    [HideInInspector] public bool[] activeGoalsSlots;
    [HideInInspector] public int[] activeGoals;
    [HideInInspector] public int[] goalStatus, goalFinish;
    [HideInInspector] public float[] timeTotal;
    [HideInInspector] public DateTime exitDate;
    [HideInInspector] private bool LoadedGoals;
    // Start is called before the first frame update
    private void Awake()
    {
        activeGoalsSlots = new bool[300];
        goalStatus = new int[300];
        goalFinish = new int[300];
        timeTotal = new float[300];
        activeGoals= new int[300];
        gameObject.SetActive(true);
        Time.timeScale = 1;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        if (!LoadedGoals)
        {
            LoadGoals();
            for (int i = 0; i <= goalNumber - 1; i++)
            {
                instantiateGoal(i);
            }
            LoadedGoals = true;
        }
    }
    private void OnDisable()
    {
        SaveGoals();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i <= goalNumber-1; i++)
        {
            if (timeTotal[i] <= 0)
            {
                //remove goal
                removeGoal(goalPositions[i], i);
                //add new goal
                addNewGoal(goalPositions[i], i, gameObject);
                //Renew Goal
                timeTotal[i] = hoursGoalLasts * 60 * 60;
            }
            else if(timeTotal[i]>=9000000000)
            {
                //goal done, waiting for award retrieval
            }
            else
            {
                timeTotal[i] = timeTotal[i] - Time.deltaTime;
            }
        }
    }
    public void LoadGoals()
    {
        if (File.Exists(Application.persistentDataPath + "/" + saveFileName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/"+ saveFileName + ".dat", FileMode.Open);
            GoalsData data = (GoalsData)bf.Deserialize(file);

            activeGoalsSlots = data.activeGoalsSlots;
            activeGoals = data.activeGoals;
            goalStatus = data.goalStatus;
            goalFinish = data.goalFinish;
            timeTotal = data.timeTotal;
            exitDate = data.exitDate;
            file.Close();
            for (int i = 0; i <= goalNumber - 1; i++)
            {
                DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                TimeSpan deltaDate = now - exitDate;
                /*int exitTimeDayOfYear = exitDate.DayOfYear;
                int nowDayOfTheYear = now.DayOfYear;
                if (nowDayOfTheYear - exitTimeDayOfYear >= 24&&timeTotal[i]!= 9000000000)
                {
                    timeTotal[i] = 0;
                }
                else if (nowDayOfTheYear - exitTimeDayOfYear<0 && timeTotal[i] != 9000000000)
                {
                    timeTotal[i] = 0;
                }
                 else
                    {
                        timeTotal[i] = timeTotal[i] - (((now.Hour - exitDate.Hour) * 60 + (now.Minute - exitDate.Minute)) * 60 + now.Second - exitDate.Second);
                    }
                */
                if (timeTotal[i] != 9000000000)
                {
                    if (0 >= timeTotal[i] - deltaDate.Seconds)
                    {
                        timeTotal[i] = 0;
                    }
                    else
                    {
                        timeTotal[i] = timeTotal[i] - Convert.ToSingle(deltaDate.TotalSeconds);
                    }
                }
            }
        }
        else
        {
            exitDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            activeGoalsSlots = new bool[300];
            activeGoals = new int[300];
            goalStatus = new int[300];
            goalFinish = new int[300];
            timeTotal = new float[300];
            for (int i = 0; i <= goalNumber - 1; i++)
            {
                //remove goal
                removeGoal(goalPositions[i], i);
                //add new goal
                addNewGoal(goalPositions[i], i, gameObject);
                //Renew Goal
                timeTotal[i] = hoursGoalLasts * 60 * 60;
            }
        }
    }
    public void SaveGoals()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + saveFileName + ".dat");

        GoalsData data = new GoalsData();
        data.exitDate = exitDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        data.activeGoalsSlots= activeGoalsSlots;
        data.activeGoals= activeGoals;
        data.goalStatus= goalStatus;
        data.goalFinish= goalFinish;
        data.timeTotal= timeTotal;
        bf.Serialize(file, data);
        file.Close();
    }
    public void addNewGoal(GameObject position, int positionInVector, GameObject parent)
    {
        //add new goal
        int aux = UnityEngine.Random.Range(0, goals.Length-1);
        GameObject goal = Instantiate(goals[aux], position.GetComponent<Transform>().localPosition, Quaternion.Euler(0,0,0), parent.GetComponent<Transform>());
        activeGoalsSlots[positionInVector] = true;
        activeGoals[positionInVector] = aux;
        goal.GetComponent<goalScript>().posInVector = positionInVector;
        goal.GetComponent<goalScript>().CurrentValues.y = -1;
    }
    public void instantiateGoal(int posInVector)
    {
        if (activeGoalsSlots[posInVector])
        {
            GameObject goal=Instantiate(goals[activeGoals[posInVector]], goalPositions[posInVector].GetComponent<Transform>().position, Quaternion.Euler(0, 0, 0), gameObject.GetComponent<Transform>());
            goal.GetComponent<goalScript>().posInVector = posInVector;
        }
        else
        {
            addNewGoal(goalPositions[posInVector], posInVector, gameObject);
        }
    }
    public void removeGoal(GameObject position, int positionInVector)
    {
        activeGoalsSlots[positionInVector] = false;
        goalStatus[positionInVector] = 0;
        timeTotal[positionInVector] = hoursGoalLasts * 60*60;
        activeGoals[positionInVector] = -1;
    }

}
[Serializable]
class GoalsData
{
    [HideInInspector] public bool[] activeGoalsSlots;
    [HideInInspector] public int[] goalStatus, goalFinish;
    [HideInInspector] public float[] timeTotal;
    [HideInInspector] public int[] activeGoals;
    [HideInInspector] public DateTime exitDate;
}