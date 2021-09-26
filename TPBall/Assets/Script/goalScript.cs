using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goalScript : MonoBehaviour
{
    [SerializeField] public string Title;
    [SerializeField] public bool checkMoney, checkHighscore, checkDeath, checkScore, checkBuy;
    [SerializeField] private GameObject goalMeasurement;
    [SerializeField] private Transform statusBar;
    [SerializeField] private Text statusText, title, timeLeft, rewardText;
    [SerializeField] public Vector2 MaxValues;
    [SerializeField] private GameObject dailyGoalScriptHolder;
    [SerializeField] public Vector2 CurrentValues;
    [SerializeField] public int posInVector;
    [SerializeField] public int reward;
    private GameObject goalMeasurementHolder;
    private bool spawnGoalMesurement=false;
    void OnEnable()
    {
        dailyGoalScriptHolder = GameObject.FindGameObjectWithTag("DailyGoalsHub");
        if (CurrentValues.y == -1|| CurrentValues.y==0 || dailyGoalScriptHolder.GetComponent<dailyGoalsScript>().activeGoals[posInVector]==-1)
        {
            CurrentValues.y = Random.Range(MaxValues.x, MaxValues.y);
            CurrentValues.x = 0;
            dailyGoalScriptHolder.GetComponent<dailyGoalsScript>().goalStatus[posInVector] = Mathf.RoundToInt(CurrentValues.x);
            dailyGoalScriptHolder.GetComponent<dailyGoalsScript>().goalFinish[posInVector] = Mathf.RoundToInt(CurrentValues.y);
            statusBar.localScale = new Vector3(0, statusBar.localScale.y, 1);
        }
        else
        {
            CurrentValues.x = dailyGoalScriptHolder.GetComponent<dailyGoalsScript>().goalStatus[posInVector];
            CurrentValues.y = dailyGoalScriptHolder.GetComponent<dailyGoalsScript>().goalFinish[posInVector];
        }
        if (spawnGoalMesurement != true)
        {
            GameObject mesurement = Instantiate(goalMeasurement);
            goalMeasurementHolder = mesurement;
            spawnGoalMesurement = true;
            mesurement.name = "mesurement_"+posInVector.ToString();
            mesurement.GetComponent<goalM>().posInVector = posInVector;
            mesurement.GetComponent<goalM>().checkMoney = checkMoney;
            mesurement.GetComponent<goalM>().checkHighscore = checkHighscore;
            mesurement.GetComponent<goalM>().checkDeath = checkDeath;
            mesurement.GetComponent<goalM>().checkBuy = checkBuy;
            mesurement.GetComponent<goalM>().saveFileName = dailyGoalScriptHolder.GetComponent<dailyGoalsScript>().saveFileName;
        }
        statusText.text = CurrentValues.x + "/" + CurrentValues.y;
        rewardText.text = reward.ToString();
        title.text = Title;
    }
    // Update is called once per frame
    void Update()
    {
        if (CurrentValues.x != 0)
        {
            if (statusBar.localScale.x <= dailyGoalScriptHolder.GetComponent<dailyGoalsScript>().goalStatus[posInVector] * 1 / dailyGoalScriptHolder.GetComponent<dailyGoalsScript>().goalStatus[posInVector])
            {
                //update statusbar
                statusBar.localScale = new Vector3(dailyGoalScriptHolder.GetComponent<dailyGoalsScript>().goalStatus[posInVector] * 1 / dailyGoalScriptHolder.GetComponent<dailyGoalsScript>().goalStatus[posInVector], statusBar.localScale.y, 1);
                statusText.text = CurrentValues.x + "/" + CurrentValues.y;

            }
        }
        float timer = dailyGoalScriptHolder.GetComponent<dailyGoalsScript>().timeTotal[posInVector];
        timer = Mathf.RoundToInt(timer);
        timer = timer / 60;
        string hours = (timer / (60)).ToString("00");
        string minutes = Mathf.Floor(timer % (24)).ToString("00");
        timeLeft.text = hours + ":" + minutes + " time left.";
    }
}
