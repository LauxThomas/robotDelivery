using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HDRColor_", menuName = "Types/HDR Color Blend")]
public class HDRColorBlendAsset : ScriptableObject
{
	public AnimationCurveAsset BlendCurve;
	[ColorUsage(true, true)]
	public Color A;
	[ColorUsage(true, true)]
	public Color B;

	public Color Lerp(float InTime)
	{
		Color resultingColor = Color.Lerp(A, B, BlendCurve.Evaluate(InTime));
		return resultingColor;
	}
}
