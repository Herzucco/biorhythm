using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (Vector3.right * speed * Time.deltaTime, Space.Self);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.transform.position = new Vector3 (-15,0,0);
		}
	}
}