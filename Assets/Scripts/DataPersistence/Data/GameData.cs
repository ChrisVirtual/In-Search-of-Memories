using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int gold;
    public Vector3 playerPosition;

    // The constructor holds initial values when there's no data saved to load. 
    public GameData()
    {
        this.gold = 0;
        playerPosition = Vector3.zero;
    }
}
