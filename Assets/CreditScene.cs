using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditScene : MonoBehaviour {
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			SceneManager.LoadScene ("MainMenu");
		}
	}
}
