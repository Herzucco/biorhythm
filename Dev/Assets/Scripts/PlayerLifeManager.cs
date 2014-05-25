using UnityEngine;
using System.Collections;

public class PlayerLifeManager : MonoBehaviour
{
	public float life;
	public PolygonCollider2D collider;
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(life <= 0f){
			StartCoroutine("Reload");
			gameObject.renderer.enabled = false;
			collider.enabled = false;
			gameObject.GetComponent<ShipController>().enabled = false;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Chromosome"){
			life -= 1f;
			GameObject.Destroy(other.gameObject);
		}
	}

	IEnumerator Reload() {
		yield return new WaitForSeconds(3f);
		Application.LoadLevel("ennemy");
	}
}
