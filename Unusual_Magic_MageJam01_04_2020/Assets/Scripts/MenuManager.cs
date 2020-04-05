using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void CloseGame()
    {
        Application.Quit(0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
