using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;
using UnityEngine.UI;
//using GoogleMobileAds.Android;
using GoogleMobileAds.Api;

public class PlayerController : MonoBehaviour
{
    [Header("Ad Settings")]
    [SerializeField] private bool useAdmob;
    [SerializeField] public string androidId, appleId;
    [SerializeField] private string appID;
    [SerializeField] private string Admob_aplicationID, Admob_videoID;
    private InterstitialAd interstitial;
    [Header("Player Settings")]
    [SerializeField] public bool useOldChargingSystem;
    private Transform tr;
    private Rigidbody2D rg;
    private ParticleSystem aux;
    private float timestamp, timestamp1, minTeleportDistance;
    [SerializeField] public Transform chargeBarTr;
    [SerializeField] public GameObject chargingText, audioCharge, audioRelease, reviveAd, particleDie, tutorialHandler, fullyChargedText;
    [SerializeField] public float maxTeleportDistance, cooldown=1f, speedUp;
    [SerializeField] public ParticleSystem onTp, died, charge1, charge2;
    private float startSizeCharge1, startSizeCharge2, startSizeCharge2y;
    private Transform trCharge1, trCharge2;
    [SerializeField] public bool UseOriginalControls, useDebugLog;
    [HideInInspector] public bool upButtonUP = false, upButtonDOWN = false, notDead = true, hasMoved = false, public_flag_dead = false;
    [HideInInspector] private float timeHold, auxxx=1, timestampCooldown;
    private Vector3 lastPlayerPosition; 
    private bool useCameraAlert;
    private GameObject auy, auyy, setup, mainCamera;
    private Button down;
    public string gameId;
    private string placementId = "video";
    private int muteState, showAd;
    private GameObject[] playerSkins;
    [SerializeField] private Color colorTarget;
    private Color colorHolder;
    Image chargeBarImage;
    private void Bruh(string text)
    {
        if (useDebugLog)
        {
            Debug.Log(text);
        }
    }
    public void changeSkin(int skinID)
    {
        GetComponent<SpriteRenderer>().sprite = playerSkins[skinID].GetComponent<SpriteRenderer>().sprite;
        GetComponent<PolygonCollider2D>().points = playerSkins[skinID].GetComponent<PolygonCollider2D>().points;
        GetComponent<BoxCollider2D>().size = playerSkins[skinID].GetComponent<BoxCollider2D>().size;
        GetComponent<BoxCollider2D>().offset = playerSkins[skinID].GetComponent<BoxCollider2D>().offset;
        setup.GetComponent<Setup>().SkinID=skinID;
        setup.GetComponent<Setup>().Save();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        showAd = Random.Range(2, 6);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        down = GameObject.FindGameObjectWithTag("DownButton").GetComponent<Button>();
        timestampCooldown = Time.time;
        cooldown = 0.15f;
        setup = GameObject.FindGameObjectWithTag("Setup");
        playerSkins = setup.GetComponent<Setup>().playerSkins;
        trCharge1 = charge1.GetComponent<Transform>();
        trCharge2 = charge2.GetComponent<Transform>();
        startSizeCharge1 = trCharge1.localScale.x;
        startSizeCharge2 = trCharge2.localScale.x;
        startSizeCharge2y = trCharge2.localScale.y;
        trCharge1.localScale = new Vector2(0, 0);
        trCharge2.localScale = new Vector2(0, 0);
        tr = GetComponent<Transform>();
        rg = GetComponent<Rigidbody2D>();
        minTeleportDistance = maxTeleportDistance/5;
        chargeBarTr.localScale = new Vector3(0, chargeBarTr.localScale.y, chargeBarTr.localScale.z);
        auxxx = 1;
        useCameraAlert = setup.GetComponent<Setup>().public_flag_useCameraAlert;
        if (!useAdmob)
        {
#if UNITY_IOS
            Advertisement.Initialize(appleId, false);
            gameId = appleId;
#else
            Advertisement.Initialize(androidId, false);
            Monetization.Initialize(androidId, false);

            gameId = androidId;
#endif
        }
        else
        {
            MobileAds.Initialize(appID);
        }
        muteState = PlayerPrefs.GetInt("muteState", 1);
        chargeBarImage = chargeBarTr.gameObject.GetComponent<Image>();
        colorHolder = chargeBarImage.color;
        //gradient settings
        /*gradient = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.yellow;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.cyan;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);*/

    }

