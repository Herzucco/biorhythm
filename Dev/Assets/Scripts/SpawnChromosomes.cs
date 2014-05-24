using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnChromosomes : MonoBehaviour {
	public GameObject Chromosome;
	public List<MagnetPattern> chromosomes;
	public float cooldown;
	public Rect area;

	private int nbChromosomes;
	// Use this for initialization
	void Start () {
		StartCoroutine(CheckChromosomes());
		StartCoroutine(SpawnManagement());
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SpawnChromosome(Vector3 position){
		GameObject chromosome = (GameObject)GameObject.Instantiate(Chromosome, position, Quaternion.identity);
		MagnetPattern magnet = chromosome.GetComponent<MagnetPattern>() ;
		magnet.id = nbChromosomes++;
		magnet.otherChromosomes = chromosomes;
		magnet.speed = Random.Range(5, 10);
		magnet.leadCapacity = Random.Range(0, 100);

		chromosomes.Add(magnet);
	}

	IEnumerator CheckChromosomes() {
		for(int i =0; i < chromosomes.Count; i++){
			MagnetPattern chromosome = chromosomes[i];
			if(chromosome.isAttached){
				chromosomes.RemoveAt(i);
				i--;
			}
		}
		yield return new WaitForSeconds(0.1f);
		StartCoroutine("CheckChromosomes");
	}

	IEnumerator SpawnManagement() {
		float x = Random.Range(area.xMin, area.xMax);
		float y = Random.Range(area.yMin, area.yMax);

		Vector3 position = new Vector3(x, y, 0.0f);
		SpawnChromosome(position);

		yield return new WaitForSeconds(cooldown);

		StartCoroutine("SpawnManagement");
	}
}
