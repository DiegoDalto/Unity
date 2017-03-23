using UnityEngine;
using System.Collections;

public class ComportamentoPedra : MonoBehaviour
{
	public float timeLife;
	private float tempTime;


	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		tempTime += Time.deltaTime;
		if (tempTime >= timeLife) {
			
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Player") {

			Destroy (this.gameObject);
		}
	}
}
