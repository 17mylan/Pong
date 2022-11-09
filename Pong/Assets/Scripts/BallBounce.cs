using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallBounce : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public AudioSource audioSource1;
    public AudioClip clip1;
    public BallMovement ballMovement;
    public ScoreManager scoreManager;
    public CameraShake cameraShake;
    public int RedBallTouch = 0;
    public int GreenBallTouch = 0;
    public Text redBallTouchText;
    public Text greenBallTouchText;
    public SpriteRenderer spriteRenderer;
    public Sprite GreenChargEmpty;
    public Sprite RedChargEmpty;
    public Sprite Charg1;
    public Sprite Charg2;
    public Sprite Charg3;
    public GameObject UIplayerRed;
    public GameObject UIplayerGreen;
    public bool lastTouch;
    public void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        lastTouch = true; // True = Green, False = Red
    }
    private void Bounce(Collision2D collision)
    {
        Vector3 ballPosition = transform.position;
        Vector3 racketPosition = collision.transform.position;
        float racketHeight = collision.collider.bounds.size.y;
        float positionX;
        if(collision.gameObject.name == "Player 1")
        {
            positionX = 1;
        }
        else
        {
            positionX = -1;
        }
        float positionY = (ballPosition.y - racketPosition.y) / racketHeight;
        ballMovement.IncreaseHitCounter();
        ballMovement.MoveBall(new Vector2(positionX, positionY));
    }
    IEnumerator Green()
    {
        UIplayerGreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        UIplayerGreen.SetActive(false);
    }
    IEnumerator Red()
    {
        UIplayerRed.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        UIplayerRed.SetActive(false);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(GreenBallTouch >= 3)
            {
                GreenBallTouch = 0;
                greenBallTouchText.text = GreenBallTouch.ToString();
                GameObject.Find("GreenChargEmpty").GetComponent<SpriteRenderer>().sprite = GreenChargEmpty;
                GameObject.Find("GreenChargEmpty").GetComponent<SpriteRenderer>().color = Color.gray;
                StartCoroutine("Green");
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (RedBallTouch >= 3)
            {
                RedBallTouch = 0;
                redBallTouchText.text = RedBallTouch.ToString();
                GameObject.Find("RedChargEmpty").GetComponent<SpriteRenderer>().sprite = RedChargEmpty;
                GameObject.Find("RedChargEmpty").GetComponent<SpriteRenderer>().color = Color.gray;
                StartCoroutine("Red");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(cameraShake.Shake(.15f, .1f));
        if (collision.gameObject.name == "Player 1" || collision.gameObject.name == "Player 2")
        {
            audioSource.PlayOneShot(clip);
            Bounce(collision);
            if (collision.gameObject.name == "Player 1")
            {
                if (lastTouch == true)
                {
                    GreenBallTouch++;
                    lastTouch = false;
                    if (GreenBallTouch <= 3)
                    {
                        greenBallTouchText.text = GreenBallTouch.ToString();
                    }
                    if (GreenBallTouch == 1)
                    {
                        GameObject.Find("GreenChargEmpty").GetComponent<SpriteRenderer>().sprite = Charg1;
                        GameObject.Find("GreenChargEmpty").GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    else if (GreenBallTouch == 2)
                    {
                        GameObject.Find("GreenChargEmpty").GetComponent<SpriteRenderer>().sprite = Charg2;
                        GameObject.Find("GreenChargEmpty").GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    else if (GreenBallTouch == 3)
                    {
                        GameObject.Find("GreenChargEmpty").GetComponent<SpriteRenderer>().sprite = Charg3;
                        GameObject.Find("GreenChargEmpty").GetComponent<SpriteRenderer>().color = Color.green;
                    }
                }
            }
            else if(collision.gameObject.name == "Player 2")
            {
                if (lastTouch == false)
                {
                    RedBallTouch++;
                    lastTouch = true;
                    if (RedBallTouch <= 3)
                    {
                        redBallTouchText.text = RedBallTouch.ToString();
                    }
                    if (RedBallTouch == 1)
                    {
                        GameObject.Find("RedChargEmpty").GetComponent<SpriteRenderer>().sprite = Charg1;
                        GameObject.Find("RedChargEmpty").GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    else if (RedBallTouch == 2)
                    {
                        GameObject.Find("RedChargEmpty").GetComponent<SpriteRenderer>().sprite = Charg2;
                        GameObject.Find("RedChargEmpty").GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    else if (RedBallTouch == 3)
                    {
                        GameObject.Find("RedChargEmpty").GetComponent<SpriteRenderer>().sprite = Charg3;
                        GameObject.Find("RedChargEmpty").GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }
            }
        }
        else if(collision.gameObject.name == "Right")
        {
            audioSource1.PlayOneShot(clip1);
            scoreManager.Player1Goal();
            ballMovement.player1Start = false;
            StartCoroutine(ballMovement.Launch());
        }
        else if (collision.gameObject.name == "Left")
        {
            audioSource1.PlayOneShot(clip1);
            scoreManager.Player2Goal();
            ballMovement.player1Start = true;
            StartCoroutine(ballMovement.Launch());
        }
    }
}