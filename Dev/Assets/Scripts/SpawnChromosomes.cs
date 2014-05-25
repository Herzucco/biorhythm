using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnChromosomes : MonoBehaviour {
	public GameObject Chromosome;
	public List<MagnetPattern> chromosomes;
	public float cooldown;
	public Rect area;
	public GameObject player;
	private ShipController playerController;
	private int nbChromosomes;
	// Use this for initialization
	void Start () {
		playerController = player.GetComponent<ShipController>();
		StartCoroutine(CheckChromosomes());
		StartCoroutine(SpawnManagement());
	}
	
	// Update is called once per frame
	void Update () {
		cooldown = Remap(playerController.currentModifier.brutValue, 0f, 1f, 1f, 0f);
	}

	public void SpawnChromosome(Vector3 position){
		GameObject chromosome = (GameObject)GameObject.Instantiate(Chromosome, position, Quaternion.identity);
		MagnetPattern magnet = chromosome.GetComponent<MagnetPattern>() ;
		magnet.id = nbChromosomes++;
		magnet.otherChromosomes = chromosomes;
		magnet.speed = Random.Range(2, 4);
		magnet.leadCapacity = Random.Range(5, 20);
		magnet.player = player;

		chromosomes.Add(magnet);
	}

	IEnumerator CheckChromosomes() {
		for(int i =0; i < chromosomes.Count; i++){
			MagnetPattern chromosome = chromosomes[i];
			if(chromosome.isAttached || chromosome.isFull){
				chromosomes.RemoveAt(i);
				i--;
			}
		}
		yield return new WaitForSeconds(1f);
		StartCoroutine("CheckChromosomes");
	}

	IEnumerator SpawnManagement() {
		if(player){
			float x = player.transform.position.x;
			float y = player.transform.position.y;

			float count = 0;
			while(IsBeetween(x, player.transform.position.x-2, player.transform.position.x+2) ||
			      IsBeetween(y, player.transform.position.y-2, player.transform.position.y+2)){

				x = Random.Range(area.xMin, area.xMax);
				y = Random.Range(area.yMin, area.yMax);


				count++;

				if(count >= 20){
					break;
				}
			}

			Vector3 position = new Vector3(x, y, 0.0f);
			SpawnChromosome(position);
		}

		yield return new WaitForSeconds(cooldown);

		StartCoroutine("SpawnManagement");
	}

	public float Remap (float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	private bool IsBeetween(float value, float min, float max){
		return (value >= min && value <= max);
	}
}
