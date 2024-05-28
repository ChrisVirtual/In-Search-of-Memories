using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Dialog, Attacking, Idle }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    GameState state;

    private void Start()
    {
        InitializeGameState();
        ResetPlayerController();
    }

    private void InitializeGameState()
    {
        if (DialogManagerInk.instance.dialogIsPlaying)
        {
            state = GameState.Dialog;
        }
        else
        {
            state = GameState.FreeRoam;
        }
    }

    private void ResetPlayerController()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.ResetState();
        }
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        ResetPlayerController();
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.FreeRoam:
                if (playerController != null)
                {
                    playerController.HandleUpdate();
                }
                break;
            case GameState.Dialog:
                DialogManager.Instance.HandleUpdate();
                break;
            case GameState.Attacking:
                if (playerController != null)
                {
                    playerController.isAttacking = true;
                }
                break;
        }
    }

    public void SetGameState(GameState newState)
    {
        state = newState;
        if (newState == GameState.FreeRoam && playerController != null)
        {
            playerController.ResetState();
        }
    }
}
