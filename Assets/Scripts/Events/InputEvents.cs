using UnityEngine;
using System;

//InputEvents class responsible for managing input-related events
public class InputEvents
{
    //Event for when movement is pressed
    public event Action<Vector2> onMovePressed;

    //Method to invoke the onMovePressed event with the given moveDir
    public void MovePressed(Vector2 moveDir)
    {
        if (onMovePressed != null)
        {
            onMovePressed(moveDir);
        }
    }

    //Event for when submission is pressed
    public event Action onSubmitPressed;

    //Method to invoke the onSubmitPressed event
    public void SubmitPressed()
    {
        if (onSubmitPressed != null)
        {
            onSubmitPressed();
        }
    }

    //Event for when quest log toggle is pressed
    public event Action onQuestLogTogglePressed;

    //Method to invoke the onQuestLogTogglePressed event
    public void QuestLogTogglePressed()
    {
        if (onQuestLogTogglePressed != null)
        {
            onQuestLogTogglePressed();
        }
    }
    public event Action onMapTogglePressed;

    //Method to invoke the onQuestLogTogglePressed event
    public void MapTogglePressed()
    {
        if (onMapTogglePressed != null)
        {
            onMapTogglePressed();
        }
    }

}