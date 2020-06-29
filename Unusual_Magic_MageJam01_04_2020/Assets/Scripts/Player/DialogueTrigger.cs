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
    Transform playerPos;
    SpriteRenderer sr;
    private void Start()
    {
        DialogueManager.instance.OnDialogueEnded += Instance_OnDialogueEnded;
        sr = GetComponent<SpriteRenderer>();
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

        if (playerPos != null)
        {
            if(playerPos.position.x < transform.position.x)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            dialogueNotification.SetActive(true);
            canTalk = true;
            playerPos = collision.transform;
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
            playerPos = null;
        }
    }

}
