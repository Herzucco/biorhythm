using UnityEngine;
using System.Collections;

public class Explosions : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<DetonatorShockwave>().size = this.GetComponent<VisGameObjectPropertyModifier>().brutValue;
		this.GetComponent<DetonatorHeatwave>().size = this.GetComponent<VisGameObjectPropertyModifier>().brutValue;
		if (player.GetComponent<ShipController>().currentWeapon.name == "Gun Bass")
		{
			this.GetComponent<DetonatorShockwave>().color = Color.red;
			this.GetComponent<DetonatorHeatwave>().color = Color.red;
		}
		if (player.GetComponent<ShipController>().currentWeapon.name == "Gun High")
		{
			this.GetComponent<DetonatorShockwave>().color = Color.blue;
			this.GetComponent<DetonatorHeatwave>().color = Color.blue;
		}
		if (player.GetComponent<ShipController>().currentWeapon.name == "Gun Mid")
		{
			this.GetComponent<DetonatorShockwave>().color = Color.green;
			this.GetComponent<DetonatorHeatwave>().color = Color.green;
		}
	}
}
