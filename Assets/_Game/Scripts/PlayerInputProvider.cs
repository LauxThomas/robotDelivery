using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputProvider : MonoBehaviour, IInputProvider
{
	public Vector2 Direction()
	{
		return new Vector2(Input.GetAxis("Horizontal"), 0);
	}
}
