using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseScoreBoard : MonoBehaviour
{
	public ScoreBoard scoreBoardObject;

	public int[] GetScoreBoard()
	{
		return scoreBoardObject.scoreList;
	}

	public String[] GetPlayerNames()
	{
		return scoreBoardObject.playerNames;
	}

	public void CheckAndInsertScore(int score, String playerName)
	{
		int[] tmpNumber = scoreBoardObject.scoreList;
		String[] tmpName = scoreBoardObject.playerNames;

		for (int i = 0; i < 5; i++)
		{
			if (scoreBoardObject.scoreList[i] < score)
			{
					for (int j = i+1; j < 5; j++)
					{
						tmpNumber[j] = scoreBoardObject.scoreList[i];
						tmpName[j] = scoreBoardObject.playerNames[i];
					}

					scoreBoardObject.scoreList[i] = score;
					scoreBoardObject.playerNames[i] = playerName;

			}
		}

	}

	public void WipeScoreBoard()
	{
		for (int i = 0; i < 5; i++)
		{
			scoreBoardObject.scoreList[i] = 0;
			scoreBoardObject.playerNames[i] = " ";
		}
	}

	public int GetSingleScore(int placement)
	{
		return scoreBoardObject.scoreList[placement];
	}

	public String getSinglePlayer(int placement)
	{
		return scoreBoardObject.playerNames[placement];
	}
}
