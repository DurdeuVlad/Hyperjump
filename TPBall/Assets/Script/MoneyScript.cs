using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    public Transform tr, trNumber;
    private Text MoneyShower;
    private Image MoneyShowerIcon;
    // Movement speed in units/sec.
    public float speed = 1.0F;
    public float rotationSpeed = 1.0f;
    private bool yeet = false;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    void Start()
    {
        MoneyShower= GameObject.Find("MoneyShower").GetComponent<Text>();
        MoneyShowerIcon= GameObject.Find("MoneyShowerIcon").GetComponent<Image>();
        trNumber = GameObject.FindGameObjectWithTag("MoneyDelete").GetComponent<Transform>();
        startTime = Time.time;
        journeyLength = Vector3.Distance(tr.position, trNumber.position);
        tr = gameObject.transform;
        tr.localRotation = Quaternion.Euler(0, 0, Random.Range(-360, 360));
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            yeet = true;
            MoneyShower.CrossFadeAlpha(1, 0.5f, true);
            MoneyShowerIcon.CrossFadeAlpha(1, 0.5f, true);
            MoneyShower.GetComponent<moneyShower>().aparinos();
            MoneyShower.GetComponent<moneyShower>().Disparion();
        }
        if (collision.gameObject.tag == "MoneyDelete")
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        MoneyShower.GetComponent<moneyShower>().AddOne();
    }
    private void Update()
    {
        tr.rotation = Quaternion.Euler(0, 0, gameObject.transform.rotation.eulerAngles.z + rotationSpeed * Time.deltaTime);
        if (yeet)
        {
            if (tr.localScale.x >= 0.05f && tr.localScale.y >= 0.05f)
            {
                tr.localScale = new Vector3(tr.localScale.x - Time.deltaTime, tr.localScale.y - Time.deltaTime, 1);
            }
                // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(tr.position, trNumber.position, fracJourney);
            if (tr.position.x<= trNumber.position.x || tr.position.y<=trNumber.position.y)
            {
                Destroy(gameObject);
            }
        }
    }

}
