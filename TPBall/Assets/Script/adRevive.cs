using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class adRevive : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Image timer;
    [SerializeField] public float time;
    public Button adButton;
    private float timestamp;
    [HideInInspector] public bool continueCorutine = false;
    [SerializeField] private GameObject setup, playerController;
    void OnEnable()
    {
        if (!setup.GetComponent<AdHandler>().rewardedAdReadytoShow || setup.GetComponent<Setup>().public_flag_HighscoreMode)
        {
            setup.GetComponent<AdHandler>().ShowAd_Inter(true);
        }
        //gameId = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().gameId;
        timestamp = Time.time;
        timer.fillAmount = 1;
        adButton = GetComponent<Button>();
        continueCorutine = setup.GetComponent<Setup>().public_flag_HighscoreMode;
        continueCorutine = !setup.GetComponent<AdHandler>().rewardedAdReadytoShow;
        gameObject.SetActive(setup.GetComponent<AdHandler>().rewardedAdReadytoShow);
        gameObject.SetActive(!setup.GetComponent<Setup>().public_flag_HighscoreMode);
        
    }
    private void OnDisable()
    {
        setup.GetComponent<AdHandler>().ShowAd_Inter(true);
    }

    public bool WaitUntilVariable()
    {
        return continueCorutine;
    }

    private void FixedUpdate()
    {
        timer.fillAmount -= time/10 * Time.deltaTime;
        if (timer.fillAmount <= 0)
        {
            continueCorutine=true;
        }
        if (adButton)
        {
            adButton.interactable = setup.GetComponent<AdHandler>().rewardedAdReadytoShow;
        }
    }

    public void PlayAd()
    {
        StartCoroutine("adPlay");
    }

    IEnumerator adPlay()
    {
        setup.GetComponent<AdHandler>().ShowAd_RewardedAd();
        yield return new WaitUntil(() => setup.GetComponent<AdHandler>().rewardedAd_Played);
        if (setup.GetComponent<AdHandler>().Player_Reward())
        {
            playerController.GetComponent<PlayerController>().RevivePlayer_Revive();

        }
        else
        {
            setup.GetComponent<AdHandler>().ShowAd_Inter(true);
        }
    }


}
