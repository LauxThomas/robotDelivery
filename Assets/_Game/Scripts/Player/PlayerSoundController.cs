using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
	[Header("--- Effects ---")]
	[SerializeField]
	protected AudioSource EffectsSource;
	[SerializeField]
	protected AudioCueBaseAsset JumpCharge;
	[SerializeField]
	protected AudioCueBaseAsset JumpLaunch;
	[SerializeField]
	protected AudioCueBaseAsset PackageLoss;

	[Header("--- Toggles ---")]
	[SerializeField]
	protected AudioSource ThrusterSource;
	[SerializeField]
	protected AudioCueBaseAsset Thrusters; // Loops

	private bool JumpGate = false;

	public void BeginJumpCharge()
	{
		if(!JumpGate)
		{
			JumpGate = true;
			JumpCharge.PlayOneShot(EffectsSource);
		}
	}

	public void ReleaseJump()
	{
		if(JumpGate)
		{
			JumpLaunch.PlayOneShot(EffectsSource);
			JumpGate = false;
		}
	}

	public void LosePackage()
	{
		PackageLoss.PlayOneShot(EffectsSource);
	}

	public void SetThruster(bool InEnabled)
	{
		if (InEnabled && !ThrusterSource.isPlaying)
		{
			Thrusters.PlayLoop(ThrusterSource);
		}
		else if(!InEnabled && ThrusterSource.isPlaying)
		{
			ThrusterSource.Stop();
		}
	}
}
