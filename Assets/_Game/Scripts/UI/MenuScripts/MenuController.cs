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
	public GameObject TutorialMenu;

	public List<GameObject> menus;

	public void Start()
	{
		menus = new List<GameObject>();
		menus.Add(MainMenu);
		menus.Add(PauseMenu);
		menus.Add(OptionsMenu);
		menus.Add(CreditsMenu);
		menus.Add(TutorialMenu);
	}

	private void Update()
	{
		if (Input.GetButton("Pause"))
		{
			foreach (GameObject menu in menus)
			{
				if (menu.activeInHierarchy)
				{
					return;
				}
			}
			showPauseMenu();
		}
	}

	public void showMainMenu()
	{
		//TODO:
		//GameManager.resetGame();
		enable(MainMenu);
	}

	public void showPauseMenu()
	{
		enable(PauseMenu);
		pauseGame();
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
		//TODO:
		//GameManager.startGame(); //am besten mit Time.timeScale = 1;
	}

	public void showCredits()
	{
		enable(CreditsMenu);
	}

	public void showTutorial()
	{
		enable(TutorialMenu);
	}

	public void back2Game()
	{
		enable(null);
		continueGame();
	}

	public void enable(GameObject enabledObject)
	{
		foreach (GameObject go in menus)
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

	public void pauseGame()
	{
		Debug.Log("GAME PAUSED");
		Time.timeScale = 0; //oder GameManger.PauseGame();
		//Disable scripts that still work while timescale is set to 0
	}

	public void continueGame()
	{

		Debug.Log("GAME CONTINUED");
		Time.timeScale = 1; //oder GameManger.ResumeGame();
		//enable the scripts again
	}
}
