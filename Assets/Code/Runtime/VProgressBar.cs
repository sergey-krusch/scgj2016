using UnityEngine;

public class VProgressBar: MonoBehaviour
{
    public float FullScale;
    public float EmptyScale;
    public float Value;

    public GameObject Indicator;

    public void Update()
    {
        var s = Indicator.transform.localScale;
        s.y = EmptyScale + (FullScale - EmptyScale) * Value;
        Indicator.transform.localScale = s;
    }
}