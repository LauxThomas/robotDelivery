using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour
{
	[SerializeField] private RuntimeScore rts;

	public GameObject level1;
	public GameObject level2;
	public GameObject level3;
	public GameObject level4;

	public List<GameObject> levels;
	public int ptr;

	private void Start()
	{
		levels = new List<GameObject>();
		levels.Add(level1);
		levels.Add(level2);
		levels.Add(level3);
		levels.Add(level4);
		showLevel(ptr);
	}

	public void showMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	private void showLevel(int lvl)
	{
		ptr = lvl;
		Debug.Log(ptr);
		for (int i = 0; i < levels.Count; i++)
		{
			if (i == ptr)
			{
				levels[i].SetActive(true);
			}
			else
			{
				levels[i].SetActive(false);
			}
		}
	}

	public void startLevel()
	{
		rts.setLevel(ptr);
		SceneManager.LoadScene("Packageloader");
	}

	public void showNextLevel()
	{
		ptr++;
		ptr %= levels.Count;
		showLevel(ptr);
	}

	public void showPreviousLevel()
	{
		ptr--;
		if (ptr < 0)
		{
			ptr = levels.Count - 1;
		}
		else
		{
			ptr %= levels.Count;
		}

		showLevel(ptr);
	}
}
