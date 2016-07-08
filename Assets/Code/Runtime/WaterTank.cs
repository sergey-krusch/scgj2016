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

    public float Consume(float amount)
    {
        if (amount >= WaterLevel)
        {
            var result = WaterLevel;
            WaterLevel = 0;
            return result;
        }
        WaterLevel -= amount;
        return amount;
    }

    public void Produce(float amount)
    {
        WaterLevel = Mathf.Clamp01(WaterLevel + amount);
    }
}