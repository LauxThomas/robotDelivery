using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreTable : MonoBehaviour
{
	[SerializeField] private RuntimeScore rts;
	[SerializeField] private ScoreBoard sb;

	[SerializeField] private GameObject c1;
	[SerializeField] private GameObject c2;
	[SerializeField] private GameObject c3;
	[SerializeField] private GameObject c4;
	[SerializeField] private GameObject c5;
	private int level;


	private void Awake()
	{
		GameObject[] highscoreTemplates = {c1, c2, c3, c4, c5};
		string[,] playerNames = sb.GetPlayerNames();
		int[,] playerScores = sb.GetScoreBoard();

		sb.CheckAndInsertScore(rts.level,rts.score,rts.player);



		for (int i = 0; i < 5; i++)
		{

			highscoreTemplates[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerNames[level, i];
			highscoreTemplates[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = playerScores[level, i].ToString();


		}
	}

	public void setLevel(int i)
	{
		level = i;
		Awake();
	}


}
