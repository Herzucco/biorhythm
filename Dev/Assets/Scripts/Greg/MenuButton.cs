using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	public string levelName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick() {
		Application.LoadLevel(levelName);
	}
}
