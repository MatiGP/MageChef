using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveMenuUISetup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] saveSlotText;
    [SerializeField] Save[] saves;
    [SerializeField] Image[] busySlotIcon;

    public void SelectSave(int selection)
    {
        PlayerPrefs.SetInt("selectedLevel", selection);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetSlot(int index)
    {
        if (saves[index].level >= 4)
        {
            busySlotIcon[index].gameObject.SetActive(true);
            saveSlotText[index].text = $"Poziom : {saves[index].level - 3}";
        }
        else
        {
            busySlotIcon[index].gameObject.SetActive(false);
            saveSlotText[index].text = $"-Puste-";
        }
    }


}
