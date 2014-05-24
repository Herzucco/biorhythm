using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour {
	public MagnetPattern parent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Chromosome"){
			parent.reachingOther(other.gameObject, this);
		}
	}
}
