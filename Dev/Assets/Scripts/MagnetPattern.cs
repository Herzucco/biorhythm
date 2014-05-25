using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagnetPattern : MonoBehaviour {
	public float speed;
	public float cooldown;
	public int leadCapacity;
	public List<GameObject> anchors;

	public bool isLeading;
	public bool isAttached;
	public int id;

	public MagnetPattern leader;
	public List<MagnetPattern> leaded;
	private MagnetPattern targetChromosome;
	public bool isFull;
	public List<MagnetPattern> otherChromosomes;
	public GameObject player;
	public bool isDead;
	private Vector3 target;
	private ShipController playerController;
	// Use this for initialization
	void Start () {
		isDead = false;
		playerController = player.GetComponent<ShipController>();
		anchors = new List<GameObject>();
		leaded = new List<MagnetPattern>();
		AddUp();
		AddDown();
		StartCoroutine(FindTarget());
		target = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(leaded.Count >= leadCapacity){
			isFull = true;
		} else{
			isFull = false;
		}

		if(targetChromosome && !isAttached && !isFull && playerController.currentModifier){
			float step = speed * playerController.currentModifier.brutValue * 10 * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, targetChromosome.gameObject.transform.position, step);
		} else if(player && playerController.currentModifier){
			float distance = Vector3.Distance(transform.position, player.transform.position);
			if(distance <= 10.0f){
				target = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
			}
			float step = speed * playerController.currentModifier.brutValue * 10 * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target, step);
		}
	}

	public void newChromosome(MagnetPattern chromosome){
		otherChromosomes.Add(chromosome);
	}

	IEnumerator FindTarget() {
		float distance = 0;
		foreach (MagnetPattern chromosome in otherChromosomes){
			if(chromosome){
				float chromosomeDistance = Vector3.Distance(transform.position, chromosome.gameObject.transform.position);
				if(chromosome.id != id && (distance == 0 || distance > chromosomeDistance) && !chromosome.isFull){
					distance = chromosomeDistance;
					targetChromosome = chromosome;
					continue;
				}
			}
		}
		yield return new WaitForSeconds(cooldown);
		StartCoroutine("FindTarget");
	}
	
	void AddUp(){
		GameObject up = transform.Find("Up").gameObject;
		anchors.Add(up);
	}
	
	void AddDown(){
		GameObject down = transform.Find("Down").gameObject;
		anchors.Add(down);
	}

	public void reachingOther(GameObject other, Detector detector){
		MagnetPattern otherMagnet = other.gameObject.GetComponent<MagnetPattern>();
		if(!isAttached){
			if(!otherMagnet.isAttached &&
			    (leadCapacity < otherMagnet.leadCapacity ||
			     (otherMagnet.isLeading && leadCapacity == otherMagnet.leadCapacity))){
				JoiningPattern join = gameObject.GetComponent<JoiningPattern>();
				join.enabled = true;
				if(otherMagnet.leaded.Count > 0){
					int count = otherMagnet.leaded.Count;
					MagnetPattern lastLeaded = otherMagnet.leaded[count-(Random.Range(1, 1))];
					if(lastLeaded) join.MakeJoin(anchors, lastLeaded.anchors, lastLeaded.gameObject.rigidbody2D, lastLeaded.transform);
				}else{
					join.MakeJoin(anchors, otherMagnet.anchors, other.gameObject.rigidbody2D, other.transform);
				}
				isLeading = false;
				leader = otherMagnet;
				//detector.enabled = false;
				StartCoroutine("Attach");
			}
			else if(!otherMagnet.isAttached){
				isLeading = true;
				isAttached = false;
			}
		}else{
//			if(!leader.isInOurGroup(otherMagnet)){
//				//leader.reachingOther(other, detector);
//			}
		}
	}

	private void ClearLeaded(MagnetPattern other){
		for(int i = 0; i < leaded.Count; i++){
			other.leaded.Add(leaded[i]);
			leaded[i].leader = other;
		}
		//other.leaded = leaded;
		leaded.Clear();
	}

	public bool isInOurGroup(MagnetPattern other){
		if(other.id == id)return true;
		for(int i = 0; i < leaded.Count; i++){
			if(leaded[i].id == other.id){
				return true;
			}
		}
		return false;
	}

	public void AlertDying(){
		List<MagnetPattern> group;
		if(isLeading){
			group = leaded;
			for(int i = 1; i < group.Count; i++){
				group[0].leaded.Add(group[i]);
				group[i].leader = group[0];
			}
		}
		else if(isAttached){
			group = leader.leaded;
			for(int i = 0; i < group.Count; i++){
				if(group[i].id == id){
					group.RemoveAt(i);
					break;
				}
			}
		}
		if(!isAttached){
			for(int i = 0; i < otherChromosomes.Count; i++){
				if(otherChromosomes[i].id == id){
					otherChromosomes.RemoveAt(i);
					break;
				}
			}
		}
		GameObject.Destroy(gameObject);
	}

	IEnumerator Attach() {
		yield return new WaitForSeconds(0.1f);
		isAttached = true;
		leader.leaded.Add(this);
//		JoiningPattern join = gameObject.GetComponent<JoiningPattern>();
//		join.UnJoin();
//		transform.parent = leader.transform;
//		join.enabled = false;
		ClearLeaded(leader);
	}
}
