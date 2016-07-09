using System.Collections.Generic;
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
    private int animalsAlive;
    private List<Animal.Subject> animals;

    public void Awake()
    {
        started = false;
        animalsAlive = 0;
        animals = new List<Animal.Subject>();
        WaterTank.WaterLevel = Root.Instance.InitialWaterLevel;
        Digger.TappedEvent += Begin;
    }

    public void Start()
    {
        AudioPlayer.Music();
    }

    public void Update()
    {
        HandleSpawn();
        HandleTime();
        TimeLabel.text = FormatTime();
    }

    public void ReplayClick()
    {
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }

    private void HandleTime()
    {
        if (!started)
            return;
        time -= Time.deltaTime;
        if (time < 0)
            GameOver();
    }

    private string FormatTime()
    {
        int t = Mathf.FloorToInt(time);
        if (t < 0)
            t = 0;
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
        t.localPosition = Vector3.zero;
        var a = o.GetComponent<Animal.Subject>();
        a.WaterTank = WaterTank;
        a.DiedEvent += () =>
        {
            if (--animalsAlive < 1)
                spawnRemainingTime = 0.0f;
        };
        animals.Add(a);
        ++animalsAlive;
    }

    private void GameOver()
    {
        var scfg = Root.Instance.Score;
        var acfg = Root.Instance.Animal;
        int s = 0;
        foreach (var a in animals)
        {
            var c = a.Value;
            if (c < 0)
                s += scfg.DeadScore;
            else if (c < acfg.EmptyNormalBound)
                s += scfg.EmptyScore;
            else if (c < acfg.NormalFullBound)
                s += scfg.NormalScore;
            else
                s += scfg.FullScore;
        }
        Session.Score = s;
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}