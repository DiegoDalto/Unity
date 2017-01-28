using UnityEngine;
using System.Collections;

public class onWay : MonoBehaviour
{

	//posicao da superfice
	public Transform superficie;
	//colisor da plataforma
	private Collider2D colisor;
	//vai armazenar a posicao Y do pé do personagem
	private float y;

	// Use this for initialization
	void Start ()
	{
		colisor = GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		y = GameObject.Find ("GroundCheck").transform.position.y;
		if (y < superficie.position.y) {
			colisor.enabled = false;
		} else {
			colisor.enabled = true;
		}
	}
}
