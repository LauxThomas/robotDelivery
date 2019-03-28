using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseRuntimeScore : MonoBehaviour
{
	public RuntimeScore scoreObject;

	public int GetScore()
	{
		return scoreObject.score;
	}

	public String GetPlayer()
	{
		return scoreObject.player;
	}

	public void AddScore(int _addScore)
	{
		scoreObject.score += _addScore;
	}

	public void SubScore(int _subScore)
	{
		if(scoreObject.score - _subScore >= 0)
		{
		   scoreObject.score -= _subScore;
		}
		else
		{
			scoreObject.score = 0;
		}

	}



}
