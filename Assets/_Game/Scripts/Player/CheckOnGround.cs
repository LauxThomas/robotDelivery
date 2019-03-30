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

	/*
	private void OnCollisionEnter(Collision other)
	{
		Debug.Log(other);
		_playerActorScript.isGrounded = true;
	}

	private void OnCollisionExit(Collision other)
	{
		_playerActorScript.isGrounded = false;
	}
	*/

	private void FixedUpdate()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.4f))
		{
			_playerActorScript.isGrounded = true;
		}
		else
		{
			_playerActorScript.isGrounded = false;
		}
	}
}
