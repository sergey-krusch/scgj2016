using Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay: MonoBehaviour
{
    public Text TimeLabel;
    public WaterTank WaterTank;
    public Transform AnimalContainer;
    public GameObject AnimalPrefab;
    public Digger.Subject Digger;

    private bool started;
    private float time;
    private float spawnRemainingTime;

    public void Awake()
    {
        started = false;
        WaterTank.WaterLevel = Root.Instance.InitialWaterLevel;
        Digger.TappedEvent += Begin;
    }

    public void Update()
    {
        HandleSpawn();
        time -= Time.deltaTime;
        TimeLabel.text = FormatTime();
    }

    public void ReplayClick()
    {
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }

    private string FormatTime()
    {
        int t = Mathf.FloorToInt(time);
        int m = t / 60;
        int s = t % 60;
        return string.Format("{0:00}:{1:00}", m, s);
    }

    private void Begin()
    {
        if (started)
            return;
        started = true;
        time = Root.Instance.LevelTime;
        TimeLabel.gameObject.SetActive(true);
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
    }
}