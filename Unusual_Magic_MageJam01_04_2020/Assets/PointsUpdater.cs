using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsUpdater : MonoBehaviour
{
    [SerializeField]PlayerPoints playerPoints;
    [SerializeField]TextMeshProUGUI pointText;

    // Start is called before the first frame update
    void Start()
    {
        playerPoints.OnPointsAdded += Points_OnPointsAdded;
    }

    private void Points_OnPointsAdded(object sender, PlayerPoints.OnPointsAddedArgs e)
    {
        pointText.text = e.points.ToString();
    }

  
}
