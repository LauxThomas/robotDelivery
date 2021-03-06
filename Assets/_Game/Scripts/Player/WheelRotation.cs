using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{

	public float TurnSpeed = 360F;
	public float InterpSpeed = 2F;

	private float TargetTurnRate = 0F;
	private float TurnRate = 0F;

	// Update is called once per frame
	void Update()
	{
		TurnRate = Mathf.Lerp(TurnRate, TargetTurnRate, Time.deltaTime * InterpSpeed);
		transform.localRotation = transform.localRotation * Quaternion.Euler(0F, TurnRate * TurnSpeed * Time.deltaTime, 0F);
	}

	public void SetTurnRate(float Rate)
	{
		if (Mathf.Sign(Rate) + Mathf.Sign(TargetTurnRate) == 0F)
		{
			TurnRate = Rate;
			TargetTurnRate = Rate;
		}
		else
		{
			TargetTurnRate = Rate;
		}
	}
}
