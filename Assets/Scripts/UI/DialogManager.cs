using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;

    [SerializeField] int lettersPerSecond; // allow to show on Unity

    public event Action OnShowDialog;
    public event Action OnHideDialog;

    public static DialogManager Instance { get; private set; }

    AudioManager audioManager; // Reference to the AudioManager script

    private void Awake() // This will expose Dialog Manager to the "world" allowing any class to be able to access this
    {
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // Get reference to AudioManager
    }

    Dialog dialog;
    int currentLine = 0;
    bool isTyping;

    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame(); // make a waiting time for the next frame be invoked
        OnShowDialog?.Invoke();

        this.dialog = dialog; // allow to call dialog variable multiple times
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyUp(KeyCode.E) && !isTyping)
        {
            ++currentLine;
            if (currentLine < dialog.Lines.Count) // If this is less than the total number of lines it will show the next line
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else // It will turn off the dialogBox marking the end of the dialog
            {
                dialogBox.SetActive(false);
                currentLine = 0; // it will reset the chat, which means the player can talk with the NPCs multiple times
                OnHideDialog?.Invoke();
            }
        }
    }

    public IEnumerator TypeDialog(string line) // it will show a letter one by one like a person talking instead to just throw all the dialog at once
    {
        isTyping = true;
        dialogText.text = ""; 
        foreach (var letter in line.ToCharArray()) 
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }
}
