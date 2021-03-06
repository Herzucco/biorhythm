﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed;
	public float xMax;
	public bool moveBySpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (moveBySpeed == true)
		this.speed = this.GetComponent<VisGameObjectPropertyModifier>().returnedValue;

		this.transform.Translate (Vector3.up * speed * Time.deltaTime, Space.Self);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Wall"){
			Destroy(this.gameObject);
		}
	}
}