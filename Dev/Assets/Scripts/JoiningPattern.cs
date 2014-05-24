using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JoiningPattern : MonoBehaviour {
	private DistanceJoint2D join;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
//		if(join && join.frequency <= 2f){
//			join.distance -= 0.1f;
//			join.frequency += 0.02f;
//		}
	}

	public void MakeJoin(List<GameObject> selfAnchors, List<GameObject> otherAnchors, Rigidbody2D otherBody, Transform otherTransform){
		Vector3 upSelf = transform.InverseTransformPoint(selfAnchors[0].transform.position);
		Vector2 upSelf2 = new Vector2(upSelf.x, upSelf.y);

		Vector3 downOther = otherTransform.InverseTransformPoint(otherAnchors[1].transform.position);
		Vector2 downOther2 = new Vector2(downOther.x, downOther.y);

		join = gameObject.GetComponent<DistanceJoint2D>();
		join.enabled = true;
		join.anchor = upSelf2;
		join.connectedAnchor = downOther2;
		join.connectedBody = otherBody;
	}

	public void UnJoin(){
		join.enabled = false;
	}
}
