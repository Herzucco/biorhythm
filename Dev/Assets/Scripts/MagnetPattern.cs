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
	public List<MagnetPattern> otherChromosomes;
	// Use this for initialization
	void Start () {
		anchors = new List<GameObject>();
		leaded = new List<MagnetPattern>();
		AddUp();
		AddDown();
		StartCoroutine(FindTarget());
	}
	
	// Update is called once per frame
	void Update () {
		if(targetChromosome && !isAttached){
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
			if(chromosome.id != id && (distance == 0 || distance > chromosomeDistance)){
				distance = chromosomeDistance;
				targetChromosome = chromosome;
				continue;
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
					MagnetPattern lastLeaded = otherMagnet.leaded[count-(Random.Range(1, count))];
					join.MakeJoin(anchors, lastLeaded.anchors, lastLeaded.gameObject.rigidbody2D, lastLeaded.transform);
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
			if(!leader.isInOurGroup(otherMagnet)){
				leader.reachingOther(other, detector);
			}
		}
	}

	private void ClearLeaded(MagnetPattern other){
		for(int i = 0; i < leaded.Count; i++){
			other.leaded.Add(leaded[i]);
			leaded[i].leader = other;
		}
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

	IEnumerator Attach() {
		yield return new WaitForSeconds(0.1f);
		isAttached = true;
		leader.leaded.Add(this);
		ClearLeaded(leader);
	}
}
