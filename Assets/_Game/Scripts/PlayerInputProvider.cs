using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputProvider : MonoBehaviour, IInputProvider
{
	public Vector3 Direction()
	{
		return new Vector3(Input.GetAxis("Horizontal"), 0);
	}

	public float ForceFromJet()
	{
		return Input.GetAxis("Jet");
	}

	public float JumpPower()
	{
		return 0f;
	}
}
