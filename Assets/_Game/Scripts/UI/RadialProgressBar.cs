using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour
{

	public Transform LoadingBar;
	public Transform TextPercent;
	[SerializeField] private float currentAmount;

	private void Start()
	{
		TextPercent.GetComponent<TextMeshProUGUI>().text = ((int) currentAmount).ToString() + "%";
	}


	// Update is called once per frame
    void Update()
    {
	    if (Input.GetKeyDown(KeyCode.I))
	    {
		    addStamina(7);
		    TextPercent.GetComponent<TextMeshProUGUI>().text = ((int) currentAmount).ToString() + "%";
	    }
	    if (Input.GetKeyDown(KeyCode.J))
	    {
		    removeStamina(5);
		    TextPercent.GetComponent<TextMeshProUGUI>().text = ((int) currentAmount).ToString() + "%";
	    }

	    LoadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
    }

	public void addStamina(int val)
	{
		currentAmount += val;
		if (currentAmount>100)
		{
			currentAmount = 100;
		}
	}

	public void removeStamina(int val)
	{
		currentAmount -= val;
		if (currentAmount<0)
		{
			currentAmount = 0;
		}
	}

	public float getStamina()
	{
		return currentAmount;
	}
}
