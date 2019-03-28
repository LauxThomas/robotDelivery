using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Changes color of the springs emission based on supplied tension.
 */
public class JumpSpringFX : MonoBehaviour
{
	public HDRColorBlendAsset ColorBlendAsset;
	public string PropertyName = "_EmissionColor";

	private int PropertyId;
	private Material emissionMaterial;

	// Start is called before the first frame update
	void Awake()
	{
		emissionMaterial = GetComponent<Renderer>().material;
		PropertyId = Shader.PropertyToID(PropertyName);
	}

	 
	/// <summary>
	/// Determinates color of supplied tension argument.
	/// </summary>
	/// <param name="InTension">Tension parameter in range of [0,1]</param>
	/// <returns>The resulting color from tension</returns>
	public Color ApplyTension(float InTension)
	{
		InTension = Mathf.Clamp01(InTension);
		Color resultingColor = ColorBlendAsset.Lerp(InTension);
		emissionMaterial.SetColor(PropertyId, resultingColor);

		return resultingColor;
	}

	float X = 0.0F;
	float Y = -1.0F;
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Y = 1F;
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			Y = -1F;
		}

		X = Mathf.Clamp01(X + Time.deltaTime * Y);
		ApplyTension(X);

	}
}
