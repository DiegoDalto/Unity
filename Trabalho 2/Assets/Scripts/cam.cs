using UnityEngine;
using System.Collections;

public class cam : MonoBehaviour
{
	public Transform Player;
	private float x;
	private float y;
	public float trasition;
	public bool comLerp;
	public bool segueY;

	public float ajusteY;

	private Transform limitadorEsquerda;
	private Transform limitadorDireita;

	void Start ()
	{
		limitadorEsquerda = GameObject.Find ("limitadorEsquerda").transform;
		limitadorDireita = GameObject.Find ("limitadorDireita").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		x = Player.position.x;

		if (segueY) {
			y = Player.position.y + ajusteY;
		} else {
			y = transform.position.y;
		}

		if (Player.position.x > limitadorEsquerda.position.x && Player.position.x < limitadorDireita.position.x) {
			if (comLerp) {
				transform.position = Vector3.Lerp (transform.position, new Vector3 (x, y, transform.position.z), trasition);
			} else {
				transform.position = new Vector3 (x, y, transform.position.z);
			}
		}
	}
}
