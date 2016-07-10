using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver: MonoBehaviour
{
    public static string Reason;

    public float Pause;
    public Text ScoreLabel;
    public Text TapAnywhereLabel;
    public Color PositiveScoreColor;
    public Color NegativeScoreColor;
    private float remainingTime;

    public void Awake()
    {
        remainingTime = Pause;
        Color32 c;
        if (Session.Score >= 0)
            c = PositiveScoreColor;
        else
            c = NegativeScoreColor;
        var cs = string.Format("#{0:X2}{1:X2}{2:X2}", c.r, c.g, c.b);
        ScoreLabel.text = string.Format(
            ScoreLabel.text, 
            cs,
            Session.Score);
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