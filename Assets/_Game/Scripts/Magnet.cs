using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Magnet : MonoBehaviour
{
	[SerializeField] private GameObject waypoint1;
	[SerializeField] private GameObject waypoint2;
	[SerializeField] private float distance;
	[SerializeField] private float speed;

	private Boolean direction;


	private void Update()
	{
		if (direction)
		{
			transform.position = (Vector3.MoveTowards(transform.position,waypoint1.transform.position, speed));
			if (Vector3.Distance(transform.position, waypoint1.transform.position) < distance)
			{
				SwitchDirection();
			}

		}
		else
		{
			transform.position =(Vector3.MoveTowards(transform.position,waypoint2.transform.position, speed));
			if (Vector3.Distance(transform.position, waypoint2.transform.position) < distance)
			{
				SwitchDirection();
			}
		}
	}

	public void SwitchDirection()
	{
		direction = !direction;
	}
}
