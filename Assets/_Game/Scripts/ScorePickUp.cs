using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ScorePickUp : MonoBehaviour
{

	[SerializeField] private int score;
	[SerializeField] private RuntimeScore scoreController;
	private Collider col;
	private MeshRenderer mr;

	public AudioCueBaseAsset CollectedSFX;
	private new AudioSource audio;

	private void Awake()
	{
		audio = GetComponent<AudioSource>();
		col  = GetComponent<Collider>();
		mr = GetComponent<MeshRenderer>();
	}

	private void OnTriggerEnter(Collider other)
	{
		scoreController.AddScore(score);
		col.enabled = false;
		mr.enabled = false;

		CollectedSFX.PlayOneShot(audio);
	}

}
