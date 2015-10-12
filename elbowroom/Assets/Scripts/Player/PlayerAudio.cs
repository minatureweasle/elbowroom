using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour {

	AudioSource myAudio;

	public AudioClip jumpClip;
	public AudioClip boostClip;

	void Start(){
		myAudio = GetComponent<AudioSource>();
	}

	public void PlayJumpClip(){
		myAudio.PlayOneShot(jumpClip);
	}

	public void PlayBoostClip(){
		myAudio.PlayOneShot(boostClip);
	}
}
