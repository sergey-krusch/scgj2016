using UnityEngine;

public class GameplayInput: MonoBehaviour
{
    private Collider2D[] candidates;

    public void Awake()
    {
        candidates = new Collider2D[32];
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var s = Physics2D.OverlapPointNonAlloc(p, candidates);
            HandleAnimals(s);
        }
    }

    private void HandleAnimals(int s)
    {
        var bestValue = float.NegativeInfinity;
        Animal.Subject bestAnimal = null;
        for (var i = 0; i < s; ++i)
        {
            var a = candidates[i].GetComponent<Animal.Subject>();
            if (a == null)
                continue;
            if (a.Value > bestValue)
            {
                bestValue = a.Value;
                bestAnimal = a;
            }
        }
        if (bestAnimal != null)
            bestAnimal.SignalTapDown();
    }
}