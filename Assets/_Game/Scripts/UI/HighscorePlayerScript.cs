using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscorePlayerScript : MonoBehaviour
{

	public Transform playerNameText;
	public Transform playerScoreText;
	private void Start()
	{
	}

	public void setText(String playerName)
	{
		playerNameText.GetComponent<TextMeshProUGUI>().text = playerName;
	}

	public void setScore(String playerName)
	{
		playerNameText.GetComponent<TextMeshProUGUI>().text = playerName;
	}
}
