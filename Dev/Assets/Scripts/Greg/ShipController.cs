using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public float speed;
	private float xMove;
	private float yMove;
	private string[] gamepadList;
	public GameObject currentWeapon;
	public GameObject bassWeapon;
	public GameObject highWeapon;
	public GameObject midWeapon;
	public GameObject[] walls;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		xMove = Input.GetAxis("Horizontal") * speed;
		yMove = Input.GetAxis("Vertical") * speed;

		gamepadList = Input.GetJoystickNames();
		Vector2 posClam = transform.position;
		posClam.x = Mathf.Clamp(transform.position.x, walls[0].transform.position.x + walls[0].transform.localScale.x, walls[1].transform.position.x - walls[1].transform.localScale.x);
		posClam.y = Mathf.Clamp(transform.position.y, walls[2].transform.position.y + walls[2].transform.localScale.y, walls[3].transform.position.y - walls[3].transform.localScale.y);
		this.transform.position = posClam;


	if (gamepadList.Length > 0)
	{
		this.rigidbody2D.transform.Translate(xMove, yMove, 0, Space.World);
		if ((Mathf.Abs(Input.GetAxis("Vertical2")) >= 0.25) || (Mathf.Abs(Input.GetAxis("Horizontal2")) >= 0.25))
		{
			this.rigidbody2D.transform.localEulerAngles = new Vector3( 0, 0, Mathf.Atan2( Input.GetAxis("Vertical2"), Input.GetAxis("Horizontal2")) * Mathf.Rad2Deg - 90);
			currentWeapon.GetComponent<Gun>().enabled = true;
			this.GetComponent<Light>().enabled = true;
		}
		else
		{
			currentWeapon.GetComponent<Gun>().enabled = false;
			this.GetComponent<Light>().enabled = false;
		}
		if (Input.GetButton("Bass Weapon"))
		{
			currentWeapon = bassWeapon;
			midWeapon.GetComponent<Gun>().enabled = false;
			highWeapon.GetComponent<Gun>().enabled = false;
			this.GetComponent<Light>().color = Color.red;
		}
		if (Input.GetButton("High Weapon"))
		{
			currentWeapon = highWeapon;
			bassWeapon.GetComponent<Gun>().enabled = false;
			midWeapon.GetComponent<Gun>().enabled = false;
			this.GetComponent<Light>().color = Color.blue;
		}
		if (Input.GetButton("Mid Weapon"))
		{
			currentWeapon = midWeapon;
			bassWeapon.GetComponent<Gun>().enabled = false;
			highWeapon.GetComponent<Gun>().enabled = false;
			this.GetComponent<Light>().color = Color.green;
		}
	}

		if (gamepadList.Length == 0)
		{
			this.rigidbody2D.transform.Translate(xMove/2, yMove/2, 0, Space.World);
			Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			diff.Normalize();
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			if (Input.GetButton("Shoot"))
			{
				currentWeapon.GetComponent<Gun>().enabled = true;
				this.GetComponent<Light>().enabled = true;
			}
			else
			{
				currentWeapon.GetComponent<Gun>().enabled = false;
				this.GetComponent<Light>().enabled = false;
			}
			if (Input.GetButton("Bass Weapon"))
			{
				currentWeapon = bassWeapon;
				midWeapon.GetComponent<Gun>().enabled = false;
				highWeapon.GetComponent<Gun>().enabled = false;
				this.GetComponent<Light>().color = Color.red;
			}
			if (Input.GetButton("High Weapon"))
			{
				currentWeapon = highWeapon;
				bassWeapon.GetComponent<Gun>().enabled = false;
				midWeapon.GetComponent<Gun>().enabled = false;
				this.GetComponent<Light>().color = Color.blue;
			}
			if (Input.GetButton("Mid Weapon"))
			{
				currentWeapon = midWeapon;
				bassWeapon.GetComponent<Gun>().enabled = false;
				highWeapon.GetComponent<Gun>().enabled = false;
				this.GetComponent<Light>().color = Color.green;
			}
		}
			//Debug.Log("X : " + xMove + " Y : " + yMove);
	}
}
