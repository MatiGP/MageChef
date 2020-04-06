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
    private void Start()
    {
        IncreasePoints(PlayerPrefs.GetInt("score"));
    }

    public int GetScore()
    {
        return points;
    }

    public void IncreasePoints(int pointsIncrease)
    {
        points += pointsIncrease;
        pointsText.text = points.ToString();
    }
}
