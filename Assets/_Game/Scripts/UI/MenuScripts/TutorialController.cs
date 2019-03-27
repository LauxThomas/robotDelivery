using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
	public Image tut1;
	public Image tut2;
	public Image tut3;

	private List<Image> tuts;
	private int ptr;

	private void Start()
	{
		tuts = new List<Image>();
		tuts.Add(tut1);
		tuts.Add(tut2);
		tuts.Add(tut3);
		showTut(ptr);
	}

	public void showNextTutorial()
	{
		ptr++;
		ptr %= tuts.Count;
		showTut(ptr);
	}

	public void showPreviousTutorial()
	{
		ptr--;
		if (ptr < 0)
		{
			ptr = tuts.Count-1;
		}
		else
		{
			ptr %= tuts.Count;
		}

		showTut(ptr);
	}

	private void showTut(int ptr)
	{
		Debug.Log(ptr);
		for (int i = 0; i < tuts.Count; i++)
		{
			if (i == ptr)
			{
				tuts[i].enabled = true;
				tuts[i].gameObject.SetActive(true);
			}
			else
			{
				tuts[i].enabled = false;
				tuts[i].gameObject.SetActive(false);
			}
		}
	}
}
