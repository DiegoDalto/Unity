using UnityEngine;
using System.Collections;

public class armadilha : MonoBehaviour
{

	public GameObject[] prefab;
	private bool ativo;
	public Transform SpawnPoint;

	public int indice;
	// definir qual objetos devera ser instanciado
	public bool atirar;
	public bool tempo;
	public float timeToSpawn;
	private float tempTime;

	public int percAtivar;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (tempo) {
			tempTime += Time.deltaTime;
			if (tempTime >= timeToSpawn) {
				int temp = Random.Range (1, 100);
				if (temp <= percAtivar) {
					Spawn ();
				}
				tempTime = 0;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player" && !tempo) {
			if (!ativo) {
				Spawn ();
				ativo = true;
			}
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag == "Player") {
			ativo = false;
		}
	}

	void Spawn ()
	{
		GameObject tempPrefab = Instantiate (prefab [indice]) as GameObject;
		tempPrefab.transform.position = SpawnPoint.position;

		if (atirar) {
			tempPrefab.GetComponent<Rigidbody2D> ().gravityScale = 0;
			tempPrefab.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-250, 0));
		} else {
			tempPrefab.GetComponent<Rigidbody2D> ().gravityScale = 1;
		}
	}
}
