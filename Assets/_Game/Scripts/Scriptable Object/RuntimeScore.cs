using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Data", menuName="Scores/RuntimeScore", order = 1)]
public class RuntimeScore : ScriptableObject
{
	public String player;
	public int score;

}
