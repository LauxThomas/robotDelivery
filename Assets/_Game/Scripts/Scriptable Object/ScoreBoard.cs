using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Scriptable Objects", menuName="Scores/ScoreBoard", order = 2)]
public class ScoreBoard : ScriptableObject
{
	public int[] scoreList = {0,0,0,0,0};
	public String[] playerNames = { " ", " ", " ", " ", " "};


	public int[] GetScoreBoard()
	{
		return scoreList;
	}

	public String[] GetPlayerNames()
	{
		return playerNames;
	}

	public void CheckAndInsertScore(int score, String playerName)
	{
		int[] tmpNumber = scoreList;
		String[] tmpName = playerNames;

		for (int i = 0; i < 5; i++)
		{
			if (scoreList[i] < score)
			{
				for (int j = i+1; j < 5; j++)
				{
					tmpNumber[j] = scoreList[i];
					tmpName[j] = playerNames[i];
				}

				scoreList[i] = score;
				playerNames[i] = playerName;

			}
		}

	}

	public void WipeScoreBoard()
	{
		for (int i = 0; i < 5; i++)
		{
			scoreList[i] = 0;
			playerNames[i] = " ";
		}
	}

	public int GetSingleScore(int placement)
	{
		return scoreList[placement];
	}

	public String getSinglePlayer(int placement)
	{
		return playerNames[placement];
	}
}
