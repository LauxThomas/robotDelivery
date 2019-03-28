using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Scriptable Objects", menuName="Scores/ScoreBoard", order = 2)]
public class ScoreBoard : ScriptableObject
{
	public int[,] scoreList = new int[5, 5]
	{
		{0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0},
	};

	public String[,] playerNames = new String[5, 5]
	{
		{ " ", " ", " ", " ", " "},
		{ " ", " ", " ", " ", " "},
		{ " ", " ", " ", " ", " "},
		{ " ", " ", " ", " ", " "},
		{ " ", " ", " ", " ", " "},
	};



	public int[,] GetScoreBoard()
	{
		return scoreList;
	}

	public String[,] GetPlayerNames()
	{
		return playerNames;
	}

	public void CheckAndInsertScore(int level, int score, String playerName)
	{
		int[,] tmpNumber = scoreList;
		String[,] tmpName = playerNames;

		for (int i = 0; i < 5; i++)
		{
			if (scoreList[level, i] < score)
			{
				for (int j = i+1; j < 5; j++)
				{
					tmpNumber[level, j] = scoreList[level, i];
					tmpName[level, j] = playerNames[level, i];
				}

				scoreList[level, i] = score;
				playerNames[level, i] = playerName;

			}
		}

	}

	public void WipeScoreBoard()
	{
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				scoreList[i, j] = 0;
				playerNames[i, j] = " ";
			}

		}
	}

	public int GetSingleScore(int level, int placement)
	{
		return scoreList[level, placement];
	}

	public String getSinglePlayer(int level, int placement)
	{
		return playerNames[level, placement];
	}
}
