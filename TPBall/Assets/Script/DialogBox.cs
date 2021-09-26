using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private GameObject setup;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (setup.GetComponent<AdHandler>().rewardedAdReadytoShow)
        {
            Debug.Log("Rewarded ad ready to show");
        }
        else
        {
            Debug.LogWarning("Rewarded ad not ready to show");
        }
        gameObject.SetActive(setup.GetComponent<AdHandler>().rewardedAdReadytoShow);
    }

}
