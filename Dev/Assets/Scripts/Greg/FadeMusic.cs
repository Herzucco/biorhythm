using UnityEngine;
using System.Collections;

public class FadeMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<UIPanel>().alpha = this.GetComponent<VisGameObjectPropertyModifier>().returnedValue;
	}
}
