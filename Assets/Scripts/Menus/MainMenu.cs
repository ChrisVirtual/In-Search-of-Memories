using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
<<<<<<< Updated upstream
    public void PlayGame()
=======
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }

        // Add this to listen for scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Clean up listener to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void NewGame()
    {
        DisableMenuButtons();
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("Consumables");
    }

    public void LoadGame()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("Consumables");
    }

    public void SaveGame()
>>>>>>> Stashed changes
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
}
