using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HDRColor_", menuName = "Types/HDR Color Blend")]
public class HDRColorBlendAsset : ScriptableObject
{
	public AnimationCurveAsset BlendCurve;
	[ColorUsage(true, true)]
	public Color A = Color.white;
	[ColorUsage(true, true)]
	public Color B = Color.white;

	public Color Lerp(float InTime)
	{
		Color resultingColor = Color.Lerp(A, B, BlendCurve.Evaluate(InTime));
		return resultingColor;
	}
}
