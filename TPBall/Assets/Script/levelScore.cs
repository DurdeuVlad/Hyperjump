using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using UnityEngine.UI;

public class levelScore : MonoBehaviour
{
    [SerializeField] private string filename;
    public int level = 1, distance;
    [SerializeField] private Vector2 DistanceRange;
    [SerializeField] private Transform finishLine, playerTr;
    [SerializeField] private Slider gameSlider;
    [SerializeField] private Text levelText;
    float aux;
    //[SerializeField] private GameObject titleText;
    // Start is called before the first frame update

    void OnEnable()
    {
        LoadFromFile();
        
        SetSlider();
        SetFinishLine();
        StartCoroutine("CheckWin");
        aux = 0;
        gameSlider.maxValue = 0;

    }

    void OnDisable()
    {
        SaveToFile();
    }

    private void FixedUpdate()
    {
        gameSlider.value = playerTr.position.y;
    }

    void ReachEnd()
    {
        //animation winn
        //se
        ResetData();
        level++;
        levelText.text = level.ToString();
        SaveToFile();
        SetFinishLine();
        SetSlider();
        StartCoroutine("CheckWin");
    }

    IEnumerator CheckWin()
    {

        aux += distance;
        yield return new WaitUntil(() => aux <= playerTr.position.y);
        ReachEnd();
    }

    void SetFinishLine()
    {
        finishLine.gameObject.SetActive(true);
        finishLine.position = new Vector3(finishLine.position.x, playerTr.position.y+distance+2.2f, finishLine.position.z);
    }

    void SetSlider()
    {
        gameSlider.maxValue += distance;
        gameSlider.minValue = playerTr.position.y;
        levelText.text = level.ToString();
    }

    void ResetData()
    {
        distance = Mathf.FloorToInt(UnityEngine.Random.Range(DistanceRange.x, DistanceRange.y));
    }

    void SaveToFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + filename);

        LevelData data = new LevelData();
        data.level = level;
        data.distance = distance;

        bf.Serialize(file, data);
        file.Close();
    }

    // Update is called once per frame
    void LoadFromFile()
    {
        if (File.Exists(Application.persistentDataPath + "/"+ filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + filename, FileMode.Open);
            LevelData LevelData = (LevelData)bf.Deserialize(file);
            file.Close();

            level = LevelData.level-1;
            distance = LevelData.distance;
            Debug.Log("Level data loaded succesfully.");
        }
        else
        {
            level = 1;
            distance = Mathf.FloorToInt(UnityEngine.Random.Range(DistanceRange.x, DistanceRange.y));
            Debug.LogWarning("No Level data has been found. Using default settings.");
        }
    }
}
[Serializable]
class LevelData
{
    [HideInInspector] public int level;
    [HideInInspector] public int distance;
}