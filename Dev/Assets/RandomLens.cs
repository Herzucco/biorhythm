using UnityEngine;
using System.Collections;

public class RandomLens : MonoBehaviour {
	public Texture2D[] textures;
	
	// Use this for initialization
	void Start () {
		SENaturalBloomAndDirtyLens lens = GetComponent<SENaturalBloomAndDirtyLens>();
		lens.lensDirtTexture = textures[Random.Range(0, textures.Length)];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
