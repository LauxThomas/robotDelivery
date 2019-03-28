using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickUp : MonoBehaviour
{

	[SerializeField] private int score;
	[SerializeField] private RuntimeScore scoreController;
	private Collider col;
	private MeshRenderer mr;

	private void Awake()
	{
		col  = GetComponent<Collider>();
		mr = GetComponent<MeshRenderer>();
	}

	private void OnTriggerEnter(Collider other)
	{
		scoreController.AddScore(score);
		col.enabled = false;
		mr.enabled = false;



	}

}
