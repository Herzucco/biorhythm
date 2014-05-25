using UnityEngine;
using System.Collections;

public class PlayerLifeManager : MonoBehaviour
{
	public float life;
	public PolygonCollider2D collider;
	public GameObject manager;
	// Use this for initialization
	void Start ()
	{
		manager = GameObject.FindGameObjectWithTag("AudioManager");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(life <= 0f){
			StartCoroutine("Reload");
			gameObject.renderer.enabled = false;
			collider.enabled = false;
			ShipController controller = gameObject.GetComponent<ShipController>();
			controller.currentWeapon.SetActive(false);
			controller.enabled = false;
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
		GameObject.Destroy(manager);
		Application.LoadLevel("menu");
	}
}

