using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class Setup : MonoBehaviour
{
    [SerializeField] public bool public_flag_showMenu, public_flag_HighscoreMode, public_flag_useCameraAlert;
    [SerializeField] public GameObject block, player, money, score, dailyGoalsHub, tutorialHandler, scoreLevel;
    [HideInInspector] public int Money, SkinID;
    [SerializeField] public int moneyMaxValue;
    [HideInInspector] public float highScore, currentScore, currentBoughtSkins;
    [SerializeField] public Transform left, right;
    [SerializeField] public float posDistance, maxRotation, deleteBlockHeight, maxRotationSpeed, minSpeed, maxSpeed, minScale;
    private float distance, posAux, moneySpawnPr, moneySpawnMax, currentMoneyCount;
    private int objectAux = 0;
    private Transform tr, playerTr;
    private GameObject newBlock;
    [SerializeField] public GameObject[] playerSkins;
    [SerializeField] bool useDebugLog;
    [SerializeField] public bool[] boughtPlayerSkins;
    [SerializeField] public AudioClip[] explosions;
    [SerializeField] public AudioClip[] charge;
    [SerializeField] public AudioClip[] release;
    [SerializeField] private Text changeGamemode;
    [SerializeField] private GameObject gamemodeTitleText;

    private void Bruh(string text)
    {
        if (useDebugLog)
        {
            Debug.Log(text);
        }
    }

    [SerializeField] public GameObject[] gameobjectsToSpawn;
    public void changeSkin(int skinID)
    {
        player.GetComponent<SpriteRenderer>().sprite = playerSkins[skinID].GetComponent<SpriteRenderer>().sprite;
        player.GetComponent<SpriteRenderer>().color = playerSkins[skinID].GetComponent<SpriteRenderer>().color;
        player.GetComponent<PolygonCollider2D>().points = playerSkins[skinID].GetComponent<PolygonCollider2D>().points;
        player.GetComponent<BoxCollider2D>().size = playerSkins[skinID].GetComponent<BoxCollider2D>().size;
        player.GetComponent<BoxCollider2D>().offset = playerSkins[skinID].GetComponent<BoxCollider2D>().offset;
        SkinID = skinID;
        Save();
        //PlayerPrefs.SetInt("SkinID", skinID);
    }
    // Start is called before the first frame update

    IEnumerator checkPlayerPosition()
    {
        int nr = 0;
        while (true)
        {
            yield return new WaitUntil(() => tr.position.y - playerTr.position.y < distance);
            SpawnBlock();
            float posAuxx;
            do
            {
                posAuxx = UnityEngine.Random.Range(left.position.x, right.position.x);
            } while (posAuxx == posAux||(posAuxx- posDistance <= posAux&&posAux<=posAuxx+ posDistance));
            tr.position = new Vector2(posAux=posAuxx, tr.position.y + distance / 2);
            int aux = Mathf.RoundToInt(UnityEngine.Random.Range(0, moneySpawnMax));
            bool iiet = moneySpawnPr >= aux;
            //Debug.Log("aux= " + aux + " moneySpawnPr= " + moneySpawnPr + " " + iiet);
            if (moneySpawnPr >= aux)
            {
                moneySpawnPr = 5;
                SpawnMoney(nr.ToString());

            }
            else
            {
                moneySpawnPr++;
                if (moneySpawnPr == moneySpawnMax)
                {
                    moneySpawnPr = moneySpawnMax;
                }
            }
            nr++;
        }
    }

    void Start()
    {
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        Load();
        changeSkin(SkinID);
        player = GameObject.FindGameObjectWithTag("Player");
        tr = GetComponent<Transform>();
        playerTr = player.GetComponent<Transform>();
        //maxSizeX = blockTr.localScale.x;
        distance = tr.position.y - playerTr.position.y;
        StartCoroutine("checkPlayerPosition");
        //StartCoroutine("spawnMoney");
        posAux = tr.position.x;
        moneySpawnPr = 5;
        moneySpawnMax = 100;
        SpawnMoney("test");
        if (public_flag_HighscoreMode)
        {
            changeGamemode.text = "highscore mode";
            gamemodeTitleText.SetActive(false);
            //gamemodeTitleText.SetActive(false);

        }
        else
        {
            changeGamemode.text = "level mode";
            gamemodeTitleText.SetActive(true);
            //gamemodeTitleText.SetActive(true);
        }


    }

    // Update is called once per frame
    /*void Update()
    {
      if (tr.position.y - playerTr.position.y < distance)
        {
            SpawnBlock();
            tr.position = new Vector2(Random.Range(left.position.x, right.position.x), tr.position.y+distance/2);
        }
        
    }*/

    private void SpawnBlock()
    {
        /*newBlock = Instantiate(block, tr.position, Quaternion.Euler(0, 0, Random.Range(-maxRotation, maxRotation)));
        blockTr = newBlock.GetComponent<Transform>();
        blockTr.localScale = new Vector2(Random.Range(minSizeX, maxSizeX), Random.Range(minSizeY, maxSizeY));
        */
        int objectAuxx;
        do
        {
            objectAuxx = UnityEngine.Random.Range(0, (gameobjectsToSpawn.Length));
        } while (objectAuxx == objectAux);
        newBlock = Instantiate(gameobjectsToSpawn[objectAux = objectAuxx], tr.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(-maxRotation, maxRotation)));
        newBlock.GetComponent<rotate>().scale= UnityEngine.Random.Range(minScale,1);
        newBlock.GetComponent<rotate>().rotationSpeed = UnityEngine.Random.Range(-maxRotation, maxRotation);
        newBlock.GetComponent<rotate>().speedDownwards = UnityEngine.Random.Range(minSpeed, maxSpeed);
        newBlock.GetComponent<rotate>().tutorialAsteriod = false;
    }

    private void SpawnMoney(string nameNr)
    {
        GameObject yeet=Instantiate(money, tr.position, Quaternion.Euler(0, 0, 0));
        yeet.GetComponent<MoneyScript>().rotationSpeed = UnityEngine.Random.Range(-maxRotationSpeed, maxRotationSpeed);
        yeet.transform.position = new Vector3(playerTr.position.x, tr.position.y, yeet.transform.position.z);
        yeet.name = yeet.name + "" + nameNr;
    }

    IEnumerator spawnMoney()
    {
        int nr=0;
        while (true)
        {
            yield return new WaitUntil(() => tr.position.y - playerTr.position.y < distance);
            int aux = Mathf.RoundToInt(UnityEngine.Random.Range(0, moneySpawnMax));
            bool iiet = moneySpawnPr >= aux;
            //Debug.Log("aux= "+ aux+ " moneySpawnPr= "+ moneySpawnPr+ " "+ iiet);
            if (moneySpawnPr >= aux)
            {
                moneySpawnPr = 5;
                SpawnMoney(nr.ToString());

            }
            else
            {
                moneySpawnPr++;
                if (moneySpawnPr==moneySpawnMax)
                {
                    moneySpawnPr = moneySpawnMax;
                }
            }
            nr++;
        }

    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
        }

        FileStream file = File.Create(Application.persistentDataPath+"/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.money = Money;
        data.skins = boughtPlayerSkins;
        data.highscore = highScore;
        data.skinID = SkinID;
        data.public_flag_HighscoreMode = public_flag_HighscoreMode;
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath+ "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            Money = Mathf.Abs(data.money);
            boughtPlayerSkins = data.skins;
            highScore = data.highscore;
            SkinID = data.skinID;
            public_flag_HighscoreMode = data.public_flag_HighscoreMode;
            Debug.Log("Gameplay data loaded succesfully.");
        }
        else
        {
            Money = 0;
            highScore = 0;
            SkinID = 0;
            public_flag_HighscoreMode = true;
            Debug.LogWarning("No gameplay data has been found. Using default settings.");
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }


    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Save();
        }
    }

    public void SetupScore()
    {
        if (public_flag_HighscoreMode)
        {
            score.SetActive(true);
            Debug.Log("score set activ");
            gamemodeTitleText.SetActive(false);
        }
        else
        {
            scoreLevel.SetActive(true);
            Debug.Log("scoreLevel set activ");
            gamemodeTitleText.SetActive(true);
        }
    }


    public void ChangeGamemode(Text text)
    {
        if (public_flag_HighscoreMode)
        {
            text.text = "highscore mode";
            gamemodeTitleText.SetActive(true);
            //gamemodeTitleText.SetActive(false);
            gamemodeTitleText.GetComponent<Text>().text = "Level mode";


        }
        else
        {
            text.text = "level mode";
            gamemodeTitleText.SetActive(true);
            gamemodeTitleText.GetComponent<Text>().text = "Clasic mode";

            //gamemodeTitleText.SetActive(true);
        }

        public_flag_HighscoreMode = !public_flag_HighscoreMode;
        Save();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
[Serializable]
class PlayerData
{
    public int money;
    public bool[] skins;
    public float highscore;
    public int skinID;
    public bool public_flag_HighscoreMode;
}
