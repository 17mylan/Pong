using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject MainMenu;
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
    }
}
