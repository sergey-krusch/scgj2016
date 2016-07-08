using System;
using UnityEngine;

public class WaterTank: MonoBehaviour
{
    private VProgressBar progressBar;
    public float WaterLevel;

    public void Awake()
    {
        progressBar = GetComponent<VProgressBar>();
    }

    public void Update()
    {
        progressBar.Value = WaterLevel;
    }
}