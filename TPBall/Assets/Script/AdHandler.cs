using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using GoogleMobileAds.Android;
using GoogleMobileAds.Api;
using System;

public class AdHandler : MonoBehaviour
{
    [Header("Ad Settings")]
    [HideInInspector] public bool rewardedAd_Played = false, rewardedAd_RewardPlayer = false, rewardedAdReadytoShow = false;
    [SerializeField] private string appID;
    [SerializeField] private string Admob_rewardedID, Admob_videoID;
    [SerializeField] private string testDevice;
    [SerializeField] private GameObject dailyReward, player;
    private int delayAd;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private bool RestartLevel=false;
    // Start is called before the first frame update
    void Start()
    {
        delayAd = UnityEngine.Random.Range(2, 5);
        LoadAd_Inter();
        LoadAd_RewardedAd();
        StartCoroutine("WaitAndLoad_Inter");
        StartCoroutine("WaitAndLoad_Rewarded");
        interstitial.OnAdClosed += HandleOnAdClosed;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdClosed;
    }
    private IEnumerable WaitAndLoad_Inter()
    {
        if (!interstitial.IsLoaded())
        {
            LoadAd_Inter();
        }
        yield return new WaitUntil(() => !interstitial.IsLoaded());
        yield return new WaitForSeconds(2);
        StartCoroutine("WaitAndLoad_Inter");
    }

    private IEnumerable WaitAndLoad_Rewarded()
    {  
        if (!rewardedAd.IsLoaded())
        {
            LoadAd_RewardedAd();
        }
        rewardedAdReadytoShow = rewardedAd.IsLoaded();
        yield return new WaitUntil(() => !rewardedAd.IsLoaded());
        yield return new WaitForSeconds(2);
        StartCoroutine("WaitAndLoad_Rewarded");
    }

    // Update is called once per frame
    public void LoadAd_Inter()
    {
#if UNITY_EDITOR
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_ANDROID
        string adUnitId = Admob_videoID;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice(testDevice)
            .Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    // Update is called once per frame
    public void LoadAd_Video()
    {
#if UNITY_EDITOR
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_ANDROID
        string adUnitId = Admob_rewardedID;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.rewardedAd = new RewardedAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice(testDevice)
            .Build();
        // Load the interstitial with the request.
        this.rewardedAd.LoadAd(request);
        
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        Time.timeScale = 1;
        if (RestartLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        RestartLevel = false;

    }
    public void ShowAd_Inter(bool restartLevel)
    {

        RestartLevel = restartLevel;
        if (delayAd <= 0)
        {
            delayAd = UnityEngine.Random.Range(2, 5);
            if (this.interstitial.IsLoaded())
            {
                this.interstitial.Show();
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                if (RestartLevel)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                RestartLevel = false;
            }
        }
        else
        {
            delayAd--;
            Time.timeScale = 1;
            if (RestartLevel)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            RestartLevel = false;
        }
    }
    
    public void LoadAd_RewardedAd()
    {
#if UNITY_EDITOR
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_ANDROID
        string adUnitId = Admob_rewardedID;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.rewardedAd = new RewardedAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice(testDevice)
            .Build();
        // Load the interstitial with the request.
        this.rewardedAd.LoadAd(request);

        if (rewardedAd.IsLoaded())
        {
            Debug.Log("Rewarded Ad Loaded");
        }
        else
        {
            Debug.LogWarning("Rewarded Ad Couldn't Load");
        }
        rewardedAdReadytoShow = rewardedAd.IsLoaded();
    }

    public void ShowAd_RewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            rewardedAd_Played = true;
            rewardedAd_RewardPlayer = false;
            Debug.LogError("Reward Ad couldn't be played");
        }
    }

    public bool Played_Ad()
    {
        return rewardedAd_Played;
    }

    public bool Player_Reward()
    {
        return rewardedAd_Played && rewardedAd_RewardPlayer;
    }

    public void Player_Rewarded()
    {
        rewardedAd_Played = false;
        rewardedAd_RewardPlayer = false;
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        rewardedAd_Played = true;
        rewardedAd_RewardPlayer = false;
    }


    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        rewardedAd_Played = true;
        rewardedAd_RewardPlayer = true;
    }
    public void PlayAdtoDoubleMoney()
    {
        StartCoroutine("DoubleTheMoney");
    }
    public IEnumerator DoubleTheMoney()
    {
        ShowAd_RewardedAd();
        yield return new WaitUntil(() => Played_Ad());
        if (Player_Reward())
        {
            dailyReward.GetComponent<dailyReward>().GiveMoney_Double();
            Debug.Log("Player rewarded");
        }
        else
        {
            dailyReward.GetComponent<dailyReward>().GiveMoney();
            Debug.LogWarning("Player not rewarded");
        }
        Player_Rewarded();
    }

    public void PlayReviveAd()
    {
        StartCoroutine("ReviveAd");
        Debug.Log("Rewarded Ad for revival is showing");
    }
    public IEnumerator ReviveAd()
    {
        if (rewardedAdReadytoShow)
        {
            ShowAd_RewardedAd();
            yield return new WaitUntil(() => Played_Ad());
            if (Player_Reward())
            {
                player.GetComponent<PlayerController>().RevivePlayer_Revive();
                Debug.Log("Player rewarded");
            }
            else
            {
                player.GetComponent<PlayerController>().RevivePlayer_AdRefused();
                Debug.LogWarning("Player not rewarded");
            }
            Player_Rewarded();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
