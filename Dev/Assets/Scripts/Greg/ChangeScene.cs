using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public string sceneName;
	private string[] gamepadList;
	public string padLabel;
	public string pcLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Back"))
	    {
			Application.LoadLevel(sceneName);
		}
		gamepadList = Input.GetJoystickNames();
		if (gamepadList.Length > 0)
		{
			this.GetComponentInChildren<UILabel>().text = padLabel;
		}
		if (gamepadList.Length == 0)
		{
			this.GetComponentInChildren<UILabel>().text = pcLabel;
		}
	}

	void OnClick() 
	{
		Application.LoadLevel(sceneName);
	}
}
