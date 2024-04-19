using System;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerEvents
{

    public event Action<int> onExperienceGained;
    public void ExperienceGained(int experience) 
    {
        if (onExperienceGained != null) 
        {
            onExperienceGained(experience);
        }
    }

    public event Action<int> onPlayerLevelChange;
    public void PlayerLevelChange(int level) 
    {
        
        if (onPlayerLevelChange != null) 
        {
            Debug.Log("playerLevelChange");
            onPlayerLevelChange(level);
        }
    }

    public event Action<int> onPlayerExperienceChange;
    public void PlayerExperienceChange(int experience) 
    {
        if (onPlayerExperienceChange != null) 
        {
            onPlayerExperienceChange(experience);
        }
    }
}