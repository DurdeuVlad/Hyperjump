using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyShower : MonoBehaviour
{
    private GameObject setup;
    private bool GameEnded, GameStarted, blockNormalCounting;
    private int x, moneyMaxValue;
    // Start is called before the first frame update
    public void Disparion()
    {
        StartCoroutine("disparinus");
    }
    public void AddOne()
    {
        if (!GameEnded & GameStarted& setup.GetComponent<Setup>().Money<= moneyMaxValue)
        {
            Debug.Log("Money: " + setup.GetComponent<Setup>().Money);
            setup.GetComponent<Setup>().Money = setup.GetComponent<Setup>().Money + 1;
            Debug.Log("Money: " + setup.GetComponent<Setup>().Money);
            MoneyShower.text = setup.GetComponent<Setup>().Money.ToString();
            setup.GetComponent<Setup>().Save();
        }
        //banu++;
        //PlayerPrefs.SetInt("Bancnote", banu);
        //MoneyShower.text = banu.ToString();
    }
    private Text MoneyShower;
    private Image MoneyShowerIcon;
    private int banu=0;
    private void OnEnable()
    {
        GameEnded = false;
        GameStarted = false;
        setup = GameObject.FindGameObjectWithTag("Setup");
        MoneyShower = GetComponent<Text>();
        MoneyShower.text = setup.GetComponent<Setup>().Money.ToString();
        //banu = PlayerPrefs.GetInt("Bancnote", 0);
        //MoneyShower.text = banu.ToString();
        MoneyShower.CrossFadeAlpha(1, 1f, true);
        MoneyShowerIcon = GameObject.Find("MoneyShowerIcon").GetComponent<Image>();
        MoneyShowerIcon.CrossFadeAlpha(1, 1f, true);
        MoneyShower.text = ""+setup.GetComponent<Setup>().Money;
        StartCoroutine("checkMoney");
        blockNormalCounting = false;
        moneyMaxValue = setup.GetComponent<Setup>().moneyMaxValue;
    }
    IEnumerator checkMoney()
    {
        while (true)
        {
            yield return new WaitUntil(() => int.Parse(MoneyShower.text) != setup.GetComponent<Setup>().Money);
            if (!blockNormalCounting)
            {
                /*if (int.Parse(MoneyShower.text) < setup.GetComponent<Setup>().Money)
                {
                    MoneyShower.text = "" + (int.Parse(MoneyShower.text) + 1);
                }
                else*/
                if (int.Parse(MoneyShower.text) > setup.GetComponent<Setup>().Money)
                {
                    MoneyShower.text = "" + (int.Parse(MoneyShower.text) - 1);
                }
                else if (int.Parse(MoneyShower.text) != setup.GetComponent<Setup>().Money)
                {
                    MoneyShower.text = "" + setup.GetComponent<Setup>().Money;
                }
            }
        }
    }
    public void addMoney(int addX)
    {
        StartCoroutine("addMoneyE");
        x = addX;
    }
    IEnumerator addMoneyE()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        if (setup.GetComponent<Setup>().Money <= moneyMaxValue)
        {
            blockNormalCounting = true;
            setup.GetComponent<Setup>().Money = setup.GetComponent<Setup>().Money + x;
            int money = setup.GetComponent<Setup>().Money + x;
            do
            {
                MoneyShower.text = "" + (int.Parse(MoneyShower.text) + x / 2);
                yield return new WaitForSecondsRealtime(0.01f);
                x = x - x / 2;
                //Debug.Log("Add 1");
            } while (int.Parse(MoneyShower.text) < money);
            blockNormalCounting = false;
            yield return null;
        }
    }
    IEnumerator disparinus()
    {
        yield return new WaitForSeconds(1.6f);
        MoneyShower.CrossFadeAlpha(0, 1f, true);
        MoneyShowerIcon.CrossFadeAlpha(0, 1f, true);
    }
    public void aparinos()
    {
        MoneyShower.CrossFadeAlpha(1, 0.5f, true);
        MoneyShowerIcon.CrossFadeAlpha(1, 0.5f, true);
    }
    public void StartMeci()
    {
        MoneyShower.CrossFadeAlpha(0, 0f, true);
        MoneyShowerIcon.CrossFadeAlpha(0, 0f, true);
        MoneyShower.GetComponent<Text>().color = Color.white;
        MoneyShowerIcon.GetComponent<Image>().color = Color.white;
        GameStarted = true;
        MoneyShower.text = setup.GetComponent<Setup>().Money.ToString();
    }
    public void GameEnd()
    {
        GameEnded = true;
    }

    public void GameResumed()
    {
        GameEnded = false;
    }

    private void FixedUpdate()
    {
        if (setup.GetComponent<Setup>().Money<=int.Parse(MoneyShower.text))
        {
            MoneyShower.text=""+ (int.Parse(MoneyShower.text)- (int.Parse(MoneyShower.text)-setup.GetComponent<Setup>().Money)/2);
            if (setup.GetComponent<Setup>().Money - int.Parse(MoneyShower.text) == -1)
            {
                MoneyShower.text = "" + setup.GetComponent<Setup>().Money;
            }
        }
    }
}
