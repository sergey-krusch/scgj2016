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

    public float Consume(float target)
    {
        if (target >= WaterLevel)
        {
            var result = WaterLevel;
            WaterLevel = 0;
            return result;
        }
        WaterLevel -= target;
        return target;
    }
}