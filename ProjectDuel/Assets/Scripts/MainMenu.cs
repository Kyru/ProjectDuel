using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private string SceneToLoad;
    public void PlayGame()
    {
        Invoke("LoadScene", 1);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void QuitGame()
    {
        Invoke("Quit", 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
