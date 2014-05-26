using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour
{
	public float life;
	public GameObject explosion;
	public GameObject camera;
	public GameObject particles;
	private MagnetPattern magnet;
	// Use this for initialization
	void Start ()
	{
		camera = GameObject.FindGameObjectWithTag("TrueMainCamera");
		magnet = gameObject.GetComponent<MagnetPattern>();
	}

	// Update is called once per frame
	void Update ()
	{
		if(life <= 0f){
			magnet.AlertDying();
			camera.GetComponent<PerlinShake>().test = true;
			Instantiate(explosion, this.transform.position, Quaternion.identity);
			Instantiate(particles, this.transform.position, Quaternion.identity);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Bullet"){
			life -= 1f;
			GameObject.Destroy(other.gameObject);
		}
	}
}

