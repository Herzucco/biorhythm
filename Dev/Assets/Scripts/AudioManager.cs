using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
	public AudioSource source;
	public List<AudioClip> clips;
	public float id;

	void Awake() {
		id = Random.Range(0f, 10000000f);
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
		int index = Random.Range(0, clips.Count);
		return clips[index];
	}

	public void ChangeMusic(){
		source.Stop();
		source.clip = PickRandom();
		source.Play();
	}
}

