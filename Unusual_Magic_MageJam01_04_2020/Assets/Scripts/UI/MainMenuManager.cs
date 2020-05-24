using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject spellBook;
    bool isMenuOpen = false;
    bool isSpellBookOpen = false;
   
    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q) && !isSpellBookOpen)
        {
            spellBook.SetActive(true);
            isSpellBookOpen = true;
        }else if(Input.GetKeyDown(KeyCode.Q) && isSpellBookOpen)
        {
            spellBook.SetActive(false);
            isSpellBookOpen = false;
        }

        


        if (Input.GetKeyDown(KeyCode.Escape) && !isMenuOpen)
        {
            AudioListener.volume = 0.1f;
            Time.timeScale = 0f;
            menu.SetActive(true);
            isMenuOpen = true;
            
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isMenuOpen)
        {            
            Time.timeScale = 1f;
            AudioListener.volume = 1f;
            menu.SetActive(false);
            isMenuOpen = false;
        }
    }

    public void ResumeGame()
    {
        AudioListener.volume = 1f;
        Time.timeScale = 1f;
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
