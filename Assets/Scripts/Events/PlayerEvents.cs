using System;
using UnityEngine;

//PlayerEvents class responsible for managing player-related events
public class PlayerEvents
{
    //Event for when experience is gained
    public event Action<int> onExperienceGained;

    //Method to invoke the onExperienceGained event with the given experience
    public void ExperienceGained(int experience)
    {
        if (onExperienceGained != null)
        {
            onExperienceGained(experience);
        }
    }

    //Event for when player's level changes
    public event Action<int> onPlayerLevelChange;

    //Method to invoke the onPlayerLevelChange event with the given level
    public void PlayerLevelChange(int level)
    {
        if (onPlayerLevelChange != null)
        {
            Debug.Log("playerLevelChange");
            onPlayerLevelChange(level);
        }
    }

    //Event for when player's experience changes
    public event Action<int> onPlayerExperienceChange;

    //Method to invoke the onPlayerExperienceChange event with the given experience
    public void PlayerExperienceChange(int experience)
    {
        if (onPlayerExperienceChange != null)
        {
            onPlayerExperienceChange(experience);
        }
    }
}
