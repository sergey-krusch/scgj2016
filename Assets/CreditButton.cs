using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditButton : MonoBehaviour {

	public void OnClick() 
	{
		SceneManager.LoadScene ("Credits");
	}
}
