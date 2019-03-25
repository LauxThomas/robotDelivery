using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
	public GameObject MainMenu;
	public GameObject PauseMenu;
	public GameObject OptionsMenu;
	public GameObject CreditsMenu;

	public List<GameObject> gos;
	public void Start()
	{
		gos = new List<GameObject>();
		gos.Add(MainMenu);
		gos.Add(PauseMenu);
		gos.Add(OptionsMenu);
		gos.Add(CreditsMenu);
	}

	public void showMainMenu()
	{
		enable(MainMenu);
	}

	public void showPauseMenu()
	{
		enable(PauseMenu);
		//pauseGame();
	}

	public void showOptionsMenu()
	{
		enable(OptionsMenu);
	}

	public void startGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void quitGame()
	{
		Debug.Log("Quit");
		Application.Quit();
	}

	public void restartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void showCredits()
	{
		//enable(Credits);
	}

	public void showTutorial()
	{
		//enable(Tutorial);
	}

	public void back2Game()
	{
		enable(null);
		//resumeGame();
	}

	public void enable(GameObject enabledObject)
	{
		foreach (GameObject go in gos)
		{
			if (go != enabledObject)
			{
				go.SetActive(false);
			}
			else
			{
				go.SetActive(true);
			}
		}
	}

}
