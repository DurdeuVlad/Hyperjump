using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class adButton : MonoBehaviour
{
    [SerializeField] private GameObject Setup;
    [SerializeField] private Button adButtons;
    void Update()
    {
        if (adButtons)
        {
            adButtons.interactable = Setup.GetComponent<AdHandler>().rewardedAdReadytoShow;
        }
    }

}
