using UnityEngine;
using System.Collections;

public class FlareMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public float Remap (float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	// Update is called once per frame
	void Update () {
		this.GetComponent<Light>().intensity = Remap(this.GetComponent<VisGameObjectPropertyModifier>().brutValue, 0f, 1f, 0f, 8f);
	}
}
