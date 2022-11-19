using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public Image On;
    public Image Off;
    public void Start(){
        if(PlayerPrefs.GetInt("ShakeStatus") == 0){
            On.gameObject.SetActive(true);
            Off.gameObject.SetActive(false);
        }
        if(PlayerPrefs.GetInt("ShakeStatus") == 1){
            Off.gameObject.SetActive(true);
            On.gameObject.SetActive(false);
        }
    }
    public void ON(){
        Off.gameObject.SetActive(true);
        On.gameObject.SetActive(false);
        PlayerPrefs.SetInt("ShakeStatus", 1);
    }
    public void OFF(){
        On.gameObject.SetActive(true);
        Off.gameObject.SetActive(false);
        PlayerPrefs.SetInt("ShakeStatus", 0);
    }
}
