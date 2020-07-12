using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Save save;

    public void CloseGame()
    {
        Application.Quit(0);
    }

    public void StartGame()
    {
        if(save.level != 0)
        {
            SceneManager.LoadScene(save.level);
        }
        else
        {
            SceneManager.LoadScene(1);
        }       
    }
}