    // Update is called once per frame
    void Update()
    {
        float timeHold1=0, timeHold2=0;
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y+speedUp*Time.deltaTime);
        if (UseOriginalControls)
        {
            if (Input.GetAxis("Horizontal") > 0 && timestamp <= Time.time)
            {

                timestamp = Time.time + cooldown;
                aux = Instantiate(onTp, tr.position, Quaternion.identity);
                tr.position = new Vector3(tr.position.x + minTeleportDistance / 2, tr.position.y, tr.position.z);
            }
            if (Input.GetAxis("Horizontal") < 0 && timestamp <= Time.time)
            {
                timestamp = Time.time + cooldown;
                aux = Instantiate(onTp, tr.position, Quaternion.identity);
                tr.position = new Vector3(tr.position.x - minTeleportDistance / 2, tr.position.y, tr.position.z);
            }
            if (Input.GetAxis("Vertical") > 0 && timestamp <= Time.time)
            {
                timestamp = Time.time + cooldown;
                aux = Instantiate(onTp, tr.position, Quaternion.identity);
                tr.position = new Vector3(tr.position.x, tr.position.y + minTeleportDistance, tr.position.z);
            }
            if (Input.GetAxis("Vertical") < 0 && timestamp <= Time.time)
            {
                timestamp = Time.time + cooldown;
                aux = Instantiate(onTp, tr.position, Quaternion.identity);
                tr.position = new Vector3(tr.position.x, tr.position.y - minTeleportDistance, tr.position.z);
            }
        }
        else
        {
            if (timestampCooldown <= Time.time)
            {
                if (upButtonUP && upButtonDOWN && notDead)
                {
                    fullyChargedText.SetActive(false);
                    if (muteState == 1)
                    {
                        Destroy(auy);
                        auyy = Instantiate(audioRelease);
                    }
                    timestamp = Time.time + cooldown;
                    aux = Instantiate(onTp, tr.position, Quaternion.identity);
                    tr.position = new Vector3(tr.position.x, tr.position.y + minTeleportDistance + timeHold, tr.position.z);
                    upButtonDOWN = false;
                    upButtonUP = false;
                    timeHold = 0;
                    timeHold1 = 0;
                    timeHold2 = 0;
                    chargeBarTr.localScale = new Vector3(0, chargeBarTr.localScale.y, chargeBarTr.localScale.z);
                    hasMoved = false;
                    auxxx = 1;
                    trCharge1.localScale = new Vector2(0, 0);
                    trCharge2.localScale = new Vector2(0, 0);
                    timestampCooldown = Time.time + cooldown;
                    chargeBarImage.color = colorHolder;
                    chargingText.SetActive(false);
                }
                if (upButtonDOWN && timestamp <= Time.time && notDead)
                {
                    lastPlayerPosition = tr.position;
                    if (muteState == 1)
                    {
                        Destroy(auyy);
                        if (timeHold == 0)
                        {
                            auy = Instantiate(audioCharge);

                        }
                    }
                    timeHold += auxxx * Time.deltaTime;
                    timeHold = idkHowToNameStuff(timeHold, maxTeleportDistance);
                    //fully charged text
                    if (timeHold == maxTeleportDistance)
                    {
                        fullyChargedText.SetActive(true);
                        //if (chargeBarImage.color == colorHolder)
                        //{
                            chargeBarImage.color = Color.Lerp(colorHolder, colorTarget, Mathf.PingPong(Time.time, 1));
                        //}
                        chargingText.SetActive(false);

                    }
                    else
                    {
                        chargingText.SetActive(true);
                    }
                    timeHold1 = auxxx * Time.deltaTime;
                    timeHold2 = auxxx * Time.deltaTime;
                    //particles
                    trCharge1.localScale = new Vector2(idkHowToNameStuff(trCharge2.localScale.x, startSizeCharge1) + startSizeCharge1 * timeHold1, idkHowToNameStuff(trCharge2.localScale.y, startSizeCharge1) + startSizeCharge1 * timeHold1);
                    trCharge2.localScale = new Vector2(idkHowToNameStuff(trCharge2.localScale.x, startSizeCharge2) + startSizeCharge2 * timeHold2, idkHowToNameStuff(trCharge2.localScale.y, startSizeCharge2y) + startSizeCharge2y * timeHold2);
                    //chargebar
                    chargeBarTr.localScale = new Vector3(timeHold, chargeBarTr.localScale.y, chargeBarTr.localScale.z);
                }
                else
                {
                    upButtonUP = false;
                    upButtonUP = false;
                }

                if (Input.GetButton("Cancel"))
                {
                    PauseGame();
                }
                if (!notDead)
                {
                    Destroy(auy);
                }
            }
        }
    }
    IEnumerator killPlayer()
    {
        yield return new WaitForSeconds(0.04f);
        if (public_flag_dead)
        {
            GameObject.FindGameObjectWithTag("MoneyShower").GetComponent<moneyShower>().GameEnd();
            notDead = false;
            died.gameObject.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            
            yield return new WaitForSeconds(died.startLifetime);
            setup.GetComponent<Setup>().Save();
            if (setup.GetComponent<AdHandler>().rewardedAdReadytoShow)
            {
                reviveAd.SetActive(true);
            }

            yield return new WaitForSeconds(0.2f);
            if (!reviveAd.activeSelf)
            {
                setup.GetComponent<AdHandler>().ShowAd_Inter(true);
            }
        }

    }
    public void RevivePlayer_Revive()
    {
        GameObject.FindGameObjectWithTag("MoneyShower").GetComponent<moneyShower>().GameResumed();
        notDead = true;
        died.gameObject.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        mainCamera.GetComponent<Transform>().position = GetComponent<Transform>().position;
    }
    public void RevivePlayer_AdRefused()
    {
        //if ad refused, restart game
        setup.GetComponent<AdHandler>().ShowAd_Inter(true);
        Debug.LogWarning("Ad refused");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kill")
        {
            if (PlayerPrefs.GetInt("LongJumpTutorial", 0) == 0)
            {
                public_flag_dead = true;
                //Bruh("Dieparticles position: " + particleDie.GetComponent<Transform>().position);
                //particleDie.GetComponent<Transform>().position = particleDie.GetComponent<Transform>().position + (collision.gameObject.GetComponent<Transform>().position-GetComponent<Transform>().position);
                //Bruh("Dieparticles position: "+particleDie.GetComponent<Transform>().position+" "+collision.gameObject.GetComponent<Transform>().position + " "+ GetComponent<Transform>().position + ";");
                StartCoroutine("killPlayer");
            }
            else if (collision.gameObject.GetComponent<rotate>().tutorialAsteriod)
            {
                Time.timeScale = 1f;
                //collision.gameObject.GetComponent<Transform>().position = new Vector3(collision.gameObject.GetComponent<Transform>().position.x, collision.gameObject.GetComponent<Transform>().position.y + 0.02f, collision.gameObject.GetComponent<Transform>().position.z);
                tr.position = new Vector3 (lastPlayerPosition.x, lastPlayerPosition.y, lastPlayerPosition.z);
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, lastPlayerPosition.y, mainCamera.transform.position.z);
                PlayerPrefs.SetInt("LongJumpTutorial", 1);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        public_flag_dead = false;
        Time.timeScale = 1f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Kill")
        {
            if (PlayerPrefs.GetInt("LongJumpTutorial", 0) == 1)
            {
                tutorialHandler.GetComponent<tutorialHandler>().LongJumpStart();
            }
            else
            {
                auxxx = 1.7f;
                Time.timeScale = 0.8f;
            }
            if (useCameraAlert)
            {
                StartCoroutine("mainCameraAlert");
            }
        }
    }
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Kill")
        {
            if (PlayerPrefs.GetInt("LongJumpTutorial", 0) == 1)
            {
                Time.timeScale = 1f;
                tr.position = new Vector3(lastPlayerPosition.x, lastPlayerPosition.y - 0.35f, lastPlayerPosition.z);
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, lastPlayerPosition.y, mainCamera.transform.position.z);
                PlayerPrefs.SetInt("LongJumpTutorial", 1);
            }
        }
    }*/
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kill")
        {
            if (PlayerPrefs.GetInt("LongJumpTutorial", 0) == 1)
            {
                Time.timeScale = 1f;
                tr.position = new Vector3(lastPlayerPosition.x, lastPlayerPosition.y - 0.35f, lastPlayerPosition.z);
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, lastPlayerPosition.y, mainCamera.transform.position.z);
                PlayerPrefs.SetInt("LongJumpTutorial", 1);
            }
        }
    }
    IEnumerator mainCameraAlert()
    {
        float alertSize = 0.95f;
        do
        {
            mainCamera.GetComponent<Camera>().orthographicSize = mainCamera.GetComponent<Camera>().orthographicSize - (mainCamera.GetComponent<Camera>().orthographicSize-alertSize) / 100;
            yield return new WaitForEndOfFrame();

        } while (alertSize<= mainCamera.GetComponent<Camera>().orthographicSize);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        auxxx = 1f;
        Time.timeScale = 1f;
        if (useCameraAlert)
        {
            mainCamera.GetComponent<Camera>().orthographicSize = 1;
        }
    }

    IEnumerable tpLast()
    {
        ParticleSystem aux;
        aux = Instantiate(onTp, tr.position, Quaternion.identity);
        yield return new WaitForSeconds(onTp.startLifetime);
        Destroy(aux);
        yield return null;
    }

    private float idkHowToNameStuff(float x, float maxValue)
    {
        float rezultat;
        int auxx;
        rezultat = x;
        //while (rezultat > maxValue)
        //{
        if (useOldChargingSystem)
        {
            if (rezultat > maxValue)
            {
                auxx = Mathf.RoundToInt(rezultat / maxValue);
                rezultat = rezultat - maxValue * auxx;
            }
        }
        else
        {
            if (rezultat > maxValue)
            {
                rezultat = maxValue;
            }
        }
    //Bruh("rezultat= " + rezultat + " maxValue= " + maxValue + " minValue= " + minValue + " corect? " + aux);
    bool aux = (rezultat <= maxValue);
        Bruh("rezultat=" + rezultat + " maxValue=" + maxValue + " corect? " + aux + " maxTeleportDistance="+ maxTeleportDistance);
        return rezultat;
    }
    public void upButtonUPf()
    {
        upButtonUP = true;
    }
    public void upButtonDOWNf()
    {
        upButtonDOWN = true;
    }
    public GameObject pause;
    public void PauseGame()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            PauseGame();
        }
    }

    public void RevivePlayer()
    {

    }
    


 
}
