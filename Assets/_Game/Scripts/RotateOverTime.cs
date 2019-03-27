using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
	public Vector3 RotationPerSecond;

	// Update is called once per frame
	void Update()
	{
		transform.localRotation = transform.localRotation * Quaternion.Euler(RotationPerSecond * Time.deltaTime);
	}
}
