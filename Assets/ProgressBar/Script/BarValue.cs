using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarValue : MonoBehaviour
{
    public Canvas canvas;
    public ProgressBar Pb;
    public float Value = 69;

    void Start()
    {
        Value = canvas.GetComponent<LevelManagement>().getGivenNumver();
    }

    // Update is called once per frame
    void Update()
    {
        //Pb.BarValue = Value;
        Pb.BarValue = canvas.GetComponent<LevelManagement>().getGivenNumver();
    }
}
