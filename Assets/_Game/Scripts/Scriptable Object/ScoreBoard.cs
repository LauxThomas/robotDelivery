using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Data", menuName="Scores/ScoreBoard", order = 2)]
public class ScoreBoard : ScriptableObject
{
	public int[] scoreList = {0,0,0,0,0};
	public String[] playerNames = { " ", " ", " ", " ", " "};


}
