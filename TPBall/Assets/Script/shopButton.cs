using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopButton : MonoBehaviour
{
    [SerializeField] private int costToBuy;
    //[SerializeField] private GameObject notEnoughMoney;
    private Button bt;
    private GameObject setup;
    private Text text;
    private bool StopUpdate;
    // Start is called before the first frame update
    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        setup = GameObject.FindGameObjectWithTag("Setup");
        StopUpdate = true;
    }
    void Start()
    {

        bt = GetComponent<Button>();
        if (setup.GetComponent<Setup>().SkinID.ToString() == gameObject.name)
        {
            bt.interactable = false;
            text.text = "Equipped";
            text.color = Color.green;
        }
        else
        {
            bt.interactable = true;
            if (setup.GetComponent<Setup>().boughtPlayerSkins[int.Parse(gameObject.name)] || costToBuy == 0)
            {
                text.text = "";
                text.color = Color.white;
            }
            else
            {
                text.text = costToBuy + "";
                text.color = Color.blue;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (StopUpdate)
        {
            if (setup.GetComponent<Setup>().SkinID.ToString() == gameObject.name)
            {
                bt.interactable = false;
                text.text = "Equipped";
                text.color = Color.green;
            }
            else
            {
                bt.interactable = true;
                if (setup.GetComponent<Setup>().boughtPlayerSkins[int.Parse(gameObject.name)] || costToBuy == 0)
                {
                    text.text = "";
                    text.color = Color.white;
                }
                else
                {
                    text.text = costToBuy + "";
                    text.color = Color.blue;
                }
            }
        }
    }
    private void OnEnable()
    {
        GetComponent<Image>().sprite = setup.GetComponent<Setup>().playerSkins[int.Parse(gameObject.name)].GetComponent<SpriteRenderer>().sprite;
    }
    public void buttonClick()
    {
        if (setup.GetComponent<Setup>().boughtPlayerSkins[int.Parse(gameObject.name)] || costToBuy == 0)
        {
            
            equip();
        }
        else
        {
           
            buy();
        }
    }
    private void buy()
    {
        if (setup.GetComponent<Setup>().Money>=costToBuy)
        {
            setup.GetComponent<Setup>().boughtPlayerSkins[int.Parse(gameObject.name)]=true;
            setup.GetComponent<Setup>().Money= setup.GetComponent<Setup>().Money - costToBuy;
            Debug.Log("setup.GetComponent<Setup>().Money="+setup.GetComponent<Setup>().Money+ " costToBuy="+ costToBuy);
            setup.GetComponent<Setup>().changeSkin(int.Parse(gameObject.name));
        }
        else
        {
            StartCoroutine("dontHaveEnough");
        }
    }
    private void equip()
    {
        setup.GetComponent<Setup>().changeSkin(int.Parse(gameObject.name));
    }
    IEnumerator dontHaveEnough()
    {
        Debug.LogWarning("NOT ENOOUGH MONEY");
        text.text = "You don't have enough cash";
        text.color = Color.red;
        StopUpdate = false;
        yield return new WaitForSeconds(1);
        text.text = costToBuy + "";
        text.color = Color.white;
        StopUpdate = true;
        yield return null;
    }
}
