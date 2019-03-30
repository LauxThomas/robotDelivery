using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "A_#_Cue", menuName = "Types/Audio Cue")]
public class AudioCueBaseAsset : ScriptableObject
{
	public AudioClip SoundEffect;
	public Vector2 Volume = new Vector2(1,1);

	public float Speed = 1F;

	public bool Loops = false;

	public void PlayOneShot(AudioSource source)
	{
		source.pitch = Speed;
		source.PlayOneShot(SoundEffect, Random.Range(Volume.x, Volume.y));
	}

	public void PlayLoop(AudioSource source)
	{
		source.loop = Loops;
		source.clip = SoundEffect;
		source.volume = (Volume.x + Volume.y) / 2.0F;
		source.Play();
	}

}
