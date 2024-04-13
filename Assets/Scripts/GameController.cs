using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;


public enum GameState { FreeRoam, Dialog, attacking, idle }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController; //Serialized Field adds a new field when this scripr bet threw in Unity Inspector to add a player controller which means drag an object inside of it

    GameState state;

    private void Start() // Switch the state of the game from free roam to Dialog and vice versa
    {
        if (DialogManagerInk.instance.dialogIsPlaying)
        {
            state = GameState.Dialog;
        }
        else if (!DialogManagerInk.instance.dialogIsPlaying)
        {
            if (state == GameState.Dialog)
                state = GameState.FreeRoam;
        }
    }

    private void Update() // Updates the game state of the game according with the situation
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
        else if (state == GameState.attacking)
        {
            playerController.isAttacking = true;
        }
    }
}
