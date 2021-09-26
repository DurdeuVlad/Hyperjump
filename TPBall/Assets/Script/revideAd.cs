using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class reviveAd : MonoBehaviour
{
    [SerializeField] public Image timer, player;
    [SerializeField] public float time;
    [SerializeField] public string androidId, appleId;
    private string gameId;
    private string placementId = "rewardedVideo";
    private Button adButton;


    void Start()
    {
#if UNITY_EDITOR
            Advertisement.Initialize(androidId, true);
            gameId = androidId;
#elif UNITY_IOS
            Advertisement.Initialize(appleId, false);
            gameId = appleId;
#else
            Advertisement.Initialize(androidId, true);
            gameId = androidId;
#endif
        adButton = GetComponent<Button>();
        if (adButton)
        {
            adButton.onClick.AddListener(ShowAd);
        }

        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, true);
        }
    }

    void Update()
    {
        if (adButton)
        {
            adButton.interactable = Monetization.IsReady(placementId);
        }
    }

    void ShowAd()
    {
        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        ad.Show(options);
    }

    void HandleShowResult(UnityEngine.Monetization.ShowResult result)
    {
        if (result == UnityEngine.Monetization.ShowResult.Finished)
        {
            player.color = Color.white;
            player.gameObject.GetComponent<Transform>().transform.position = new Vector2(player.gameObject.GetComponent<Transform>().transform.position.x, player.gameObject.GetComponent<Transform>().transform.position.y - 1);

        }
        else if (result == UnityEngine.Monetization.ShowResult.Skipped)
        {
            Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (result == UnityEngine.Monetization.ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
