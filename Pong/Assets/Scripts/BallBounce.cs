using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public SpriteRenderer spriteRenderer;
    public GameObject UIplayerRed;
    public GameObject UIplayerGreen;
    public GameObject ButImage;
    public GameObject ButTextOrange;
    public GameObject ButTextBlue;
    public GameObject Arbitre;
    public bool lastTouch;
    public bool firstTouch = false;
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
    public Sprite RandomGadgetIce;
    public Sprite RandomGadgetSpeed;
    public GameObject RandomGadgetIce_Blue;
    public GameObject RandomGadgetIce_Orange;
    Player1 player1;
    Player2 player2;
    Player1Alone player1Alone;
    Player2Auto player2Auto;
    public float racketSpeed = 10;
    public void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        lastTouch = true; // True = Green, False = Red
        StartCoroutine("InizializeArbitre");
        randomNumber = Random.Range(1, 3);
        if (randomNumber == 1)
        {
            C_Start1.PlayOneShot(CS_clip_1);
        }
        if (randomNumber == 2)
        {
            C_Start2.PlayOneShot(CS_clip_2);
        }
        StartCoroutine("RandomSounds");
        GameObject.Find("RandomGadget").GetComponent<SpriteRenderer>().sprite = RandomGadgetIce;
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
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (GameObject.Find("But") == false)
            {
                if (GameObject.Find("RandomGadgetIce_Blue") == true)
                {
                    GameObject.Find("RandomGadgetIce_Blue").SetActive(false);
                    StartCoroutine("BlueIce");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (GameObject.Find("But") == false)
            {
                if (GameObject.Find("RandomGadgetIce_Orange") == true)
                {
                    GameObject.Find("RandomGadgetIce_Orange").SetActive(false);
                    StartCoroutine("OrangeIce");
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Top" || collision.gameObject.name == "Bottom" || collision.gameObject.name == "RightBorder" || collision.gameObject.name == "RightBorder2" || collision.gameObject.name == "LeftBorder" || collision.gameObject.name == "LeftBorder2")
        {
            if (PlayerPrefs.GetInt("ShakeStatus") == 0)
            {
                StartCoroutine(cameraShake.Shake(.15f, .1f));
            }
            audioSource.PlayOneShot(clip3);
        }
        if (collision.gameObject.name == "RandomGadget")
        {
            if(firstTouch == false)
            {
                firstTouch = true;
            }
            else if(firstTouch == true)
            {
                if (lastTouch == true)
                {
                    if (GameObject.Find("RandomGadget").GetComponent<SpriteRenderer>().sprite == RandomGadgetIce)
                    {
                        RandomGadgetIce_Orange.SetActive(true);
                    }
                }
                if (lastTouch == false)
                {
                    if (GameObject.Find("RandomGadget").GetComponent<SpriteRenderer>().sprite == RandomGadgetIce)
                    {
                        RandomGadgetIce_Blue.SetActive(true);
                    }
                }
            }
        }
        if (collision.gameObject.name == "Player 1" || collision.gameObject.name == "Player 2")
        {
            if (PlayerPrefs.GetInt("ShakeStatus") == 0)
            {
                StartCoroutine(cameraShake.Shake(.15f, .3f));
            }
            if (firstTouch == false)
            {
                firstTouch = true;
            }
            audioSource.PlayOneShot(clip);
            Bounce(collision);
            if (collision.gameObject.name == "Player 1")
            {
                if (lastTouch == true)
                {
                    lastTouch = false;
                }
            }
            else if (collision.gameObject.name == "Player 2")
            {
                if (lastTouch == false)
                {
                    lastTouch = true;
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
            lastTouch = true;
        }
        else if (collision.gameObject.name == "Left")
        {
            audioSource1.PlayOneShot(clip1);
            audioSource1.PlayOneShot(clip4);
            scoreManager.Player2Goal();
            ballMovement.player1Start = true;
            StartCoroutine(ballMovement.Launch());
            StartCoroutine("GoalOrange");
            lastTouch = false;
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
    IEnumerator BlueIce()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Pong Game")
        {
            UIplayerGreen.SetActive(true);
            player2 = FindObjectOfType<Player2>();
            player2.racketSpeed = 1f;
            yield return new WaitForSeconds(1.5f);
            UIplayerGreen.SetActive(false);
            player2.racketSpeed = 10f;
        }
        else
        {
            UIplayerGreen.SetActive(true);
            player2Auto = FindObjectOfType<Player2Auto>();
            player2Auto.racketSpeed = 1f;
            yield return new WaitForSeconds(1.5f);
            UIplayerGreen.SetActive(false);
            player2Auto.racketSpeed = 10f;
        }
    }
    IEnumerator OrangeIce()
    {
        player1 = FindObjectOfType<Player1>();
        player1.racketSpeed = 1;
        UIplayerRed.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        UIplayerRed.SetActive(false);
        player1.racketSpeed = 10;
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
        if (GameObject.Find("But") == false)
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
}