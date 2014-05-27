using UnityEngine;
using System.Collections;

public class OpenWeb : MonoBehaviour {

	public string urlAdress;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick() 
	{
		Application.OpenURL(urlAdress);
	}
}
