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
    public AudioSource audioSource2;
    public AudioClip clip2;
    public AudioSource audioSource3;
    public AudioClip clip3;
    public AudioSource audioSource4;
    public AudioClip clip4;
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
    public Sprite BlueShoot;
    public GameObject UIplayerRed;
    public GameObject UIplayerGreen;
    public GameObject ButImage;
    public GameObject ButTextOrange;
    public GameObject ButTextBlue;
    public GameObject Arbitre;
    public bool lastTouch;
    public int randomNumber;

    public AudioSource C_Start1;
    public AudioClip CS_clip_1;
    public AudioSource C_Start2;
    public AudioClip CS_clip_2;

    public AudioSource Commentary;
    public AudioClip C_clip_1;
    public AudioSource Commentary2;
    public AudioClip C_clip_2;
    public AudioSource Commentary3;
    public AudioClip C_clip_3;
    public AudioSource Commentary4;
    public AudioClip C_clip_4;
    public AudioSource Commentary5;
    public AudioClip C_clip_5;
    public AudioSource Commentary6;
    public AudioClip C_clip_6;
    public AudioSource Commentary7;
    public AudioClip C_clip_7;
    public void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        lastTouch = true; // True = Green, False = Red
        StartCoroutine("InizializeArbitre");
        randomNumber = Random.Range(1, 3);
        if(randomNumber == 1)
        {
            C_Start1.PlayOneShot(CS_clip_1);
        }
        if(randomNumber == 2)
        {
            C_Start2.PlayOneShot(CS_clip_2);
        }
        StartCoroutine("RandomSounds");
    }
    IEnumerator InizializeArbitre()
    {
        Arbitre.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        Arbitre.SetActive(false);
    }
    IEnumerator RandomSounds()
    {
        yield return new WaitForSeconds(13); //    CHANGER POUR METTRE PLUS DE TEMPS ENVIRON 12secondes
        randomNumber = Random.Range(1, 8);
        if(GameObject.Find("But") == false)
        {
            if (randomNumber == 1)
            {
                Commentary.PlayOneShot(C_clip_1);
            }
            else if (randomNumber == 2)
            {
                Commentary2.PlayOneShot(C_clip_2);
            }
            else if (randomNumber == 3)
            {
                Commentary3.PlayOneShot(C_clip_3);
            }
            else if (randomNumber == 4)
            {
                Commentary4.PlayOneShot(C_clip_4);
            }
            else if (randomNumber == 5)
            {
                Commentary5.PlayOneShot(C_clip_5);
            }
            else if (randomNumber == 6)
            {
                Commentary6.PlayOneShot(C_clip_6);
            }
            else if (randomNumber == 7)
            {
                Commentary7.PlayOneShot(C_clip_7);
            }
            else
            {
                print("Aucun son joué");
            }
            StartCoroutine("RandomSounds");
        }
        else
        {
            //Restart Coroutine because sound was normally played during goal!
            StartCoroutine("RandomSounds");
        }
        
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
        if (collision.gameObject.name == "Top" || collision.gameObject.name == "Bottom" || collision.gameObject.name == "RightBorder" || collision.gameObject.name == "RightBorder2" || collision.gameObject.name == "LeftBorder" || collision.gameObject.name == "LeftBorder2")
        {
            StartCoroutine(cameraShake.Shake(.15f, .1f));
            audioSource.PlayOneShot(clip3);
        }
        if (collision.gameObject.name == "Player 1" || collision.gameObject.name == "Player 2")
        {
            StartCoroutine(cameraShake.Shake(.15f, .3f));
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
            audioSource1.PlayOneShot(clip2);
            audioSource1.PlayOneShot(clip4);
            scoreManager.Player1Goal();
            ballMovement.player1Start = false;
            StartCoroutine(ballMovement.Launch());
            StartCoroutine("GoalBlue");
        }
        else if (collision.gameObject.name == "Left")
        {
            audioSource1.PlayOneShot(clip1);
            audioSource1.PlayOneShot(clip4);
            scoreManager.Player2Goal();
            ballMovement.player1Start = true;
            StartCoroutine(ballMovement.Launch());
            StartCoroutine("GoalOrange");
        }
    }
    IEnumerator GoalBlue()
    {
        ButImage.SetActive(true);
        ButTextBlue.SetActive(true);
        yield return new WaitForSeconds(3);
        ButImage.SetActive(false);
        ButTextBlue.SetActive(false);
    }
    IEnumerator GoalOrange()
    {
        ButImage.SetActive(true);
        ButTextOrange.SetActive(true);
        yield return new WaitForSeconds(3);
        ButImage.SetActive(false);
        ButTextOrange.SetActive(false);
    }
}