using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver: MonoBehaviour
{
    public static string Reason;

    public Text ReasonLabel;

    public void Awake()
    {
        ReasonLabel.text = Reason;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
}