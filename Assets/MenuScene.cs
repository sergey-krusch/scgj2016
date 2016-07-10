using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScene : MonoBehaviour {

	void Start () {
		AudioPlayer.MenuMusic ();
	}

	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Debug.Log ("Working");
			SceneManager.LoadScene ("Gameplay");
		}
	}
}
