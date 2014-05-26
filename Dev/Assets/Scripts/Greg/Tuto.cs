using UnityEngine;
using System.Collections;

public class Tuto : MonoBehaviour {

	public GameObject[] pcControls;
	public GameObject[] padControls;
	private string[] gamepadList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gamepadList = Input.GetJoystickNames();
		if (gamepadList.Length > 0)
		{
			for(int i = 0; i < padControls.Length; i++)
			{
				pcControls[i].SetActive(false);
				padControls[i].SetActive(true);
			}
		}
		if (gamepadList.Length == 0)
		{
			for(int i = 0; i < pcControls.Length; i++)
			{
				pcControls[i].SetActive(true);
				padControls[i].SetActive(false);
			}
		}
	}
}
