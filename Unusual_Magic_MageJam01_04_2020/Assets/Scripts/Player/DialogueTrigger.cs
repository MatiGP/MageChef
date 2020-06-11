using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] GameObject dialogueNotification;
    public Dialogue dialogue;

    bool dialogOpen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            dialogueNotification.SetActive(true);           
        }
    }

    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetKeyDown(KeyCode.W) && !dialogOpen)
            {
                DialogueManager.instance.StartDialogue(dialogue);
                dialogOpen = true;
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialogueNotification.SetActive(false);
            DialogueManager.instance.EndDialogue();
            dialogOpen = false;
        }
    }

    
}
