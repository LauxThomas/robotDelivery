using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Changes color of the springs emission based on supplied tension.
 */
public class JumpSpringFX : MonoBehaviour
{
	public Color BaseColor;
	public Color TensionColor;

	public AnimationCurve TensionCurve;

	private Material emissionMaterial;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
