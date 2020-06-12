using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] GameObject dialogueNotification;
    public Dialogue dialogue;

    bool dialogOpen;
    bool canTalk;

    private void Start()
    {
        DialogueManager.instance.OnDialogueEnded += Instance_OnDialogueEnded;
    }

    private void Instance_OnDialogueEnded(object sender, System.EventArgs e)
    {
        dialogOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !dialogOpen && canTalk)
        {
            DialogueManager.instance.StartDialogue(dialogue);
            dialogOpen = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            dialogueNotification.SetActive(true);
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialogueNotification.SetActive(false);
            DialogueManager.instance.EndDialogue();
            dialogOpen = false;
            canTalk = false;
        }
    }

    
}
