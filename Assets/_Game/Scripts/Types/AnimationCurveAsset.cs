using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AC_", menuName = "Types/AnimationCurve")]
public class AnimationCurveAsset : ScriptableObject
{
	public AnimationCurve Curve;

	public float Evaluate(float InTime)
	{
		return Curve.Evaluate(InTime);
	}
}
