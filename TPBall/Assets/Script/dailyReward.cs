using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class dailyReward : MonoBehaviour
{
    [SerializeField] private string saveFileName;
    [SerializeField] private float waitTime;
    [SerializeField] private GameObject setup, textMoney, moneyShower;
    [SerializeField] private bool showToday = false;
    [SerializeField] private TimeSpan day;
    private int moneyReward, currentMoney;
    private bool show;
    private DateTime entryDate, startDate;
    void OnEnable()
    {
        day = new TimeSpan(1, 0, 0, 0);
        show = false;
        currentMoney = setup.GetComponent<Setup>().Money;
        moneyReward = Mathf.Abs(currentMoney * 1 / 5)+10;
        textMoney.GetComponent<Text>().text = moneyReward.ToString();
        Load();
        //StartCoroutine("delayedStart");
        if (!show)
        {
            Destroy(gameObject);
        }

    }
    IEnumerator delayedStart()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        gameObject.SetActive(true);
    }
    // Update is called once per frame
    public void GiveMoney()
    {
        moneyShower.GetComponent<moneyShower>().addMoney(moneyReward);
        Save();
        Destroy(gameObject);
    }
    public void GiveMoney_Double()
    {
        moneyShower.GetComponent<moneyShower>().addMoney(moneyReward*2);
        Save();
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        Save();
    }
    private void OnDisable()
    {
        Save();
    }
    public DateTime Today()
    {
        //return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        //return DateTime.Now;
        return DateTime.UtcNow;
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/" + saveFileName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + saveFileName + ".dat", FileMode.Open);
            DailyReward data = (DailyReward)bf.Deserialize(file);

            startDate=data.startDate;
            //Debug.Log("TODAYMINUS()= " + TodayMinus() + " STARTDATE= " + startDate + " SHOW DAILY REWARD: " + (TodayMinus() > startDate));
            Debug.Log("SHOW DAILY REWARD: " + (Today() - startDate > day)+" TODAY()= " + Today() + " STARTDATE= " + startDate + "TODAY()-STARTDATE=" + (Today()-startDate) + "==="+ day);

            if (Today() - startDate>day)
            {
                //data.startDate = Today();
                //startDate = Today();
                show = true;
            }
            else
            {
                show = false;
            }
            file.Close();
        }
        else
        {
            startDate = Today();
            Debug.LogWarning("No file named: / " + saveFileName + ".dat/n . Using default settings.");

            show = false;
        }
        if (showToday)
        {
            show = true;
        }
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + saveFileName + ".dat");

        DailyReward data = new DailyReward();
        if (Today() - startDate > day)
        {
            data.startDate = Today();
        }
        else
        {
            data.startDate = startDate;
        }
        bf.Serialize(file, data);
        file.Close();
    }
}
[Serializable]
public class DailyReward
{
    [HideInInspector] public DateTime startDate;
}
