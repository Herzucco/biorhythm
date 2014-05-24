using UnityEngine;
using System.Collections;
using System; //This allows the IComparable Interface
using System.Collections.Generic;

public class MagnetPattern : MonoBehaviour {
	public float speed;
	public float cooldown;
	public int leadCapacity;
	public List<Vector2> anchors;

	private MagnetPattern targetChromosome;
	public List<MagnetPattern> otherChromosomes;
	// Use this for initialization
	void Start () {
		//otherChromosomes = new List<MagnetPattern>();
		anchors = new List<Vector2>();
		AddUp();
		AddDown();
		Debug.Log(anchor);
		StartCoroutine(FindTarget());
	}
	
	// Update is called once per frame
	void Update () {
		if(targetChromosome){
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, targetChromosome.gameObject.transform.position, step);
		}
	}

	public void newChromosome(MagnetPattern chromosome){
		otherChromosomes.Add(chromosome);
	}

	IEnumerator FindTarget() {
		float distance = 0;
		foreach (MagnetPattern chromosome in otherChromosomes){
			float chromosomeDistance = Vector3.Distance(transform.position, chromosome.gameObject.transform.position);;
			if(distance == 0 || distance > chromosomeDistance){
				distance = chromosomeDistance;
				targetChromosome = chromosome;
				continue;
			}
		}
		yield return new WaitForSeconds(cooldown);
	}
	
	void AddUp(){
		GameObject up = transform.Find("Up").gameObject;
		Vector3 position = up.transform.localPosition;
		Vector2 anch = new Vector2(position.x, position.y);
		anchors.Add(anch);
	}
	
	void AddDown(){
		GameObject down = transform.Find("Down").gameObject;
		Vector3 position = down.transform.localPosition;
		Vector2 anch = new Vector2(position.x, position.y);
		anchors.Add(anch);
	}

	public void reachingOther(GameObject other){
		MagnetPattern otherMagnet = other.gameObject.GetComponent<MagnetPattern>();
		gameObject.GetComponent<MagnetPattern>().enabled = false;
		if(leadCapacity <= otherMagnet.leadCapacity){
			JoiningPattern join = gameObject.GetComponent<JoiningPattern>();
			join.enabled = true;
			join.MakeJoin(anchors, otherMagnet.anchors, other.gameObject.rigidbody2D);
			//reach up;
		}
		else{
			//be leader;
		}
	}
}
