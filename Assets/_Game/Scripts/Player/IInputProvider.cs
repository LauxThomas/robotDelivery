using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInputProvider
{
	Vector3 Direction();

	float ForceFromJet();

	float JumpPower();

	float ResetButton();
}
