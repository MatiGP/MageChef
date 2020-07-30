using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Save[] save;
    [SerializeField] TextMeshProUGUI startText;

    int currentSaveIndex;

    private void Awake()
    {
        currentSaveIndex = PlayerPrefs.GetInt("selectedLevel");

        if(save[currentSaveIndex].level != 0)
        {
            startText.text = "Kontynuuj";
        }
        else
        {
            startText.text = "Nowa gra";
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
}
