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
    public GameObject MenuControls;
    public GameObject MenuChoice;
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
        if (_String == "Menu Choice")
        {
            MainMenu.SetActive(false);
            MenuChoice.SetActive(true);
        }
        if (_String == "Play Button 2 Players")
        {
            SceneManager.LoadScene("Pong Game");
        }
        if (_String == "Play Button Alone")
        {
            SceneManager.LoadScene("Pong Game Alone");
        }
        if (_String == "Return From Menu Choice")
        {
            MenuChoice.SetActive(false);
            MainMenu.SetActive(true);
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
        if (_String == "Controls Button")
        {
            MenuControls.SetActive(true);
            MenuOptions.SetActive(false);
        }
        if (_String == "Return From Controls Button")
        {
            MenuOptions.SetActive(true);
            MenuControls.SetActive(false);
        }
    }
}
