using UnityEngine;
using System.Collections;

public class VideoScript : MonoBehaviour {

	public MovieTexture videoTexture;

	// Use this for initialization
	void Start () {
		this.renderer.material.mainTexture = videoTexture;
		videoTexture.loop = true;
		videoTexture.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
