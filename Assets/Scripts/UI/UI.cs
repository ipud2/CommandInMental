using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    public void Play()
    {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("main");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("UI");
    }

}
