using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{

    [SerializeField] Animator levelTransitionAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SaveSystem.instance.SaveState();
            SaveSystem.instance.ResetCheckpoint();
            StartCoroutine(TransistionToNextLevel());           
        }
    }

    IEnumerator TransistionToNextLevel()
    {
        levelTransitionAnimator.SetTrigger("startTransition");

        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

}
