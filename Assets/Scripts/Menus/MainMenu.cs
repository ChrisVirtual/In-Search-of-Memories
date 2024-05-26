using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }

    public void NewGame()
    {
        DiableMenuButtons();
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("SaveLoadScene");
    }

    public void LoadGame()
    {
        DiableMenuButtons();
        SceneManager.LoadSceneAsync("SaveLoadScene");
    }

    public void SaveGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void DiableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }

}
