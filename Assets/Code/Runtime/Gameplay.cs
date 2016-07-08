using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay: MonoBehaviour
{
    public float InitialWaterLevel;
    public WaterTank WaterTank;
    public Animal.Subject Animal;

    public void Awake()
    {
        WaterTank.WaterLevel = InitialWaterLevel;
        Animal.DiedEvent += s =>
        {
            GameOver.Reason = s;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        };
    }

    public void ReplayClick()
    {
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
}