using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public float speed;
	private float xMove;
	private float yMove;
	public string[] gamepadList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		xMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		yMove = Input.GetAxis("Vertical") * speed * Time.deltaTime;

		gamepadList = Input.GetJoystickNames();

		this.rigidbody2D.transform.Translate(xMove, yMove, 0, Space.World);
		if ((Mathf.Abs(Input.GetAxis("Vertical2")) >= 0.25) || (Mathf.Abs(Input.GetAxis("Horizontal2")) >= 0.25))
		{
			this.rigidbody2D.transform.localEulerAngles = new Vector3( 0, 0, Mathf.Atan2( Input.GetAxis("Vertical2"), Input.GetAxis("Horizontal2")) * Mathf.Rad2Deg - 90);
			this.GetComponentInChildren<Gun>().enabled = true;
		}
		if (Input.GetButton("Shoot"))
		{
			this.GetComponentInChildren<Gun>().enabled = true;
		}
		else
		{
			this.GetComponentInChildren<Gun>().enabled = false;
		}

		if (gamepadList.Length == 0)
		{
			Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			diff.Normalize();
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
		}
			//Debug.Log("X : " + xMove + " Y : " + yMove);
	}
}
