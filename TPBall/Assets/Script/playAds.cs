using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class playAds : MonoBehaviour
{
    [SerializeField] public bool useDebugLog, testMode;
    [SerializeField] public float showAdsPercentageBase, showAdsMax;
    private float showAdsPercentage;
    [SerializeField] public string androidId, appleId;
    private void Bruh(string text)
    {
        if (useDebugLog)
        {
            Debug.Log(text);
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        #if UNITY_EDITOR
            Advertisement.Initialize(androidId, true);
        #elif UNITY_IOS
            Advertisement.Initialize(appleId, false);
        #else
            Advertisement.Initialize(androidId, true);
        #endif
    }
    private void OnDestroy()
    {
        if (showAdsMax > showAdsPercentageBase + Time.timeSinceLevelLoad / 10)
        {
            showAdsPercentage = showAdsPercentageBase + Time.timeSinceLevelLoad/10;
            Bruh("showAdsPercentage: "+ showAdsPercentage);
        }
        else
        {
            showAdsPercentage = showAdsMax;
            Bruh("showAdsPercentage: " + showAdsPercentage);
        }
        float aux = Random.Range(1, 100);
        Bruh("aux ads=" + aux);
        //Time.timeScale = 0;
        if (aux < showAdsPercentage+showAdsPercentageBase)
        {
            Advertisement.Show("video");
        }
        Time.timeScale = 1;
    }


}
