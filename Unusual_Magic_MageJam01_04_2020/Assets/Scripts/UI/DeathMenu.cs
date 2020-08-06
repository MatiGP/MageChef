using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void LoadCheckponit()
    {
        SaveSystem.instance.LoadState();
        gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        SaveSystem.instance.ResetCheckpoint();
        SceneManager.LoadScene(0);       
    }
}
