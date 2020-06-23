using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] GameObject dialogueNotification;
    [SerializeField] CinemachineVirtualCamera dialogueCam;
    public Dialogue dialogue;

    bool dialogueOpen;
    bool canTalk;

    private void Start()
    {
        DialogueManager.instance.OnDialogueEnded += Instance_OnDialogueEnded;
    }

    private void Instance_OnDialogueEnded(object sender, System.EventArgs e)
    {
        dialogueOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !dialogueOpen && canTalk)
        {
            DialogueManager.instance.StartDialogue(dialogue);
            dialogueCam.gameObject.SetActive(true);
            dialogueOpen = true;
        }

        if (!dialogueOpen)
        {
            dialogueCam.gameObject.SetActive(false);
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
            dialogueOpen = false;
            canTalk = false;
        }
    }

    
}
