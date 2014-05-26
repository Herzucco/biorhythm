using UnityEngine;
using System.Collections;

public class TrackName : MonoBehaviour {

	private string audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().source.clip.name;
		this.GetComponent<GUIText>().text = audioSource;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
