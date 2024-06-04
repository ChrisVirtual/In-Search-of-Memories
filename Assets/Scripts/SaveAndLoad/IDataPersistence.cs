using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData data); // no ref because when we load, we are just reading the data.

    void SaveData(ref GameData data); // ref because we want to modify the game data when we save the game.
}
