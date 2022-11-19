using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] RectTransform fader;
    private void Start()
    {
        fader.gameObject.SetActive(true);
         LeanTween.alpha (fader, 1, 0);
         LeanTween.alpha (fader, 0, 0.5f).setOnComplete (() => {
             fader.gameObject.SetActive (false);
        });
    }
}