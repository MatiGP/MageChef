using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public float typeTime = 0.04f;

    public event EventHandler OnDialogueEnded;    

    public static DialogueManager instance;

    [SerializeField] PlayerController playerController;
    [SerializeField] Animator animator;
    Queue<string> sentences;

    bool typingSentence;
    string currentSentence;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sentences = new Queue<string>();

    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        playerController.DisableMovement();
        playerController.DisableJumpingDialogOpen();

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0 && !typingSentence)
        {
            EndDialogue();
            return;
        }
        
        if (!typingSentence)
        {
            currentSentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentSentence));
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = currentSentence;
            typingSentence = false;
        }

        
        
    }

    IEnumerator TypeSentence(string sentence)
    {
        typingSentence = true;
        dialogueText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeTime);
        }
        typingSentence = false;
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        playerController.EnableMovement();
        playerController.EnableJumpingDialogClose();
        OnDialogueEnded?.Invoke(this, EventArgs.Empty);
    }
}
