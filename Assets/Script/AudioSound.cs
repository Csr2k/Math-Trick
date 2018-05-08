using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSound : MonoBehaviour {

	public static AudioClip ShootSound, DamagedSound, WrongSound;
	static AudioSource audioSrc;

	void Start () {
		ShootSound = Resources.Load<AudioClip> ("Shoot");
		DamagedSound = Resources.Load<AudioClip> ("Damaged");
		WrongSound = Resources.Load<AudioClip> ("Wrong");
		audioSrc = GetComponent<AudioSource> ();
	}

	public static void PlaySound(string clip){
		switch (clip) {
		case "Shoot":
			audioSrc.PlayOneShot (ShootSound);
			break;
		case "Damaged":
			audioSrc.PlayOneShot (DamagedSound);
			break;
		case "Wrong":
			audioSrc.PlayOneShot (WrongSound);
			break;
		}
	}
}
