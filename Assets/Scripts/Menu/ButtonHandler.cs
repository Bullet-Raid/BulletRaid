using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

	public void NewGameBtn()
	{
		SceneManager.LoadScene("Game");
	}
	public void QuitGameBtn()
	{
		Application.Quit();
	}
}
