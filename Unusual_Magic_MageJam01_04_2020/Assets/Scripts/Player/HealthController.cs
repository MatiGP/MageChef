using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{   
    [SerializeField] List<Image> listOfHPImages;
    Health hp;
    
    void Start()
    {
        hp = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {       
        for(int i = 0; i < hp.GetHealthAmmount(); i++)
        {
            listOfHPImages[i].gameObject.SetActive(true);
        }
        for(int i = hp.GetHealthAmmount(); i < 4; i++)
        {
            listOfHPImages[i].gameObject.SetActive(false);
        }
    }
}
