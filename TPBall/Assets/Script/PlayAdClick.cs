using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class PlayAdClick : MonoBehaviour
{
    [SerializeField] private string appID = "ca-app-pub-8943330341510219~7213579768", adID;
    private RewardedAd rewardedAd;
    [SerializeField] private GameObject DailyReward;
    // Start is called before the first frame update
    private void Start()
    {
#if UNITY_EDITOR
        adID = "ca-app-pub-3940256099942544/5224354917";
#else
        adID = "ca-app-pub-8943330341510219/9563894010";
#endif
        this.rewardedAd = new RewardedAd(adID);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }
    void OnEnable()
    {
        gameObject.SetActive(this.rewardedAd.IsLoaded());
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
             "HandleRewardedAdRewarded event received for "
                 + amount.ToString() + " " + type);
    }
    
}
