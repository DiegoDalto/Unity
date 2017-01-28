using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

public enum soundFx
{
	JUMP,
	OPEN,
	DEATH,
	MUSICATHEMA,
	COIN
}

public class soundController : MonoBehaviour
{
	public AudioClip jump;
	public AudioClip openChest;
	public AudioClip death;
	public AudioClip musicaThema;
	public AudioClip coin;

	private AudioSource audio;

	public static soundController instance;

	// Use this for initialization
	void Start ()
	{
		audio = GetComponent<AudioSource> ();
		instance = this;
	}

	public static void playSound (soundFx currentSound)
	{
		switch (currentSound) {
		case soundFx.DEATH:
			instance.audio.PlayOneShot (instance.death);
			break;
		case soundFx.JUMP:
			instance.audio.PlayOneShot (instance.jump);
			instance.audio.volume = 0.2f;
			break;
		case soundFx.MUSICATHEMA:
			instance.audio.PlayOneShot (instance.musicaThema);
			instance.audio.volume = 1f;
			break;
		case soundFx.OPEN:
			instance.audio.PlayOneShot (instance.openChest);
			instance.audio.volume = 0.5f;
			break;
		case soundFx.COIN:
			instance.audio.PlayOneShot (instance.coin);
			instance.audio.volume = 0.4f;
			break;
		}
	}
}
