using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistCanvas : MonoBehaviour
{
    // Called when the script instance is being loaded.
    void Start()
    {
        // Make the GameObject persistent across scenes.
        DontDestroyOnLoad(gameObject);
    }
}
