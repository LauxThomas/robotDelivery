using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOnGround : MonoBehaviour
{
	[SerializeField] private GameObject playerActorPrefab;

	private PlayerActor_Script _playerActorScript;

	private void Awake()
	{
		_playerActorScript = playerActorPrefab.GetComponent<PlayerActor_Script>();
	}

	private void OnCollisionEnter(Collision other)
	{
		_playerActorScript.isGrounded = true;
	}

	private void OnCollisionExit(Collision other)
	{
		_playerActorScript.isGrounded = false;
	}
}
