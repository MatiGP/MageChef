using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject player;
 
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            SceneManager.LoadScene(0);
        }
    }
}
