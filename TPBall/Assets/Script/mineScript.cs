using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mineScript : MonoBehaviour
{
    [SerializeField] private float waitUntilAttack, speed, timeAttackStart;
    [SerializeField] private GameObject laser;
    [SerializeField] private Sprite Idle_Sprite, Active_Sprite;
    [SerializeField] private ParticleSystem die;
    private GameObject Player;
    private Transform playerTr;
    private bool charge, attack;
    private Rigidbody2D rg;
    // Start is called before the first frame update
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerTr = Player.GetComponent<Transform>();
        rg = GetComponent<Rigidbody2D>();
        charge = false;
        attack = false;
        gameObject.GetComponent<rotate>().rotationSpeed = 0;
        gameObject.GetComponent<rotate>().speedDownwards = 0.2f;
        gameObject.GetComponent<rotate>().scale = 1f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&&!attack)
        {
            charge = true;
            timeAttackStart = Time.time;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !attack)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Idle_Sprite;
            laser.SetActive(false);
            charge = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kill"&&attack)
        {
            Destroy(collision.gameObject);
            Instantiate(die, gameObject.transform);
            Destroy(gameObject);    
        }
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    { 
        if (charge)
        {
            Vector3 point = playerTr.position;
            gameObject.GetComponent<SpriteRenderer>().sprite = Active_Sprite;
            laser.SetActive(true);

            Vector3 targetDir = point - transform.position;
            float angle = Vector3.Angle(targetDir, transform.up);
            transform.Rotate(new Vector3(0, 0, angle/10));
            if (timeAttackStart+ waitUntilAttack < Time.time)
            {
                charge = false;
                attack = true;
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        }
        if (attack)
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            rg.velocity = transform.up * speed * Time.deltaTime;
        }
        

    }

}
