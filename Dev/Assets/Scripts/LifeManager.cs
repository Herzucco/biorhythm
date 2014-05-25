using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour
{
	public float life;
	public GameObject explosion;

	private MagnetPattern magnet;
	// Use this for initialization
	void Start ()
	{
		magnet = gameObject.GetComponent<MagnetPattern>();
	}

	// Update is called once per frame
	void Update ()
	{
		if(life <= 0f){
			magnet.AlertDying();
			Instantiate(explosion, this.transform.position, Quaternion.identity);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Bullet"){
			life -= 1f;
			GameObject.Destroy(other.gameObject);
		}
	}
}

