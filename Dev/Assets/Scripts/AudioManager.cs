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
		GameObject otherManager = GameObject.FindGameObjectWithTag("AudioManager");
		if(otherManager && otherManager != gameObject){
			clips = otherManager.GetComponent<AudioManager>().clips;
			Destroy(otherManager);
		}
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
		source.clip = PickRandom();
		source.Play();
	}
}

