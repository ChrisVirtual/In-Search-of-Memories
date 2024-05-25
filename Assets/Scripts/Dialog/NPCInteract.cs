using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour, Interactable
{
    [SerializeField] private TextAsset inkJSON;
    //[SerializeField] private GameObject visualCue;

    public void Interact()
    {
        DialogManagerInk.instance.EnterDialogMode(inkJSON);
    }


}
