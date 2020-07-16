using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellbookSpice : MonoBehaviour
{
    [SerializeField] Image spiceIcon;
    [SerializeField] TextMeshProUGUI spiceName;

    public void SetUpSpice(Spice spice)
    {
        spiceIcon.sprite = spice.spiceIcon;
        spiceName.text = spice.spiceName;
    }

    
}
