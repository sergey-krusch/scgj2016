using UnityEngine;

public class Digger: MonoBehaviour
{
    public float XLimit;
    public float Speed;
    public float Direction;

    public void Update()
    {
        var p = transform.position;
        p.x += Direction * Speed * Time.deltaTime;
        if (p.x <= -XLimit)
        {
            p.x = -XLimit;
            Direction *= -1.0f;
        }
        else if (p.x >= XLimit)
        {
            p.x = XLimit;
            Direction *= -1.0f;
        }
        transform.position = p;
    }
}