using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Save[] save;
    [SerializeField] TextMeshProUGUI startText;

    int currentSave;

    private void Awake()
    {
        currentSave = PlayerPrefs.GetInt("selectedLevel");

    }

    public void CloseGame()
    {
        Application.Quit(0);
    }

    public void StartGame()
    {
        if(save[currentSave].level != 0)
        {
            SceneManager.LoadScene(save[currentSave].level);
        }
        else
        {
            SceneManager.LoadScene(1);
        }       
    }
}
