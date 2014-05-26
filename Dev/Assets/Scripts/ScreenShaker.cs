using UnityEngine;
using System.Collections;

public class ScreenShaker : MonoBehaviour {
	public float shake;
	public float shakeAmount;
	public float decreaseFactor;
	public float frequency;

	private float count;
	private Vector3 basePosition;
	// Use this for initialization
	void Start () {
		basePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		count += Time.deltaTime;
		if (shake > 0 && count >= frequency) {
			Debug.Log(transform.localPosition);
			Vector3 newPos = new Vector3(transform.position.x + Random.Range(0f, shakeAmount) - Random.Range(0f, shakeAmount),
			                             transform.position.y + Random.Range(0f, shakeAmount) - Random.Range(0f, shakeAmount),
			                             basePosition.z);
			transform.position = newPos;
			shake -= Time.deltaTime * decreaseFactor;
			count = 0;
		} else {
			shake = 0.0f;
			transform.position = basePosition;
		}
	}
	
	public void Shake(float force){
		shake = force;
		count = frequency;
	}
}
