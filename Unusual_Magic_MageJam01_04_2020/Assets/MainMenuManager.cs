using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject menu;

    bool isMenuOpen = false;
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isMenuOpen)
        {
            AudioListener.volume = 0.1f;
            Time.timeScale = 0;
            menu.SetActive(true);
            isMenuOpen = true;
            
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isMenuOpen)
        {            
            Time.timeScale = 1;
            AudioListener.volume = 1f;
            menu.SetActive(false);
            isMenuOpen = false;
        }
    }

    public void ResumeGame()
    {
        AudioListener.volume = 1f;
        Time.timeScale = 1;
        menu.SetActive(false);
        isMenuOpen = false;
    }

    public void MainMenu()
    {
        AudioListener.volume = 1f;
        SceneManager.LoadScene(0);
        isMenuOpen = false;
    }
}
