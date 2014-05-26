using UnityEngine;
using System.Collections;

public class PlayerLifeManager : MonoBehaviour
{
	public float life;
	public PolygonCollider2D collider;
	public GameObject manager;
	public GameObject explosion;
	// Use this for initialization
	void Start ()
	{
		manager = GameObject.FindGameObjectWithTag("AudioManager");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(life <= 0f && life > -100f){
			StartCoroutine("Reload");
			gameObject.renderer.enabled = false;
			collider.enabled = false;
			ShipController controller = gameObject.GetComponent<ShipController>();
			controller.currentWeapon.SetActive(false);
			controller.enabled = false;
			for(int i = 0; i < 10; i++){
				float x = Random.Range(gameObject.transform.position.x-1,gameObject.transform.position.x+1);
				float y = Random.Range(gameObject.transform.position.y-1,gameObject.transform.position.y+1);

				Instantiate(explosion, new Vector3(x, y, 0.0f), gameObject.transform.rotation);
			}
			controller.shakeCamera.test = true;
			controller.shakeCamera.duration =  2;
			controller.shakeCamera.speed =  20;
			controller.shakeCamera.magnitude = 1;
			life = -100f;
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
		manager.GetComponent<AudioManager>().source.Stop();
		Application.LoadLevel("menu");
	}
}

