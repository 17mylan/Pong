using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;
    public BallMovement ballMovement;
    public ScoreManager scoreManager;
    public Color Green;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player 1" || collision.gameObject.name == "Player 2")
        {
            Bounce(collision);
            audioSource.PlayOneShot(clip, volume);
            GameObject.Find("Top").SetActive(false); //GetComponent<SpriteRenderer>().color = Green;
            GameObject.Find("Bottom").SetActive(false); //GetComponent<SpriteRenderer>().color = Green;
            GameObject.Find("Right").SetActive(false); //GetComponent<SpriteRenderer>().color = Green;
            GameObject.Find("Left").SetActive(false); //GetComponent<SpriteRenderer>().color = Green;
        }
        else if(collision.gameObject.name == "Right")
        {
            scoreManager.Player1Goal();
            ballMovement.player1Start = false;
            StartCoroutine(ballMovement.Launch());
        }
        else if (collision.gameObject.name == "Left")
        {
            scoreManager.Player2Goal();
            ballMovement.player1Start = true;
            StartCoroutine(ballMovement.Launch());
        }
    }
}
