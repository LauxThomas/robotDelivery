using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Switch : MonoBehaviour
{

	[SerializeField] private Door door;
	private Boolean activated = false;

	private void Awake()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		activated = true;
		door.Unlock();
	}

	public Boolean isActivated()
	{
		return activated;
	}
}
