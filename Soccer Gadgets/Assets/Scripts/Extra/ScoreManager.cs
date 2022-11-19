using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public int scoreToReach;
    private int player1Score = 0;
    private int player2Score = 0;
    public GameObject MatchPointBlue;
    public GameObject MatchPointOrange;
    public Text player1ScoreText;
    public Text player2ScoreText;
    public void Start(){
        MatchPointOrange.SetActive(false);
        MatchPointBlue.SetActive(false);
    }
    public void Player1Goal(){
        player1Score++;
        player1ScoreText.text = player1Score.ToString();
        CheckScore();
    }
    public void Player2Goal(){
        player2Score++;
        player2ScoreText.text = player2Score.ToString();
        CheckScore();
    }
    private void CheckScore(){
        if (player1Score == scoreToReach || player2Score == scoreToReach){
            if (player1Score > player2Score){
                SceneManager.LoadScene("GreenWinner");
            }
            else if (player1Score < player2Score){
                SceneManager.LoadScene("RedWinner");
            }
        }
        if (player1Score == scoreToReach - 1){
            StartCoroutine("Blue");

        }
        if (player2Score == scoreToReach - 1){
            StartCoroutine("Orange");
        }
    }
    IEnumerator Blue(){
        MatchPointBlue.SetActive(true);
        yield return new WaitForSeconds(3);
        MatchPointBlue.SetActive(false);
    }
    IEnumerator Orange(){
        MatchPointOrange.SetActive(true);
        yield return new WaitForSeconds(3);
        MatchPointOrange.SetActive(false);
    }
}