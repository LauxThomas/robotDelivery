using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "Scores/ScoreBoard", order = 2)]
public class ScoreBoard : ScriptableObject
{
	public int[,] scoreList = new int[5, 5]
	{
		{1337, 1336, 1335, 1338, 12356},
		{345, 274, 853, 13, 12},
		{124, 235, 364, 3574, 123},
		{0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0},
	};

	public String[,] playerNames = new String[5, 5]
	{
		{" Hanseins", " hanszwei", "dreierhans ", "vierervolker ", "fünferfrank "},
		{" erwineins", " zachariaszwei", "dieterdrei ", " volfgangvier", " frederik der 5."},
		{" dritteslevel", " asdg", " rdgjtz", "zukjhdf ", " sfgjdgh"},
		{" ", " ", " ", " ", " "},
		{" ", " ", " ", " ", " "},
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
		bool isInsert = false;
		Debug.Log("CheckAndInsert" + level + score + playerName);
		int saveNumber;
		int writeNumber = 0;
		String savePlayer;
		String writePlayer = "";
		for (int i = 0; i < 5; i++)
		{
			if (isInsert)
			{
				saveNumber = scoreList[level, i];
				savePlayer = playerNames[level, i];
				Debug.Log("savenumber1: "+saveNumber);
				scoreList[level, i] = writeNumber;
				playerNames[level, i] = writePlayer;
				Debug.Log("savenumber2: "+saveNumber);

				writeNumber = saveNumber;
				writePlayer = savePlayer;
			}

			if (scoreList[level, i] < score && !isInsert)
			{
				Debug.Log("erste if");
				saveNumber = scoreList[level, i];
				savePlayer = playerNames[level, i];
				scoreList[level, i] = score;
				playerNames[level, i] = playerName;

				writeNumber = saveNumber;
				writePlayer = savePlayer;
				isInsert = true;
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
