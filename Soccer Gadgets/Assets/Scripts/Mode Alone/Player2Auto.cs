using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Auto : MonoBehaviour
{
    public float racketSpeed;

    private Rigidbody2D rb;
    private Vector2 racketDirection;
    public Animator animator;
    public string collided;
    public float randomValue;
    public float directionY;
    Player1Alone player1Alone;
    public GameObject UIplayerRed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collided = "Bottom";
    }
    private void FixedUpdate()
    {
        rb.velocity = racketDirection * racketSpeed;
    }
    void Update()
    {
        float directionY = Time.deltaTime;
        animator.SetFloat("Speed", 1);
        if (GameObject.Find("GreenPlayerUsePower") == true)
        {
            if (collided == "Top")
            {
                transform.Translate(0, -0.001f, 0);
            }
            if (collided == "Bottom")
            {
                transform.Translate(0, 0.001f, 0);
            }
        }
        else if (collided == "Top")
        {
            transform.Translate(0, -8f * directionY, 0);
            randomValue = Random.Range(0f, 100f);
            if (randomValue < 0.22f)
            {
                collided = "Bottom";
            }
        }
        else if (collided == "Bottom")
        {
            transform.Translate(0, 8f * directionY, 0);
            randomValue = Random.Range(0f, 100f);
            if (randomValue < 0.22f)
            {
                collided = "Top";
            }
        }
        if (GameObject.Find("RandomGadgetIce_Orange") == true)
        {
            GameObject.Find("RandomGadgetIce_Orange").SetActive(false);
            StartCoroutine("OrangeIceAuto");
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Top")
        {
            collided = "Top";
        }
        if (collision.gameObject.name == "Bottom")
        {
            collided = "Bottom";
        }
    }
    IEnumerator OrangeIceAuto()
    {
        player1Alone = FindObjectOfType<Player1Alone>();
        player1Alone.racketSpeed = 1;
        UIplayerRed.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        UIplayerRed.SetActive(false);
        player1Alone.racketSpeed = 10;
    }
}