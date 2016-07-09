using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver: MonoBehaviour
{
    public static string Reason;

    public float Pause;
    public Text ScoreLabel;
    public Text TapAnywhereLabel;
    private float remainingTime;

    public void Awake()
    {
        remainingTime = Pause;
        ScoreLabel.text = string.Format(ScoreLabel.text, Session.Score);
    }

    public void Update()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime < 0)
        {
            TapAnywhereLabel.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0))
                SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
        }
    }
}