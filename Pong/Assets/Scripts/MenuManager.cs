using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject MainMenu;
    public GameObject MenuOptions;
    public GameObject MenuCredits;
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ButtonClicked(string _String)
    {
        if (_String == "Play Button")
        {
            SceneManager.LoadScene("Pong Game");
        }
        if (_String == "Credits Button")
        {
            MainMenu.SetActive(false);
            MenuCredits.SetActive(true);
        }
        if (_String == "Return From Credits Button")
        {
            MainMenu.SetActive(true);
            MenuCredits.SetActive(false);
        }
        if (_String == "Options Button")
        {
            MainMenu.SetActive(false);
            MenuOptions.SetActive(true);
        }
        if (_String == "Return From Options Button")
        {
            MainMenu.SetActive(true);
            MenuOptions.SetActive(false);
        }
    }
}
