using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schalter : MonoBehaviour
{
	[SerializeField] private Rigidbody _door;

	private Boolean open = false;



	private void OnTriggerEnter(Collider other)
	{

		if (!open)
		{
			_door.transform.Translate(Vector3.up*6);
			open = true;
		}

	}
}
