using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public GameObject bullet;
	public float delay;
	private float timer;
	//public GameObject visManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.timer += Time.deltaTime;
		this.delay = this.GetComponent<VisGameObjectPropertyModifier>().returnedValue;
		if (Input.GetKey("space"))
		{
			if(this.timer >= this.delay)
			{
				this.timer -= this.delay;
				Instantiate(this.bullet, this.transform.position, this.transform.rotation);
			}
		}
	}
}
