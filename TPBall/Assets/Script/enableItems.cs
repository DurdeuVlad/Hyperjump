using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableItems : MonoBehaviour
{
    [SerializeField] GameObject[] toEnable;
    private void OnEnable()
    {
        foreach(GameObject game in toEnable)
        {
            game.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject game in toEnable)
        {
            game.SetActive(false);
        }
    }
}
