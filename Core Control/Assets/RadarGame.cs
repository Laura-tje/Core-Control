using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class RadarGame : MonoBehaviour
{
    [SerializeField] bool isRadarActive = true;
    [SerializeField] GameObject ChosenButton;
    [SerializeField] GameObject[] Buttons;
    void Start()
    {
        Buttons = GameObject.FindGameObjectsWithTag("RadarButtons");
        foreach (var button in Buttons)
        {
            button.SetActive(false);
        }

    }

    void Update()
    {
        
    }
}
