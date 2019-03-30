using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
	//public Image tut1;
	//public Image tut2;
	//public Image tut3;
	public GameObject t1;
	public GameObject t2;
	public GameObject t3;

	//private List<Image> tuts;
	private List<GameObject> t;
	private int ptr;

	private void Start()
	{
		//tuts = new List<Image>();
		//tuts.Add(tut1);
		//tuts.Add(tut2);
		//tuts.Add(tut3);
		t = new List<GameObject>();
		t.Add(t1);
		t.Add(t2);
		t.Add(t3);
		showTut(ptr);

	}

	public void showNextTutorial()
	{
		ptr++;
		ptr %= t.Count;
		showTut(ptr);
	}

	public void showPreviousTutorial()
	{
		ptr--;
		if (ptr < 0)
		{
			ptr = t.Count-1;
		}
		else
		{
			ptr %= t.Count;
		}

		showTut(ptr);
	}

	public void showTut(int ptr)
	{
		Debug.Log(ptr);
		for (int i = 0; i < t.Count; i++)
		{
			if (i == ptr)
			{
				t[i].SetActive(true);
			}
			else
			{
				t[i].SetActive(false);
			}
		}
	}

	public void disableAll()
	{
		gameObject.SetActive(false);
	}
}
