using UnityEngine;

public class Gameplay: MonoBehaviour
{
    public float InitialWaterLevel;
    public float WaterLevelDropRate;

    public WaterTank WaterTank;

    public void Awake()
    {
        WaterTank.WaterLevel = InitialWaterLevel;
    }

    public void Update()
    {
        var wl = WaterTank.WaterLevel;
        wl -= WaterLevelDropRate * Time.deltaTime;
        WaterTank.WaterLevel = Mathf.Clamp01(wl);
    }

}