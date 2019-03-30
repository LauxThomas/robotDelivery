using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

	[SerializeField] private Switch Button1;
	[SerializeField] private Switch Button2;
	[SerializeField] private Switch Button3;
	[SerializeField] private float heigth;

	private Boolean open = false;
	private Vector3 destination = Vector3.up;

	public void Unlock()
	{
		if (Button1.isActivated() & Button2.isActivated() & Button3.isActivated() & !open)
		{
			destination = transform.position + Vector3.up * heigth;
			open = true;

		}

	}

	private void Update()
	{
		if (open)
		{
			transform.position = Vector3.MoveTowards(transform.position, destination, 2);
		}
	}
}
