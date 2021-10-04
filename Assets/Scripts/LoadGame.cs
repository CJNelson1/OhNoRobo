using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void StartIntro()
    {
        SceneManager.LoadScene("Story");
    }
    public void StartTutorial()
    {
        SceneManager.LoadScene(5);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Torso");
    }
}
