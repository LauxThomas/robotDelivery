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

	public void Unlock()
	{
		if (Button1.isActivated() & Button2.isActivated() & Button3.isActivated() & !open)
		{
			open = true;
			transform.position += Vector3.up * heigth * Time.fixedDeltaTime;
		}

	}

}
