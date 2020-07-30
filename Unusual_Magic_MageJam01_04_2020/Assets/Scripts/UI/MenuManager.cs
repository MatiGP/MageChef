using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Save[] save;
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] GameObject newGameButton;

    int currentSaveIndex;

    private void Awake()
    {
        currentSaveIndex = PlayerPrefs.GetInt("selectedLevel");

        if(save[currentSaveIndex].level != 0)
        {
            startText.text = "Kontynuuj";
            newGameButton.SetActive(true);
        }
        else
        {
            startText.text = "Nowa gra";
            newGameButton.SetActive(false);
        }

    }

    public void CloseGame()
    {
        Application.Quit(0);
    }

    public void StartGame()
    {
        if(save[currentSaveIndex].level != 0)
        {
            SceneManager.LoadScene(save[currentSaveIndex].level);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }       
    }

    public void StartNewGame()
    {
        save[currentSaveIndex].level = 0;
        save[currentSaveIndex].SaveFile(currentSaveIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
