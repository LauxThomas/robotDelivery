using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Scriptable Objects", menuName="Scores/RuntimeScore", order = 1)]
public class RuntimeScore : ScriptableObject
{
	public String player;
	public int score;


	public int GetScore()
	{
		return score;
	}

	public String GetPlayer()
	{
		return player;
	}

	public void AddScore(int _addScore)
	{
		score += _addScore;
		//Debug.Log(score);
	}

	public void SubScore(int _subScore)
	{
		if(score - _subScore >= 0)
		{
			score -= _subScore;
		}
		else
		{
			score = 0;
		}

	}
}
