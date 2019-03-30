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


	public void BeginJumpCharge()
	{
		JumpCharge.PlayOneShot(EffectsSource);
	}

	public void ReleaseJump()
	{
		JumpLaunch.PlayOneShot(EffectsSource);
	}

	public void LosePackage()
	{
		PackageLoss.PlayOneShot(EffectsSource);
	}

	public void SetThruster(bool InEnabled)
	{
		if (InEnabled)
		{
			Thrusters.PlayLoop(ThrusterSource);
		}
		else
		{
			ThrusterSource.Stop();
		}
	}
}
