using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
	public float score;
	public float slowingFactor=5;


	// Start is called before the first frame update
	void Start()
	{
		updateScoreText();
	}

	// Update is called once per frame
	void Update()
	{
		score += Time.deltaTime*1/slowingFactor;
		updateScoreText();
	}

	private void updateScoreText()
	{
		gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString("F2");
	}

	public void addScore(int val)
	{
		score += val;
		updateScoreText();
	}

	public void savePlayerScore(String playerName)
	{
		//Ein Spieler kann nur einen Wert haben. Wenn sich der Spieler verbessert, wird sein Highscore geupdated.
		if (PlayerPrefs.GetFloat(playerName,0)>score)
		{
		PlayerPrefs.SetFloat(playerName,score);
		}
	}


}
