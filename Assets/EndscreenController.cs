using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndscreenController : MonoBehaviour
{
	[SerializeField] private RuntimeScore rts;
	public void showMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void tryAgain()
	{
		SceneManager.LoadScene("Packageloader");
	}

	public void nextLevel()
	{
		rts.setLevel(rts.getLevel()+1);
		SceneManager.LoadScene("Packageloader");
	}
}
