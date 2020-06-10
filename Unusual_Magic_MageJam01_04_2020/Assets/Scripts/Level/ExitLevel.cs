using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] int levelIndexToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SaveSystem.instance.SaveState();
            SaveSystem.instance.ResetCheckpoint();
            SceneManager.LoadScene(levelIndexToLoad);           
        }
    }
}
