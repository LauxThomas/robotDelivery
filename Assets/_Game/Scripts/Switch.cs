using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Switch : MonoBehaviour
{

	[SerializeField] private Door door;
	[SerializeField] private Magnet magnet;
	private Boolean activated;
	private Collider col;

	private void Awake()
	{
		col = GetComponent<Collider>();
	}

	private void OnTriggerEnter(Collider other)
	{
		activated = true;
		if (door != null)
		{
			door.Unlock();
		}

		if (magnet != null)
		{
			magnet.switchActivation();
			col.enabled = false;
		}

	}

	public Boolean isActivated()
	{
		return activated;
	}
}
