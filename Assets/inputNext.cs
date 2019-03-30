using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inputNext : MonoBehaviour
{

	public GameObject inputField;
	public RuntimeScore rts;


	public void loadNextLevel()
	{
		rts.player = inputField.GetComponent<TextMeshProUGUI>().text;
		SceneManager.LoadScene("Levelselection");
	}

	public void showMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
