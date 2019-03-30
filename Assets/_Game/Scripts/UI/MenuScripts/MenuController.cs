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
	public GameObject PauseTutorialMenu;
	public GameObject highScoreMenu;
	public GameObject endScreen;
	public GameObject highScoreTable;

	[SerializeField] private RuntimeScore runtimeScore;


	public List<GameObject> menus;

	public GameObject HUD;
	private int level;


	public void Start()
	{
		menus = new List<GameObject>();
		menus.Add(MainMenu);
		menus.Add(PauseMenu);
		menus.Add(OptionsMenu);
		menus.Add(CreditsMenu);
		menus.Add(TutorialMenu);
		menus.Add(PauseTutorialMenu);
		menus.Add(highScoreMenu);
		menus.Add(endScreen);
	}

	private void Update()
	{
		if (Input.GetButton("Pause"))
		{
			Debug.Log("PAUSE PREsSED");
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
		//GameManager.resetGame();
		SceneManager.LoadScene("MainMenu");
		//enable(MainMenu);
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
		//GameManager.startLevel(1);
		runtimeScore.level = 0;
		//SceneManager.LoadScene("Packageloader");
		SceneManager.LoadScene("insertName");
	}

	public void quitGame()
	{
		Debug.Log("Quit");
		Application.Quit();
	}

	public void restartLevel()
	{
		SceneManager.LoadScene("Packageloader");
	}

	public void startNextLevel()
	{
		runtimeScore.level += 1;
		SceneManager.LoadScene("Packageloader");
	}

	public void showCredits()
	{
		enable(CreditsMenu);
	}

	public void showTutorial()
	{
		enable(TutorialMenu);
	}

	public void showPauseTutorial()
	{
		enable(PauseTutorialMenu);
	}

	public void showHighscore()
	{
		enable(highScoreMenu);
	}

	public void showEndScreen(int finishedLevel)
	{
		enable(endScreen);
		highScoreTable.GetComponent<HighscoreTable>().setLevel(finishedLevel);
		level = finishedLevel;
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
		HUD.GetComponent<HUDScript>().toggleHUD(false);
		Time.timeScale = 0; //oder GameManger.PauseGame();
		//Disable scripts that still work while timescale is set to 0
	}

	public void continueGame()
	{
		HUD.GetComponent<HUDScript>().toggleHUD(true);
		FindObjectOfType<TutorialController>().disableAll();
		Time.timeScale = 1; //oder GameManger.ResumeGame();
		//enable the scripts again
	}

	public void resumeGame()
	{

		enable(null);
		HUD.GetComponent<HUDScript>().toggleHUD(true);
		Time.timeScale = 1; //oder GameManger.ResumeGame();
	}
}
