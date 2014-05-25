using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public GameObject bullet;
	public float delay;
	private float timer;
	private bool canShoot;
	//public GameObject visManager;

	// Use this for initialization
	void Start () {
	
	}

	public static float Remap (float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	// Update is called once per frame
	void Update () {
		this.timer += Time.deltaTime;
		this.delay = Remap(this.GetComponent<VisGameObjectPropertyModifier>().brutValue, 0f, 1f, 0.5f, 0f);

		if(this.timer >= this.delay)
		{
			this.timer -= this.delay;
			Instantiate(this.bullet, this.transform.position, this.transform.rotation);
		}
	}
}