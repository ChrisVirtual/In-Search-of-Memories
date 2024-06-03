using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryMenu : MonoBehaviour
{
    public void RetryButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Credits");
    }
}
