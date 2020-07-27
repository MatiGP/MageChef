using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellbookSpice : MonoBehaviour
{
    [SerializeField] Image spiceIcon;
    [SerializeField] TextMeshProUGUI spiceName;
    [SerializeField] TextMeshProUGUI spiceCount;

    public void SetUpSpice(Spice spice, int count)
    {
        spiceIcon.sprite = spice.spiceIcon;
        spiceName.text = spice.spiceName;
        spiceCount.text = count.ToString();
    }

    
}
