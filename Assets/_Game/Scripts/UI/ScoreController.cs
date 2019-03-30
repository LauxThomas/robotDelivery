using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

	public RuntimeScore runtimeScore;
	public Transform timeText;
	public float timeFloat;



	// Start is called before the first frame update
	void Start()
	{
		updateScoreText();

	}

	// Update is called once per frame
	void Update()
	{
		timeFloat += Time.deltaTime;
		timeText.GetComponent<TextMeshProUGUI>().text = "Time: " + timeFloat.ToString("F2");
		updateScoreText();
	}

	private void updateScoreText()
	{
		gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + runtimeScore.score;
	}
//	public void savePlayerScore(String playerName)
//	{
//		//Ein Spieler kann nur einen Wert haben. Wenn sich der Spieler verbessert, wird sein Highscore geupdated.
//		if (PlayerPrefs.GetFloat(playerName,0)>score)
//		{
//		PlayerPrefs.SetFloat(playerName,score);
//		}
//	}


}
