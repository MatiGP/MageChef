using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerPoints : MonoBehaviour
{
    public static PlayerPoints instance;
    [SerializeField] TextMeshProUGUI pointsText;

    int points;

    private void Awake()
    {
        instance = this;
    }

    public void IncreasePoints(int pointsIncrease)
    {
        points += pointsIncrease;
        pointsText.text = points.ToString();
    }
}
