using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    public void ToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ToMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void ToTutorialPage1()
    {
        SceneManager.LoadScene("TutorialPage1");
    }

    public void ToTutorialPage2()
    {
        SceneManager.LoadScene("TutorialPage2");
    }

    public void ToTutorialPage3()
    {
        SceneManager.LoadScene("TutorialPage3");
    }
}
