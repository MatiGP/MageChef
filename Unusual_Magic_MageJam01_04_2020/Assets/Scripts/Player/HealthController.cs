using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] GameObject remainingLifeHolder;
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
    
    public void IncreaseHP()
    {
        GameObject healthObject = new GameObject();
        Image newHealthSR = healthObject.AddComponent<Image>();
        RectTransform rectTransform = remainingLifeHolder.GetComponent<RectTransform>();

        newHealthSR.sprite = listOfHPImages[0].sprite;
        healthObject.transform.parent = remainingLifeHolder.transform;
        listOfHPImages.Add(newHealthSR);
        hp.IncreaseMaxHP();
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.rect.width + 60);
    }

}
