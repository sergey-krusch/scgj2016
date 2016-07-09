using Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay: MonoBehaviour
{
    public WaterTank WaterTank;
    public Transform AnimalContainer;
    public GameObject AnimalPrefab;
    public Digger.Subject Digger;

    private bool started;
    private float spawnRemainingTime;

    public void Awake()
    {
        WaterTank.WaterLevel = Root.Instance.InitialWaterLevel;
        Digger.TappedEvent += () =>
        {
            started = true;
        };
    }

    public void Update()
    {
        HandleSpawn();
    }

    public void ReplayClick()
    {
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }

    private void HandleSpawn()
    {
        if (!started)
            return;
        spawnRemainingTime -= Time.deltaTime;
        if (spawnRemainingTime < 0)
        {
            spawnRemainingTime = Root.Instance.AnimalSpawnInterval;
            SpawnAnimal();
        }
    }

    private void SpawnAnimal()
    {
        var o = Instantiate(AnimalPrefab);
        var t = o.transform;
        t.SetParent(AnimalContainer, false);
        t.position = Vector3.zero;
        var a = o.GetComponent<Animal.Subject>();
        a.WaterTank = WaterTank;
        a.DiedEvent += s =>
        {
            GameOver.Reason = s;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        };
    }
}