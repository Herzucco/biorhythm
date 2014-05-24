using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JoiningPattern : MonoBehaviour {
	private SpringJoint2D join;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MakeJoin(List<Vector2> selfAnchors, List<Vector2> otherAnchors, Rigidbody2D otherBody){
		join = gameObject.GetComponent<SpringJoint2D>();
		join.enabled = true;
		join.anchor = selfAnchors[0];
		join.connectedAnchor = otherAnchors[1];
		join.connectedBody = otherBody;
		enabled = false;
	}
}
