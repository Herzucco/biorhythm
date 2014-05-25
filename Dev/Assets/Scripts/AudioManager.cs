using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
	public AudioSource source;
	public List<AudioClip> clips;

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if(clips.Count > 0 && (!source.clip || !source.isPlaying)){
			source.clip = PickRandom();
			source.Play();
		}
	}

	public AudioClip PickRandom(){
		int index = Random.Range(0, clips.Count-1);
		return clips[index];
	}
}

